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
}
