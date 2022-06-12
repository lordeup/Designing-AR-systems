using System.Linq;
using UnityEngine;
using Card.Type;
using Field;
using Photon.Pun;
using Player;
using Room;
using Storage;
using Tracking.CompanyCard;
using Tracking.NumberCard;
using UI;
using Utils;
using PhotonPlayer = Photon.Realtime.Player;

public class MultiplayerGameManager : MonoBehaviour
{
    [SerializeField] private CompanyCardRecognizer companyCardRecognizer;
    [SerializeField] private NumberCardRecognizer numberCardRecognizer;

    [SerializeField] private UIManager uiManager;
    [SerializeField] private FieldManager fieldManager;
    [SerializeField] private PlayerUtils playerUtils;

    public bool isTrackingFound;

    private GameStorage _storage;
    private bool _isInitialized;
    private PhotonPlayer _currentPlayer;
    private PhotonView _photonView;

    private void Start()
    {
        _storage = gameObject.AddComponent<GameStorage>();
        _photonView = GetComponent<PhotonView>();

        companyCardRecognizer.AddListenerToCard(SetCompanyCard);
    }

    private void Update()
    {
        if (!_isInitialized && !SharedUtils.IsNull(companyCardRecognizer.Card))
        {
            InitField();
            _isInitialized = true;
        }

        if (numberCardRecognizer.GetCountCards() == 2)
        {
            StartCoroutine(CustomWaitUtils.WaitWhile(
                () => !isTrackingFound,
                () => ExecutePassTurn(ExecuteMove))
            );
        }
    }

    private void InitField()
    {
        var player = playerUtils.InitializationPlayer();
        fieldManager.Data = new PlayerData(player, companyCardRecognizer.Card);
        companyCardRecognizer.RemoveAllListeners();
        _currentPlayer = playerUtils.GetMasterPlayer();

        ShowTurnPlayer();
        SetActiveUI(true);
    }

    private void ExecuteMove()
    {
        var amount = numberCardRecognizer.GetAmountCards();
        fieldManager.ExecuteMove(amount);
        numberCardRecognizer.ClearCards();

        var cellManager = fieldManager.GetCurrentCellManager();
        Log(cellManager);
        HandleCommand(cellManager);
    }

    private void HandleCommand(CellManager cellManager)
    {
        var money = cellManager.money;
        var cellType = cellManager.cellType;

        if (cellType == CellType.Finish)
        {
            SetActiveUI(false);
            CheckForWin();
        }
        else if (cellType == CellType.Profit)
        {
            ExecutePassTurn(() => fieldManager.ProfitCellCommand(money));
        }
        else if (cellType == CellType.Cost)
        {
            ExecutePassTurn(() => fieldManager.CostCellCommand(money));
        }
        else if (cellType == CellType.Impact)
        {
            var impactPointManager = uiManager.GetImpactPointManager();
            impactPointManager.SetActivePanel(true);
            impactPointManager.SetPanelValues();

            StartCoroutine(CustomWaitUtils.WaitWhile(
                () => ArrayUtils.IsEmpty(impactPointManager.GetSelectedImpactValues()),
                () => ExecutePassTurn(ActionAfterFindingImpactCard))
            );
        }
    }

    private void ActionAfterFindingImpactCard()
    {
        var impactPointManager = uiManager.GetImpactPointManager();
        fieldManager.ImpactCellCommand(impactPointManager.GetSelectedImpactValues());
        impactPointManager.ClearImpactItem();
        impactPointManager.SetActivePanel(false);
    }

    private void ExecutePassTurn(CustomWaitUtils.DelegateWaitMethod method)
    {
        method?.Invoke();
        PassTurn();
    }

    private void PassTurn()
    {
        var playerType = playerUtils.GetPlayerType(_currentPlayer);

        var player = playerType switch
        {
            // PlayerType.Player1 => playerUtils.GetPlayerByType(PlayerType.Player2),
            // PlayerType.Player2 => playerUtils.GetPlayerByType(PlayerType.Player1),
            _ => _currentPlayer
        };

        SetCurrentPlayer(player.UserId);
    }

    private void SetCurrentPlayer(string userId)
    {
        _photonView.RPC("SetCurrentPlayerRpc", RpcTarget.All, userId);
    }

