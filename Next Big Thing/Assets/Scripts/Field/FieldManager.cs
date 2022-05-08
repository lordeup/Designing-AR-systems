using Card;
using Player;
using UnityEngine;

namespace Field
{
    public class FieldManager : MonoBehaviour
    {
        public CompanyCard CompanyCard { get; set; }
        public FounderCard FounderCard { get; set; }
        public PlayerControlManager PlayerManager { get; set; }

        [SerializeField] private GameObject currentCell;
        private GameObject[] _cells;

        // TODO
        private bool _isFirst;

        private void Start()
        {
            _cells = GameObject.FindGameObjectsWithTag(GameObjectTag.Cell.ToString());
        }

        private void Update()
        {
            if (!_isFirst && !Utils.IsNull(PlayerManager))
            {
                UpdatePlayerPosition();
                _isFirst = true;
            }
        }

        public void MakeMove()
        {
            var length = _cells.Length;
            var currentIndex = System.Array.IndexOf(_cells, currentCell);
            var newIndex = currentIndex + 1;

            currentCell = _cells[newIndex];
            UpdatePlayerPosition();
        }

        private void UpdatePlayerPosition()
        {
            var position = currentCell.transform.position + PlayerUtils.InitPlayerPosition;
            PlayerManager.SetPlayerPosition(position);
        }
    }
}
