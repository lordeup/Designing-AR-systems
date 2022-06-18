using UnityEngine;
using Card.Type;
using Field;
using Photon.Pun;
using Player;
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
    private PhotonView _photonView;

    private PhotonPlayer _currentPlayer;
    private double _myMoney;
    private double _myScore;
    private double _otherMoney;
    private double _otherScore;

    private bool _isMyFinish;
    private bool _isOtherFinish;

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
        SyncParameters();
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
            ExecutePassTurn(PlayerFinished);
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
        SyncParameters();
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

    private void SyncParameters()
    {
        var userId = PhotonNetworkUtils.GetLocalUserId();
        var score = fieldManager.Data.CompanyCard.Score;
        var money = fieldManager.Data.CompanyCard.Money;

        _photonView.RPC("SyncParametersRpc", RpcTarget.All, userId, score, money);
    }

    [PunRPC]
    private void SyncParametersRpc(string userId, double score, double money)
    {
        if (PhotonNetworkUtils.IsMine(userId))
        {
            _myScore = score;
            _myMoney = money;
        }
        else
        {
            _otherScore = score;
            _otherMoney = money;
        }

        ShowDataScoreMoney();
        CheckForWin();
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

    private void PlayerFinished()
    {
        var userId = PhotonNetworkUtils.GetLocalUserId();
        _photonView.RPC("PlayerFinishedRpc", RpcTarget.All, userId);
    }

    [PunRPC]
    private void PlayerFinishedRpc(string userId)
    {
        UIUtils.ExecuteUserAction(userId, () => _isMyFinish = true, () => _isOtherFinish = true);
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
    }

    private void ShowDataScoreMoney()
    {
        uiManager.SetMyScoreValue(_myScore);
        uiManager.SetMyMoneyValue(_myMoney);

        uiManager.SetOtherScoreValue(_otherScore);
        uiManager.SetOtherMoneyValue(_otherMoney);
    }

    private void CheckForWin()
    {
        var userId = PhotonNetworkUtils.GetLocalUserId();

        if (CheckInvalidData(_myScore, _myMoney))
        {
            uiManager.ShowPlayerLose(userId);
        }
        else if (CheckInvalidData(_otherScore, _otherMoney))
        {
            uiManager.ShowPlayerWin(userId);
        }

        if (_isMyFinish && _isOtherFinish)
        {
            if (_myMoney > _otherMoney)
            {
                uiManager.ShowPlayerWin(userId);
            }
            else if (_myMoney < _otherMoney)
            {
                uiManager.ShowPlayerLose(userId);
            }
            else
            {
                uiManager.ShowDrawPanel();
            }
        }
    }

    private static bool CheckInvalidData(double score, double money)
    {
        return score < 0 || money < 0;
    }
}
