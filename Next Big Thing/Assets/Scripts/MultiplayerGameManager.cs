using System.Collections.Generic;
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

    private const int CountNumberCard = 2;

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

        if (numberCardRecognizer.GetCountCards() >= CountNumberCard)
        {
            ExecuteMove();
            // StartCoroutine(CustomWaitUtils.WaitWhile(() => !isTrackingFound, ExecuteMove));
        }
    }

    private void InitField()
    {
        var player = playerUtils.InitializationPlayer();
        fieldManager.Data = new PlayerData(player, companyCardRecognizer.Card);
        companyCardRecognizer.RemoveAllListeners();
        _currentPlayer = playerUtils.GetMasterPlayer();

        ShowTurnPlayer();
        uiManager.SetActiveUI(true);
    }

    private void ExecuteMove()
    {
        var amount = numberCardRecognizer.GetAmountCards(CountNumberCard);
        fieldManager.ExecuteMove(amount);

        var cellManager = fieldManager.GetCurrentCellManager();
        CellLogging(cellManager);

        uiManager.WaitActionPanelActive(() => HandleCommand(cellManager));
        numberCardRecognizer.ClearCards();
    }

    private void HandleCommand(CellManager cellManager)
    {
        var money = cellManager.money;
        var cellType = cellManager.cellType;

        if (cellType == CellType.Finish)
        {
            uiManager.SetActiveUI(false);
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

    private void ExecutePassTurn(SharedUtils.DelegateMethod method)
    {
        method?.Invoke();
        PassTurn();
    }

    private void PassTurn()
    {
        var playerType = playerUtils.GetPlayerType(_currentPlayer);

        var player = playerType switch
        {
            PlayerType.Player1 => playerUtils.GetPlayerByType(PlayerType.Player2),
            PlayerType.Player2 => playerUtils.GetPlayerByType(PlayerType.Player1),
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
        if (numberCardRecognizer.GetCountCards() >= CountNumberCard) return;

        numberCardRecognizer.AddCard(type);
        uiManager.Log("Карта числа: " + type);
    }

    private void CellLogging(CellManager cellManager)
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

        uiManager.ShowActionPanel(text);
    }

    private void ShowTurnPlayer()
    {
        SetScoreValue();
        SetMoneyValue();

        if (PhotonNetworkUtils.IsMine(_currentPlayer.UserId))
        {
            numberCardRecognizer.AddListenerToCard(SetNumberCard);
            uiManager.ShowYourTurn();
        }
        else
        {
            numberCardRecognizer.RemoveAllListeners();
            uiManager.ShowOtherTurnPanel();
        }

        uiManager.WaitTurnPanelActive(ShowDataScoreMoney);
    }

    private void SetScoreValue()
    {
        var userId = PhotonNetworkUtils.GetLocalUserId();
        var score = fieldManager.Data.CompanyCard.Score;

        var properties = GetScoreProperties();
        var dictionary = ArrayUtils.SetDictionaryValue(properties, userId, score);

        CustomPropertyUtils.UpdateCustomPropertyByKey(CustomPropertyKeys.PlayerScore, dictionary);
    }

    private void SetMoneyValue()
    {
        var userId = PhotonNetworkUtils.GetLocalUserId();
        var money = fieldManager.Data.CompanyCard.Money;

        var properties = GetMoneyProperties();
        var dictionary = ArrayUtils.SetDictionaryValue(properties, userId, money);

        CustomPropertyUtils.UpdateCustomPropertyByKey(CustomPropertyKeys.PlayerMoney, dictionary);
    }

    private void ShowDataScoreMoney()
    {
        var players = playerUtils.GetPlayers();
        var scoreProperties = GetScoreProperties();
        var moneyProperties = GetMoneyProperties();

        foreach (var player in players)
        {
            var userId = player.UserId;
            if (scoreProperties.TryGetValue(userId, out var score))
            {
                uiManager.ShowScoreValue(userId, score);
                if (score <= 0)
                {
                    uiManager.ShowPlayerLose(userId);
                }
            }

            if (moneyProperties.TryGetValue(userId, out var money))
            {
                uiManager.ShowMoneyValue(userId, money);
                if (money <= 0)
                {
                    uiManager.ShowPlayerLose(userId);
                }
            }
        }
    }

    private void CheckForWin()
    {
        var moneyProperties = GetMoneyProperties();
        var maxValueKey = moneyProperties.OrderByDescending(item => item.Value).First().Key;
        uiManager.ShowPlayerWin(maxValueKey);
    }

    private Dictionary<string, double> GetScoreProperties()
    {
        return playerUtils.GetPlayerScoreCustomProperties();
    }

    private Dictionary<string, double> GetMoneyProperties()
    {
        return playerUtils.GetPlayerMoneyCustomProperties();
    }
}
