using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] protected Camera mainCam;
    [SerializeField] protected float range = 2f;

    void Update()
    {
        this.Interact();
    }

    protected void Interact()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out RaycastHit hit, this.range))
            {
                if (hit.collider.GetComponentInParent<IInteract>() is IInteract interactObj)
                {
                    interactObj.Interact();
                }
            }
        }
    }
}
