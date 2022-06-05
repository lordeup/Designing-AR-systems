using System.Collections.Generic;
using TMPro;
using UnityEngine;

public static class Utils
{
    public const bool IsMobile = true;

    public static bool IsNull(object obj)
    {
        return obj == null;
    }

    public static void SetActivePanel(Component panel, bool state)
    {
        panel.gameObject.SetActive(state);
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

    public static Dictionary<TKey, TValue> SetDictionaryValue<TKey, TValue>(Dictionary<TKey, TValue> objects, TKey key,
        TValue value)
    {
        var dictionary = new Dictionary<TKey, TValue>(objects);

        if (dictionary.ContainsKey(key))
        {
            dictionary[key] = value;
        }
        else
        {
            dictionary.Add(key, value);
        }

        return dictionary;
    }
}
