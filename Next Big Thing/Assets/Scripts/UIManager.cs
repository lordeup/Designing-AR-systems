using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private RectTransform winningPanel;
    [SerializeField] private RectTransform losingPanel;
    [SerializeField] private RectTransform yourTurnPanel;
    [SerializeField] private RectTransform otherTurnPanel;
    [SerializeField] private RectTransform logPanel;
    [SerializeField] private RectTransform moneyPanel;

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

    public void ShowWinningPanel()
    {
        StartCoroutine(CustomWaitUtils.WaitForSeconds(() => Utils.SetActivePanel(winningPanel, true), 1f));
    }

    public void ShowLosingPanel()
    {
        StartCoroutine(CustomWaitUtils.WaitForSeconds(() => Utils.SetActivePanel(losingPanel, true), 1f));
    }

    public void ShowYourTurn()
    {
        SetActiveYourTurnPanel(true);
        StartCoroutine(CustomWaitUtils.WaitForSeconds(() => SetActiveYourTurnPanel(false), 3f));
    }

    public void ShowOtherTurnPanel()
    {
        SetActiveOtherTurnPanel(true);
        StartCoroutine(CustomWaitUtils.WaitForSeconds(() => SetActiveOtherTurnPanel(false), 3f));
    }

    public void SetActiveLogPanel(bool state)
    {
        Utils.SetActivePanel(logPanel, state);
    }

    public void SetActiveMoneyPanel(bool state)
    {
        Utils.SetActivePanel(moneyPanel, state);
    }

    private void SetActiveYourTurnPanel(bool state)
    {
        Utils.SetActivePanel(yourTurnPanel, state);
    }

    private void SetActiveOtherTurnPanel(bool state)
    {
        Utils.SetActivePanel(otherTurnPanel, state);
    }
}
