using UnityEngine;

public class MultiplayerGameManager : MonoBehaviour
{
    private GameStorage _storage;

    public CardRecognizer cardRecognizer;

    private void Start()
    {
        _storage = gameObject.AddComponent<GameStorage>();

        cardRecognizer.AddListenerToCard(SetCards);
    }

    private static void SetCards(GameCard card)
    {
        Debug.Log("_____________________________________________________ " + card);
    }
}
