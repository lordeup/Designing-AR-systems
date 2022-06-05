using System.Globalization;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private RectTransform winningPanel;
    [SerializeField] private RectTransform losingPanel;

    [SerializeField] private RectTransform yourTurnPanel;
    [SerializeField] private RectTransform otherTurnPanel;

    [SerializeField] private RectTransform logPanel;
    [SerializeField] private RectTransform actionPanel;
    [SerializeField] private RectTransform choicePanel;

    [SerializeField] private RectTransform myScoreMoneyPanel;
    [SerializeField] private RectTransform otherScoreMoneyPanel;

    public void Log(string text)
    {
        // Utils.SetPanelTextValue(logPanel, text);
    }

    public void SetActionValue(string text)
    {
        // Utils.SetPanelTextValue(actionPanel, text);
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

    public void ShowActionPanel()
    {
        SetActiveActionPanel(true);
        StartCoroutine(CustomWaitUtils.WaitForSeconds(() => SetActiveActionPanel(false), 3f));
    }

    public void SetMyScoreValue(int score)
    {
        SetScoreValue(myScoreMoneyPanel, score);
    }

    public void SetMyMoneyValue(double money)
    {
        SetMoneyValue(myScoreMoneyPanel, money);
    }

    public void SetOtherScoreValue(int score)
    {
        SetScoreValue(otherScoreMoneyPanel, score);
    }

    public void SetOtherMoneyValue(double money)
    {
        SetMoneyValue(otherScoreMoneyPanel, money);
    }

    private void SetActiveYourTurnPanel(bool state)
    {
        Utils.SetActivePanel(yourTurnPanel, state);
    }

    private void SetActiveOtherTurnPanel(bool state)
    {
        Utils.SetActivePanel(otherTurnPanel, state);
    }

    public void SetActiveLogPanel(bool state)
    {
        Utils.SetActivePanel(logPanel, state);
    }

    public void SetActiveChoicePanel(bool state)
    {
        Utils.SetActivePanel(choicePanel, state);
    }

    public void SetActiveMyScoreMoneyPanel(bool state)
    {
        Utils.SetActivePanel(myScoreMoneyPanel, state);
    }

    public void SetActiveOtherScoreMoneyPanel(bool state)
    {
        Utils.SetActivePanel(otherScoreMoneyPanel, state);
    }

    private void SetActiveActionPanel(bool state)
    {
        Utils.SetActivePanel(actionPanel, state);
    }

    private static void SetScoreValue(Component panel, int score)
    {
        Utils.SetPanelTextValue(panel, GameObjectTag.Score, score.ToString());
    }

    private static void SetMoneyValue(Component panel, double money)
    {
        Utils.SetPanelTextValue(panel, GameObjectTag.Money, money.ToString(CultureInfo.InvariantCulture));
    }
}
