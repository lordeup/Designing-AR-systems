using UnityEngine;

namespace Field
{
    public class CellManager : MonoBehaviour
    {
        [SerializeField] private CellType cellType;
        [SerializeField] private new string name;
        [SerializeField] private double money;

        private void Start()
        {
            var component = gameObject.GetComponent<Renderer>();
            InitializationCellColor(component);
        }

        private void Update()
        {
        }

        private void InitializationCellColor(Renderer component)
        {
            var material = component.material;

            material.color = cellType switch
            {
                CellType.Start => new Color32(57, 127, 38, 255),
                CellType.Finish => new Color32(59, 152, 24, 255),
                CellType.Profit => new Color32(133, 197, 70, 255),
                CellType.Cost => new Color32(255, 140, 34, 255),
                CellType.Impact => new Color32(35, 105, 167, 255),
                _ => material.color
            };
        }
    }
}
