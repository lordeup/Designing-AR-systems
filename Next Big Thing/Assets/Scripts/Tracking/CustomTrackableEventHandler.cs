namespace Tracking
{
    public class CustomTrackableEventHandler : DefaultObserverEventHandler
    {
        private MultiplayerGameManager _gameManager;

        private void Awake()
        {
            _gameManager = FindObjectOfType<MultiplayerGameManager>();
        }

        protected override void OnTrackingFound()
        {
            _gameManager.enabled = true;
            base.OnTrackingFound();
        }
    }
}
