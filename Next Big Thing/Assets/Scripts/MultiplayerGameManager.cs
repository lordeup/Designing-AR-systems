using System;
using UnityEngine;
using Card.Type;
using Tracking.CompanyCard;
using Tracking.FounderCard;
using Tracking.ImpactPoint;
using Photon.Pun;

public class MultiplayerGameManager : MonoBehaviour
{
    [SerializeField] private Transform prefabPlayer1;
    [SerializeField] private Transform prefabPlayer2;
    [SerializeField] private CompanyCardRecognizer companyCardRecognizer;
    [SerializeField] private FounderCardRecognizer founderCardRecognizer;
    [SerializeField] private ImpactPointRecognizer impactPointRecognizer;

    private GameStorage _storage;
    private PlayerType _playerType;

    private void Start()
    {
        _storage = gameObject.AddComponent<GameStorage>();
        _playerType = PlayerType.Player1;

        companyCardRecognizer.AddListenerToCard(SetCompanyCard);
        founderCardRecognizer.AddListenerToCard(SetFounderCard);
        impactPointRecognizer.AddListenerToCard(SetImpactPoint);

        InitializationPlayers();
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

    private void InitializationPlayers()
    {
        var player = GetPlayer(_playerType);
        if (SceneController.IsNull(player)) return;

        var playerPosition = new Vector3(0, 0, 0.45f);
        PhotonNetwork.Instantiate(player.name, playerPosition, Quaternion.identity);
    }

    private Transform GetPlayer(PlayerType playerType)
    {
        return playerType switch
        {
            PlayerType.Player1 => prefabPlayer1,
            PlayerType.Player2 => prefabPlayer2,
            _ => throw new ArgumentOutOfRangeException(nameof(playerType), playerType, null)
        };
    }
}
