using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private RectTransform winningPanel;
    [SerializeField] private RectTransform logPanel;
    [SerializeField] private RectTransform moneyPanel;

    [SerializeField] private GameObject makeMoveButton;

    [SerializeField] private TextMeshProUGUI logText;
    [SerializeField] private TextMeshProUGUI moneyValueText;

    public void Log(string message)
    {
        logText.text = message;
    }

    public void SetMoneyValue(string message)
    {
        moneyValueText.text = message;
    }

    public void SetActiveWinningPanel(bool state)
    {
        winningPanel.gameObject.SetActive(state);
    }

    public void SetActiveLogPanel(bool state)
    {
        logPanel.gameObject.SetActive(state);
    }

    public void SetActiveMoneyPanel(bool state)
    {
        moneyPanel.gameObject.SetActive(state);
    }

    public void SetActiveMakeMoveButton(bool state)
    {
        makeMoveButton.SetActive(state);
    }
}
