using UnityEngine;

public class GameController : MonoBehaviour
{
    private GameStorage _storage;

    private void Start()
    {
        _storage = gameObject.AddComponent<GameStorage>();
    }

    private void Update()
    {
    }
}
