using Player;
using UnityEngine;

namespace Field
{
    public class FieldManager : MonoBehaviour
    {
        [SerializeField] private GameObject currentCell;

        public PlayerData playerData;

        private PlayerControlManager _playerManager;
        private GameObject[] _cells;
        private bool _isInstantiateCreated;

        private void Start()
        {
            _cells = GameObject.FindGameObjectsWithTag(GameObjectTag.Cell.ToString());
        }

        private void Update()
        {
            if (_isInstantiateCreated || Utils.IsNull(playerData)) return;

            _playerManager = playerData.Player.GetComponent<PlayerControlManager>();
            _isInstantiateCreated = true;
            UpdatePlayerPosition();
        }

        public void MakeMove()
        {
            var length = _cells.Length;
            var currentIndex = System.Array.IndexOf(_cells, currentCell);
            var newIndex = currentIndex + 1;

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
    }
}
