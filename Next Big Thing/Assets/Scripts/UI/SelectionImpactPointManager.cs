using System.Collections.Generic;
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
        private List<Impact> _impacts;
        private List<ImpactValue> _impactValues;

        private void Start()
        {
            _impactPointStorage = gameObject.AddComponent<ImpactPointStorage>();
        }

        public List<ImpactValue> GetSelectedImpactValues()
        {
            return _impactValues;
        }

        public void ClearImpactItem()
        {
            _impactValues.Clear();
        }

        public void SetPanelValues()
        {
            var impactPoint = _impactPointStorage.GetRandomImpactPoint();
            _impacts = _impactPointStorage.GetImpactsByType(impactPoint.PointType);

            if (_impacts.Count < 2) return;

            UIUtils.SetPanelTextValue(panel, GameObjectTag.DescriptionSelectionTextValue, impactPoint.Description);
            UIUtils.SetPanelTextValue(panel, GameObjectTag.FirstSelectionTextValue, _impacts.First().Description);
            UIUtils.SetPanelTextValue(panel, GameObjectTag.SecondSelectionTextValue, _impacts.Last().Description);
        }

        public void SelectFirstImpactItem()
        {
            _impactValues = _impacts.First().ImpactValues;
        }

        public void SelectSecondImpactItem()
        {
            _impactValues = _impacts.Last().ImpactValues;
        }

        public void SetActivePanel(bool state)
        {
            UIUtils.SetActivePanel(panel, state);
        }
    }
}
