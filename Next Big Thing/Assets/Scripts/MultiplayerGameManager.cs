using UnityEngine;
using Card.Type;
using Tracking.CompanyCard;
using Tracking.FounderCard;
using Tracking.ImpactPoint;

public class MultiplayerGameManager : MonoBehaviour
{
    public CompanyCardRecognizer companyCardRecognizer;

    public FounderCardRecognizer founderCardRecognizer;

    public ImpactPointRecognizer impactPointRecognizer;

    private GameStorage _storage;

    private void Start()
    {
        _storage = gameObject.AddComponent<GameStorage>();

        companyCardRecognizer.AddListenerToCard(SetCompanyCard);
        founderCardRecognizer.AddListenerToCard(SetFounderCard);
        impactPointRecognizer.AddListenerToCard(SetImpactPoint);
    }

    private void SetCompanyCard(CompanyCardType type)
    {
        Debug.Log("Company card: " + type);
        var card = _storage.GetCompanyCardByType(type);
    }

    private void SetFounderCard(FounderCardType type)
    {
        Debug.Log("Founder card: " + type);
        var card = _storage.GetFounderCardByType(type);
        Debug.Log("card: " + card.Money);
    }

    private void SetImpactPoint(ImpactPointType type)
    {
        Debug.Log("Impact point: " + type);
        var card = _storage.GetImpactPointByType(type);
    }
}
