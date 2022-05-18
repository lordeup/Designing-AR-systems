using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;
using Card.Type;
using Field;
using Photon.Pun;
using Player;
using Room;
using Tracking.FounderCard;
using Tracking.ImpactPoint;
using Tracking.NumberCard;
using PhotonPlayer = Photon.Realtime.Player;

public class MultiplayerGameManager : MonoBehaviour
{
    [SerializeField] private FounderCardRecognizer founderCardRecognizer;
    [SerializeField] private ImpactPointRecognizer impactPointRecognizer;
    [SerializeField] private NumberCardRecognizer numberCardRecognizer;

    [SerializeField] private UIManager uiManager;
    [SerializeField] private FieldManager fieldManager;
    [SerializeField] private PlayerUtils playerUtils;

    private GameStorage _storage;
    private bool _isInitialized;
    private PhotonPlayer _currentPlayer;
    private PhotonView _photonView;

    private void Start()
    {
        _storage = gameObject.AddComponent<GameStorage>();
        _photonView = GetComponent<PhotonView>();

        founderCardRecognizer.AddListenerToCard(SetFounderCard);
    }

    private void Update()
    {
        if (!_isInitialized && !Utils.IsNull(founderCardRecognizer.Card))
        {
            InitField();
            _isInitialized = true;
        }

        if (numberCardRecognizer.GetCountCards() == 2)
        {
            ExecuteMove();
            numberCardRecognizer.ClearCards();
        }
    }

    private void InitField()
    {
        var player = playerUtils.InitializationPlayer();
        fieldManager.Data = new PlayerData(player, founderCardRecognizer.Card);
        founderCardRecognizer.RemoveAllListeners();
        _currentPlayer = playerUtils.GetMasterPlayer();

        ShowTurnPlayer();
        SetActiveUI(true);
    }

    private void ExecuteMove()
    {
        var amount = numberCardRecognizer.GetAmountCards();
        fieldManager.ExecuteMove(amount);

        var cellManager = fieldManager.GetCurrentCellManager();
        Log(cellManager);
        HandleCommand(cellManager);
        SetMoneyValue();
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
            impactPointRecognizer.AddListenerToCard(SetImpactPoint);
            uiManager.Log("Отсканируйте камерой карточку влияния.");

            StartCoroutine(CustomWaitUtils.WaitWhile(
                () => Utils.IsNull(impactPointRecognizer.Card),
                () => ExecutePassTurn(ActionAfterFindingImpactCard))
            );
        }
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

    private void SetFounderCard(FounderCardType type)
    {
        founderCardRecognizer.Card = _storage.GetFounderCardByType(type);
        uiManager.Log("Карта основателя: " + type);
    }

    private void SetImpactPoint(ImpactPointType type)
    {
        impactPointRecognizer.Card = _storage.GetImpactPointByType(type);
        uiManager.Log("Карта влияния: " + type);
    }

    private void SetNumberCard(NumberCardType type)
    {
        numberCardRecognizer.AddCard(type);
        uiManager.Log("Карта числа: " + type);
    }

    private void ActionAfterFindingImpactCard()
    {
        fieldManager.ImpactCellCommand(impactPointRecognizer.Card);
        impactPointRecognizer.Card = null;
        impactPointRecognizer.RemoveAllListeners();
    }

    private void Log(CellManager cellManager)
    {
        var cellName = cellManager.name;
        var cellMoney = cellManager.money;
        var cellType = cellManager.cellType;

        if (cellType == CellType.Cost)
        {
            uiManager.Log(cellName + " -" + cellMoney);
        }
        else if (cellType == CellType.Profit)
        {
            uiManager.Log(cellName + " +" + cellMoney);
        }
        else
        {
            uiManager.Log("");
        }
    }

    private void CheckForWin()
    {
        var property = playerUtils.GetPlayerMoneyCustomProperties();
        var maxValueKey = property.OrderByDescending(item => item.Value).First().Key;
        Debug.LogWarning("maxValueKey " + maxValueKey);
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

        SetMoneyValue();
    }

    private void SetMoneyValue()
    {
        var userId = playerUtils.GetLocalPlayer().UserId;
        var money = fieldManager.Data.FounderCard.Money;

        var property = playerUtils.GetPlayerMoneyCustomProperties();
        var dictionaryMoney = new Dictionary<string, double>(property);

        if (dictionaryMoney.ContainsKey(userId))
        {
            dictionaryMoney[userId] = money;
        }
        else
        {
            dictionaryMoney.Add(userId, money);
        }

        CustomPropertyUtils.UpdateCustomPropertyByKey(CustomPropertyKeys.PlayerMoney, dictionaryMoney);
        uiManager.SetMoneyValue(money.ToString(CultureInfo.InvariantCulture));
    }

    private bool IsMine(string userId)
    {
        return playerUtils.GetLocalPlayer().UserId.Equals(userId);
    }

    private void SetActiveUI(bool state)
    {
        uiManager.SetActiveMoneyPanel(state);
        uiManager.SetActiveLogPanel(state);
    }
}
