using UnityEngine;
using UnityEngine.UI;

public class BackgroundApplier : MonoBehaviour
{
    public Image backgroundImage;

    void Start()
    {
        if (_Gamemanager.Instance == null || _Gamemanager.Instance.selectedBackground == null)
        {
            Debug.LogWarning("Kein Background gesetzt!");
            return;
        }

        backgroundImage.sprite = _Gamemanager.Instance.selectedBackground;
        backgroundImage.preserveAspect = true;
    }
}
