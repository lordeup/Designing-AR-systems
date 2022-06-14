using TMPro;
using UnityEngine;

namespace Utils
{
    public static class UIUtils
    {
        public static void SetActivePanel(Component panel, bool state)
        {
            panel.gameObject.SetActive(state);
        }
        
        public static bool GetActiveGameObject(Component component)
        {
            return component.gameObject.activeSelf;
        }

        public static void SetPanelTextValue(Component panel, GameObjectTag tag, string text)
        {
            var components = panel.GetComponentsInChildren<TextMeshProUGUI>();

            foreach (var component in components)
            {
                if (component.CompareTag(tag.ToString()))
                {
                    component.text = text;
                }
            }
        }
    }
}
