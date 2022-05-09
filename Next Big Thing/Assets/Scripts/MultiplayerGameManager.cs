using UnityEngine;
using Card.Type;
using Field;
using Player;
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
        if (_isInitialized || !companyCardRecognizer.isFound || !founderCardRecognizer.isFound) return;

        var player = playerUtils.InitializationPlayer();
        fieldManager.playerData = new PlayerData(player, companyCardRecognizer.Card, founderCardRecognizer.Card);
        _isInitialized = true;
    }

    public void MakeMove()
    {
        fieldManager.MakeMove();
        var cellManager = fieldManager.GetCurrentCellManager();

        uiManager.Log(cellManager.name + " " + cellManager.money + " " + cellManager.cellType);
    }

    private void SetCompanyCard(CompanyCardType type)
    {
        if (companyCardRecognizer.isFound) return;
        companyCardRecognizer.Card = _storage.GetCompanyCardByType(type);
        companyCardRecognizer.isFound = true;

        uiManager.Log("Card defined " + type);
    }

    private void SetFounderCard(FounderCardType type)
    {
        if (founderCardRecognizer.isFound) return;
        founderCardRecognizer.Card = _storage.GetFounderCardByType(type);
        founderCardRecognizer.isFound = true;

        uiManager.Log("Card defined " + type);
    }

    private void SetImpactPoint(ImpactPointType type)
    {
        var card = _storage.GetImpactPointByType(type);
        uiManager.Log("Card defined " + type);
    }
}
