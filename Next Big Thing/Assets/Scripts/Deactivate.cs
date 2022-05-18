using UnityEngine;

public class Deactivate : MonoBehaviour
{
    public bool isActive = true;

    private void Update()
    {
        if (!isActive)
        {
            gameObject.SetActive(false);
        }
    }
}
