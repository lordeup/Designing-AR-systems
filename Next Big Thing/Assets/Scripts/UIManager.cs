using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI logText;

    public void Log(string message)
    {
        Debug.Log(message);
        logText.text += "\n" + message;
    }
}
