using System;
using UnityEngine;
using Card.Type;
using Field;
using Player;
using Room;
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
    [SerializeField] private UIManager uiManager;
    [SerializeField] private FieldManager fieldManager;

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
        var card = _storage.GetCompanyCardByType(type);
        uiManager.Log(type.ToString());
    }

    private void SetFounderCard(FounderCardType type)
    {
        Debug.Log("Founder card: " + type);
        var card = _storage.GetFounderCardByType(type);
        uiManager.Log(type.ToString());
    }

    private void SetImpactPoint(ImpactPointType type)
    {
        var card = _storage.GetImpactPointByType(type);
        uiManager.Log(type.ToString());
    }

    private void InitializationPlayers()
    {
        var player = GetPlayer(_playerType);
        if (SceneController.IsNull(player)) return;

        var playerPosition = new Vector3(0, 0, 0.45f);
        // Instantiate(player, playerPosition, Quaternion.identity);
        // PhotonNetwork.Instantiate(player.name, playerPosition, Quaternion.identity);
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
