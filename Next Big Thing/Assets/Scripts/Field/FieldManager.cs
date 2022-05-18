using Card;
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

        private void Start()
        {
            _cells = GameObject.FindGameObjectsWithTag(GameObjectTag.Cell.ToString());
        }

        private void Update()
        {
            if (_isInstantiateCreated || Utils.IsNull(Data)) return;

            _playerManager = Data.Player.GetComponent<PlayerControlManager>();
            _isInstantiateCreated = true;
            UpdatePlayerPosition();
        }

        public void ExecuteMove(int newIndex)
        {
            var currentIndex = System.Array.IndexOf(_cells, currentCell);
            if (currentIndex == _cells.Length - 1) return;

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

        public void ProfitCellCommand(double money)
        {
            Data.FounderCard.Money += money;
        }

        public void CostCellCommand(double money)
        {
            Data.FounderCard.Money -= money;
        }

        public void ImpactCellCommand(ImpactPoint card)
        {
            Data.FounderCard.Money += card.Point;
        }
    }
}