    [PunRPC]
    private void SetCurrentPlayerRpc(string userId)
    {
        _currentPlayer = playerUtils.GetPlayerById(userId);
        ShowTurnPlayer();
    }

    private void SetCompanyCard(CompanyCardType type)
    {
        companyCardRecognizer.Card = _storage.GetCompanyCardByType(type);
        uiManager.Log("Карта компании: " + type);
    }

    private void SetNumberCard(NumberCardType type)
    {
        numberCardRecognizer.AddCard(type);
        uiManager.Log("Карта числа: " + type);
    }

    private void Log(CellManager cellManager)
    {
        var cellName = cellManager.name;
        var cellMoney = cellManager.money;
        var cellType = cellManager.cellType;

        var text = cellType switch
        {
            CellType.Cost => cellName + " -" + cellMoney,
            CellType.Profit => cellName + " +" + cellMoney,
            _ => ""
        };

        if (string.IsNullOrEmpty(text)) return;
        uiManager.SetActionValue(text);
        uiManager.ShowActionPanel();
    }

    private void CheckForWin()
    {
        var properties = playerUtils.GetPlayerMoneyCustomProperties();
        var maxValueKey = properties.OrderByDescending(item => item.Value).First().Key;
        ShowPlayerWin(maxValueKey);
    }

    private void ShowPlayerWin(string userId)
    {
        if (IsMine(userId))
        {
            uiManager.ShowWinningPanel();
        }
        else
        {
            uiManager.ShowLosingPanel();
        }
    }

    private void ShowTurnPlayer()
    {
        if (IsMine(_currentPlayer.UserId))
        {
            numberCardRecognizer.AddListenerToCard(SetNumberCard);
            uiManager.ShowYourTurn();
        }
        else
        {
            numberCardRecognizer.RemoveAllListeners();
            uiManager.ShowOtherTurnPanel();
        }

        UpdateScoreMoneyValue();
    }

    private void UpdateScoreMoneyValue()
    {
        SetScoreValue();
        SetMoneyValue();
        StartCoroutine(CustomWaitUtils.WaitForSeconds(ShowDataScoreMoney, 0.5f));
    }

    private void SetScoreValue()
    {
        var userId = GetLocalUserId();
        var score = fieldManager.Data.CompanyCard.Score;

        var properties = playerUtils.GetPlayerScoreCustomProperties();
        var dictionary = ArrayUtils.SetDictionaryValue(properties, userId, score);

        CustomPropertyUtils.UpdateCustomPropertyByKey(CustomPropertyKeys.PlayerScore, dictionary);
    }

    private void SetMoneyValue()
    {
        var userId = GetLocalUserId();
        var money = fieldManager.Data.CompanyCard.Money;

        var properties = playerUtils.GetPlayerMoneyCustomProperties();
        var dictionary = ArrayUtils.SetDictionaryValue(properties, userId, money);

        CustomPropertyUtils.UpdateCustomPropertyByKey(CustomPropertyKeys.PlayerMoney, dictionary);
    }

    private void ShowDataScoreMoney()
    {
        var players = playerUtils.GetPlayers();
        var scoreProperties = playerUtils.GetPlayerScoreCustomProperties();
        var moneyProperties = playerUtils.GetPlayerMoneyCustomProperties();

        foreach (var player in players)
        {
            var userId = player.UserId;
            if (scoreProperties.TryGetValue(userId, out var score))
            {
                if (IsMine(userId))
                {
                    uiManager.SetMyScoreValue(score);
                }
                else
                {
                    uiManager.SetOtherScoreValue(score);
                }
            }

            if (moneyProperties.TryGetValue(userId, out var money))
            {
                if (IsMine(userId))
                {
                    uiManager.SetMyMoneyValue(money);
                }
                else
                {
                    uiManager.SetOtherMoneyValue(money);
                }
            }
        }
    }

    private bool IsMine(string userId)
    {
        return GetLocalUserId().Equals(userId);
    }

    private string GetLocalUserId()
    {
        return playerUtils.GetLocalPlayer().UserId;
    }

    // TODO move?
    private void SetActiveUI(bool state)
    {
        uiManager.SetActiveMyScoreMoneyPanel(state);
        uiManager.SetActiveOtherScoreMoneyPanel(state);
        uiManager.SetActiveLogPanel(state);
    }
}
