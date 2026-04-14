using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    protected static PlayerInventory instance;
    public static PlayerInventory Instance => instance;

    public bool hasKey = false;

    private void Awake()
    {
        instance = this;
    }
}
