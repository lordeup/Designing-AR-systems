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
    private GameObject _instantiate;

    private void Start()
    {
        _storage = gameObject.AddComponent<GameStorage>();

        companyCardRecognizer.AddListenerToCard(SetCompanyCard);
        founderCardRecognizer.AddListenerToCard(SetFounderCard);
        impactPointRecognizer.AddListenerToCard(SetImpactPoint);
        _instantiate = playerUtils.InitializationPlayer();

        if (!Utils.IsNull(_instantiate))
        {
            fieldManager.PlayerManager = _instantiate.GetComponent<PlayerControlManager>();
        }
    }

    public void MakeMove()
    {
        fieldManager.MakeMove();
    }

    private void SetCompanyCard(CompanyCardType type)
    {
        if (fieldManager.CompanyCard != null) return;
        fieldManager.CompanyCard = _storage.GetCompanyCardByType(type);
        uiManager.Log("Card defined " + type);
    }

    private void SetFounderCard(FounderCardType type)
    {
        if (fieldManager.FounderCard != null) return;
        fieldManager.FounderCard = _storage.GetFounderCardByType(type);
        uiManager.Log("Card defined " + type);
    }

    private void SetImpactPoint(ImpactPointType type)
    {
        var card = _storage.GetImpactPointByType(type);
        uiManager.Log("Card defined " + type);
    }
}
