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

        public void WaitTurnPanelActive(SharedUtils.DelegateMethod method)
        {
            WaitGameObjectActive(yourTurnPanel, method);
            WaitGameObjectActive(otherTurnPanel, method);
        }

        public void WaitActionPanelActive(SharedUtils.DelegateMethod method)
        {
            WaitGameObjectActive(actionPanel, method);
        }

        public void Log(string text)
        {
            UIUtils.SetPanelTextValue(logPanel, GameObjectTag.TextValue, text);
        }

        public void ShowPlayerWin(string userId)
        {
            UIUtils.ExecuteUserAction(userId, ShowWinningPanel, ShowLosingPanel);
        }

        public void ShowPlayerLose(string userId)
        {
            UIUtils.ExecuteUserAction(userId, ShowLosingPanel, ShowWinningPanel);
        }

        public void ShowScoreValue(string userId, double value)
        {
            UIUtils.ExecuteUserAction(userId,
                () => SetScoreValue(myScoreMoneyPanel, value),
                () => SetScoreValue(otherScoreMoneyPanel, value)
            );
        }

        public void ShowMoneyValue(string userId, double value)
        {
            UIUtils.ExecuteUserAction(userId,
                () => SetMoneyValue(myScoreMoneyPanel, value),
                () => SetMoneyValue(otherScoreMoneyPanel, value)
            );
        }

        public void SetActiveUI(bool state)
        {
            SetActiveMyScoreMoneyPanel(state);
            SetActiveOtherScoreMoneyPanel(state);
            SetActiveLogPanel(state);
        }

        public void ShowYourTurn()
        {
            SetActiveYourTurnPanel(true);
            WaitForSeconds(() => SetActiveYourTurnPanel(false), 3f);
        }

        public void ShowOtherTurnPanel()
        {
            SetActiveOtherTurnPanel(true);
            WaitForSeconds(() => SetActiveOtherTurnPanel(false), 3f);
        }

        public void ShowActionPanel(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return;
            }

            UIUtils.SetPanelTextValue(actionPanel, GameObjectTag.TextValue, text);

            SetActiveActionPanel(true);
            WaitForSeconds(() => SetActiveActionPanel(false), 3f);
        }

        private void ShowWinningPanel()
        {
            WaitForSeconds(() => UIUtils.SetActivePanel(winningPanel, true), 1f);
        }

        private void ShowLosingPanel()
        {
            WaitForSeconds(() => UIUtils.SetActivePanel(losingPanel, true), 1f);
        }

        private void SetActiveYourTurnPanel(bool state)
        {
            UIUtils.SetActivePanel(yourTurnPanel, state);
        }

        private void SetActiveOtherTurnPanel(bool state)
        {
            UIUtils.SetActivePanel(otherTurnPanel, state);
        }

        private void SetActiveLogPanel(bool state)
        {
            UIUtils.SetActivePanel(logPanel, state);
        }

        private void SetActiveMyScoreMoneyPanel(bool state)
        {
            UIUtils.SetActivePanel(myScoreMoneyPanel, state);
        }

        private void SetActiveOtherScoreMoneyPanel(bool state)
        {
            UIUtils.SetActivePanel(otherScoreMoneyPanel, state);
        }

        private void SetActiveActionPanel(bool state)
        {
            UIUtils.SetActivePanel(actionPanel, state);
        }

        private static void SetScoreValue(Component panel, double score)
        {
            UIUtils.SetPanelTextValue(panel, GameObjectTag.ScoreValue, SharedUtils.DoubleToString(score));
        }

        private static void SetMoneyValue(Component panel, double money)
        {
            UIUtils.SetPanelTextValue(panel, GameObjectTag.MoneyValue, SharedUtils.DoubleToString(money));
        }

        private void WaitForSeconds(SharedUtils.DelegateMethod method, float second)
        {
            StartCoroutine(CustomWaitUtils.WaitForSeconds(() => method?.Invoke(), second));
        }

        private void WaitGameObjectActive(Component component, SharedUtils.DelegateMethod method)
        {
            StartCoroutine(CustomWaitUtils.WaitWhile(
                () => UIUtils.GetActiveGameObject(component),
                () => method?.Invoke())
            );
        }
    }
}
