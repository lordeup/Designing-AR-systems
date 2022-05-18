using System.Globalization;
using UnityEngine;
using Card.Type;
using Field;
using Photon.Pun;
using Player;
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
            numberCardRecognizer.AddListenerToCard(SetNumberCard);
            _isInitialized = true;
        }

        if (numberCardRecognizer.GetCountCards() == 2)
        {
            MakeMove();
            numberCardRecognizer.ClearCards();
        }
    }

    private void InitField()
    {
        var player = playerUtils.InitializationPlayer();
        fieldManager.Data = new PlayerData(player, founderCardRecognizer.Card);
        founderCardRecognizer.RemoveAllListeners();
        _currentPlayer = playerUtils.GetPlayerByType(PlayerType.Player1);

        ShowTurnPlayer();
        SetActiveUI(true);
    }

    private void MakeMove()
    {
        var amount = numberCardRecognizer.GetAmountCards();
        fieldManager.MakeMove(amount);

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
            // TODO add check win
            uiManager.ShowWinningPanel();
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
            // PlayerType.Player1 => playerUtils.GetPlayerByType(PlayerType.Player2),
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

    // private void SyncParameters()
    // {
    // }
    //
    // [PunRPC]
    // private void SetPlayerParametersRpc()
    // {
    // }

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
        else if (cellType == CellType.Impact)
        {
        }
        else
        {
            uiManager.Log("");
        }
    }

    private void ShowTurnPlayer()
    {
        if (IsMyTurn())
            uiManager.ShowYourTurn();
        else
            uiManager.ShowOtherTurnPanel();

        SetMoneyValue();
    }

    private void SetMoneyValue()
    {
        var money = fieldManager.Data.FounderCard.Money;
        uiManager.SetMoneyValue(money.ToString(CultureInfo.InvariantCulture));
    }

    private bool IsMyTurn()
    {
        return playerUtils.GetLocalPlayer().Equals(_currentPlayer);
    }

    private void SetActiveUI(bool state)
    {
        uiManager.SetActiveMoneyPanel(state);
        uiManager.SetActiveLogPanel(state);
    }
}
