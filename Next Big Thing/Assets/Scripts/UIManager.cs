using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI logText;

    public void Log(string message)
    {
        Debug.Log(message);
        logText.text = message;
    }
}
