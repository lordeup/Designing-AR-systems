using System.Linq;
using Card;
using Storage;
using UnityEngine;
using Utils;

namespace UI
{
    public class SelectionImpactPointManager : MonoBehaviour
    {
        [SerializeField] private RectTransform panel;

        private ImpactPointStorage _impactPointStorage;
        private ImpactPoint _impactPoint;
        private Impact _impact;

        private void Start()
        {
            _impactPointStorage = gameObject.AddComponent<ImpactPointStorage>();
        }

        public Impact GetSelectedImpactItem()
        {
            return _impact;
        }

        public void SetPanelValues()
        {
            _impactPoint = _impactPointStorage.GetRandomImpactPoint();
            var impacts = _impactPoint.Impacts;

            if (impacts.Count < 2) return;

            UIUtils.SetPanelTextValue(panel, GameObjectTag.DescriptionSelectionTextValue, _impactPoint.Description);
            UIUtils.SetPanelTextValue(panel, GameObjectTag.FirstSelectionTextValue, impacts.First().Description);
            UIUtils.SetPanelTextValue(panel, GameObjectTag.SecondSelectionTextValue, impacts.Last().Description);
        }

        public void SelectFirstImpactItem()
        {
            _impact = _impactPoint.Impacts.First();
        }

        public void SelectSecondImpactItem()
        {
            _impact = _impactPoint.Impacts.Last();
        }

        public void SetActivePanel(bool state)
        {
            UIUtils.SetActivePanel(panel, state);
        }
    }
}
