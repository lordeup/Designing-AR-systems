using System.Globalization;
using UnityEngine;
using Card.Type;
using Field;
using Player;
using Room;
using Tracking.CompanyCard;
using Tracking.FounderCard;
using Tracking.ImpactPoint;

public class MultiplayerGameManager : MonoBehaviour
{
    [SerializeField] private CompanyCardRecognizer companyCardRecognizer;
    [SerializeField] private FounderCardRecognizer founderCardRecognizer;
    [SerializeField] private ImpactPointRecognizer impactPointRecognizer;

    [SerializeField] private UIManager uiManager;
    [SerializeField] private FieldManager fieldManager;
    [SerializeField] private PlayerUtils playerUtils;

    private GameStorage _storage;
    private bool _isInitialized;

    private void Start()
    {
        _storage = gameObject.AddComponent<GameStorage>();

        companyCardRecognizer.AddListenerToCard(SetCompanyCard);
        founderCardRecognizer.AddListenerToCard(SetFounderCard);
        impactPointRecognizer.AddListenerToCard(SetImpactPoint);
    }

    private void Update()
    {
        if (_isInitialized || !founderCardRecognizer.isFound) return;

        var player = playerUtils.InitializationPlayer();
        fieldManager.Data = new PlayerData(player, founderCardRecognizer.Card);
        SetActiveUI(true);
        SetMoneyValue();
        _isInitialized = true;
    }

    public void MakeMove()
    {
        fieldManager.MakeMove();
        var cellManager = fieldManager.GetCurrentCellManager();
        MovementLog(cellManager);
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
            StartCoroutine(SceneController.WaitMethod(SetActiveWinningPanel, 1f));
        }
        else if (cellType == CellType.Profit)
        {
            fieldManager.ProfitCellCommand(money);
        }
        else if (cellType == CellType.Cost)
        {
            fieldManager.CostCellCommand(money);
        }
        else if (cellType == CellType.Impact)
        {
        }
    }

    private void SetCompanyCard(CompanyCardType type)
    {
        if (companyCardRecognizer.isFound) return;
        companyCardRecognizer.Card = _storage.GetCompanyCardByType(type);
        companyCardRecognizer.isFound = true;

        uiManager.Log("Карта компании: " + type);
    }

    private void SetFounderCard(FounderCardType type)
    {
        if (founderCardRecognizer.isFound) return;
        founderCardRecognizer.Card = _storage.GetFounderCardByType(type);
        founderCardRecognizer.isFound = true;

        uiManager.Log("Карта основателя: " + type);
    }

    private void SetImpactPoint(ImpactPointType type)
    {
        var card = _storage.GetImpactPointByType(type);
        uiManager.Log("Карта влияния: " + type);
    }

    private void MovementLog(CellManager cellManager)
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

    private void SetMoneyValue()
    {
        var money = fieldManager.Data.FounderCard.Money;
        uiManager.SetMoneyValue(money.ToString(CultureInfo.InvariantCulture));
    }

    private void SetActiveWinningPanel()
    {
        uiManager.SetActiveWinningPanel(true);
    }

    private void SetActiveUI(bool state)
    {
        uiManager.SetActiveMoneyPanel(state);
        uiManager.SetActiveLogPanel(state);
        uiManager.SetActiveMakeMoveButton(state);
    }
}
