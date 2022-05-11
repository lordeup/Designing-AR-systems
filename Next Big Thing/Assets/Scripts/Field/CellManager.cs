using UnityEngine;

namespace Field
{
    public class CellManager : MonoBehaviour
    {
        public CellType cellType;
        public new string name;
        public double money;

        private void Start()
        {
            var component = gameObject.GetComponent<Renderer>();
            InitializationCellColor(component);
        }

        private void InitializationCellColor(Renderer component)
        {
            var material = component.material;

            material.color = cellType switch
            {
                CellType.Profit => new Color32(133, 197, 70, 255),
                CellType.Cost => new Color32(255, 140, 34, 255),
                CellType.Impact => new Color32(35, 105, 167, 255),
                _ => material.color
            };
        }
    }
}
