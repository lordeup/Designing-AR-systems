using Player;
using UnityEngine;

namespace Field
{
    public class FieldManager : MonoBehaviour
    {
        [SerializeField] private GameObject currentCell;

        public PlayerData Data;

        private PlayerControlManager _playerManager;
        private GameObject[] _cells;
        private bool _isInstantiateCreated;
        private System.Random _random;

        private void Start()
        {
            _cells = GameObject.FindGameObjectsWithTag(GameObjectTag.Cell.ToString());
            _random = new System.Random();
        }

        private void Update()
        {
            if (_isInstantiateCreated || Utils.IsNull(Data)) return;

            _playerManager = Data.Player.GetComponent<PlayerControlManager>();
            _isInstantiateCreated = true;
            UpdatePlayerPosition();
        }

        public void MakeMove()
        {
            var currentIndex = System.Array.IndexOf(_cells, currentCell);
            if (currentIndex == _cells.Length - 1) return;

            var newIndex = GetNewCellIndex(currentIndex);
            currentCell = _cells[newIndex];
            UpdatePlayerPosition();
        }

        public CellManager GetCurrentCellManager()
        {
            return currentCell.GetComponent<CellManager>();
        }

        private void UpdatePlayerPosition()
        {
            var position = currentCell.transform.position + PlayerUtils.InitPlayerPosition;
            _playerManager.SetPlayerPosition(position);
        }

        private int GetNewCellIndex(int currentIndex)
        {
            int newIndex;
            while (true)
            {
                var next = _random.Next(1, 7);
                newIndex = currentIndex + next;
                if (newIndex < _cells.Length)
                {
                    break;
                }
            }

            return newIndex;
        }

        public void ProfitCellCommand(double money)
        {
            Data.FounderCard.Money += money;
        }

        public void CostCellCommand(double money)
        {
            Data.FounderCard.Money -= money;
        }
    }
}
