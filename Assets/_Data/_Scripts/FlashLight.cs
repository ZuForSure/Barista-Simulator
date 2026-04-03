using UnityEngine;

public class FlashLight : MonoBehaviour
{
    [SerializeField] protected Light spotLight;
    protected bool isOn;

    private void Awake()
    {
        spotLight = GetComponentInChildren<Light>();
        isOn = false;
        this.spotLight.gameObject.SetActive(isOn);
    }

    private void Update()
    {
        this.ToggleFlashlight();
    }

    private void ToggleFlashlight()
    {
        if (!InputManager.Instance.IsFlashLight) return;

        this.isOn = !isOn;
        this.spotLight.gameObject.SetActive(isOn);
    }
}
