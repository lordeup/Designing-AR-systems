using System.Globalization;
using UnityEngine;
using Utils;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private RectTransform winningPanel;
        [SerializeField] private RectTransform losingPanel;

        [SerializeField] private RectTransform yourTurnPanel;
        [SerializeField] private RectTransform otherTurnPanel;

        [SerializeField] private RectTransform logPanel;
        [SerializeField] private RectTransform actionPanel;

        [SerializeField] private RectTransform myScoreMoneyPanel;
        [SerializeField] private RectTransform otherScoreMoneyPanel;

        [SerializeField] private SelectionImpactPointManager impactPointManager;

        public SelectionImpactPointManager GetImpactPointManager()
        {
            return impactPointManager;
        }

        public void Log(string text)
        {
            // UIUtils.SetPanelTextValue(logPanel, text);
        }

        public void SetActionValue(string text)
        {
            UIUtils.SetPanelTextValue(actionPanel, GameObjectTag.TextValue, text);
        }

        public void ShowWinningPanel()
        {
            StartCoroutine(CustomWaitUtils.WaitForSeconds(() => UIUtils.SetActivePanel(winningPanel, true), 1f));
        }

        public void ShowLosingPanel()
        {
            StartCoroutine(CustomWaitUtils.WaitForSeconds(() => UIUtils.SetActivePanel(losingPanel, true), 1f));
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
            UIUtils.SetActivePanel(yourTurnPanel, state);
        }

        private void SetActiveOtherTurnPanel(bool state)
        {
            UIUtils.SetActivePanel(otherTurnPanel, state);
        }

        public void SetActiveLogPanel(bool state)
        {
            UIUtils.SetActivePanel(logPanel, state);
        }

        public void SetActiveMyScoreMoneyPanel(bool state)
        {
            UIUtils.SetActivePanel(myScoreMoneyPanel, state);
        }

        public void SetActiveOtherScoreMoneyPanel(bool state)
        {
            UIUtils.SetActivePanel(otherScoreMoneyPanel, state);
        }

        private void SetActiveActionPanel(bool state)
        {
            UIUtils.SetActivePanel(actionPanel, state);
        }

        private static void SetScoreValue(Component panel, int score)
        {
            UIUtils.SetPanelTextValue(panel, GameObjectTag.ScoreValue, score.ToString());
        }

        private static void SetMoneyValue(Component panel, double money)
        {
            UIUtils.SetPanelTextValue(panel, GameObjectTag.MoneyValue, money.ToString(CultureInfo.InvariantCulture));
        }
    }
}
