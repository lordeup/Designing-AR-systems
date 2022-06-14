using System.Collections.Generic;
using Card;
using Card.Type;
using Player;
using UnityEngine;
using Utils;

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
            if (_isInstantiateCreated || SharedUtils.IsNull(Data)) return;

            _playerManager = Data.Player.GetComponent<PlayerControlManager>();
            _isInstantiateCreated = true;
            UpdatePlayerPosition();
        }

        public void ExecuteMove(int count)
        {
            var currentIndex = System.Array.IndexOf(_cells, currentCell);
            var maxIndex = _cells.Length - 1;
            var amount = currentIndex + count;
            var newIndex = amount > maxIndex ? maxIndex : amount;

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
            Data.CompanyCard.Money += money;
        }

        public void CostCellCommand(double money)
        {
            Data.CompanyCard.Money -= money;
        }

        public void ImpactCellCommand(List<ImpactValue> values)
        {
            foreach (var card in values)
            {
                if (card.ImpactType == ImpactType.Score)
                {
                    Data.CompanyCard.Score += card.Value;
                }
                else if (card.ImpactType == ImpactType.Money)
                {
                    Data.CompanyCard.Money += card.Value;
                }
            }
        }
    }
}
