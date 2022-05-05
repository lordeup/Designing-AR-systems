using UnityEngine;

namespace Field
{
    public class FieldManager : MonoBehaviour
    {
        private GameObject[] _cells;
        private Renderer _selectedCell;

        private void Start()
        {
            _cells = GameObject.FindGameObjectsWithTag(GameObjectTag.Cell.ToString());
        }

        private void Update()
        {
        }
    }
}
