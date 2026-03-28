using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
                if (hit.collider.GetComponentInParent<Door>() is Door door)
                {
                    door.Open();
                    Debug.Log(door.transform.name);
                }
            }

        }
    }
}
