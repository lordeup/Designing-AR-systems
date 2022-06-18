using UnityEngine;
using Utils;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private RectTransform winningPanel;
        [SerializeField] private RectTransform losingPanel;
        [SerializeField] private RectTransform drawPanel;

        [SerializeField] private RectTransform yourTurnPanel;
        [SerializeField] private RectTransform otherTurnPanel;

        [SerializeField] private RectTransform logPanel;
        [SerializeField] private RectTransform actionPanel;

        [SerializeField] private RectTransform myScoreMoneyPanel;
        [SerializeField] private RectTransform otherScoreMoneyPanel;

        [SerializeField] private SelectionImpactPointManager impactPointManager;

        private bool _isActivePanel;

        public SelectionImpactPointManager GetImpactPointManager()
        {
            return impactPointManager;
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

        public void ShowDrawPanel()
        {
            SetActiveDrawPanel(true);
        }

        public void SetActiveUI(bool state)
        {
            SetActiveMyScoreMoneyPanel(state);
            SetActiveOtherScoreMoneyPanel(state);
            SetActiveLogPanel(state);
        }

        public void ShowYourTurn()
        {
            if (_isActivePanel) return;

            SetActiveYourTurnPanel(true);
            WaitForSeconds(() => SetActiveYourTurnPanel(false), 3f);
        }

        public void ShowOtherTurnPanel()
        {
            if (_isActivePanel) return;

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
            SetActiveWinningPanel(true);
        }

        private void ShowLosingPanel()
        {
            SetActiveLosingPanel(true);
        }

        public void SetMyScoreValue(double value)
        {
            SetScoreValue(myScoreMoneyPanel, value);
        }

        public void SetOtherScoreValue(double value)
        {
            SetScoreValue(otherScoreMoneyPanel, value);
        }

        public void SetMyMoneyValue(double value)
        {
            SetMoneyValue(myScoreMoneyPanel, value);
        }

        public void SetOtherMoneyValue(double value)
        {
            SetMoneyValue(otherScoreMoneyPanel, value);
        }

        private void SetActiveWinningPanel(bool state)
        {
            UIUtils.SetActivePanel(winningPanel, state);
            _isActivePanel = state;
        }

        private void SetActiveLosingPanel(bool state)
        {
            UIUtils.SetActivePanel(losingPanel, state);
            _isActivePanel = state;
        }

        private void SetActiveDrawPanel(bool state)
        {
            UIUtils.SetActivePanel(drawPanel, state);
            _isActivePanel = state;
        }

        private void SetActiveYourTurnPanel(bool state)
        {
            UIUtils.SetActivePanel(yourTurnPanel, state);
            _isActivePanel = state;
        }

        private void SetActiveOtherTurnPanel(bool state)
        {
            UIUtils.SetActivePanel(otherTurnPanel, state);
            _isActivePanel = state;
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
            _isActivePanel = state;
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
