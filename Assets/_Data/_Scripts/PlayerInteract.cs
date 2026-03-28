using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] protected Camera mainCam;
    [SerializeField] protected float range = 2f;
    [SerializeField] protected Door door;

    void Update()
    {
        this.Interact();
    }

    protected void Interact()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Ray ray = new(Camera.main.transform.position, Camera.main.transform.forward);

            if (Physics.Raycast(ray, out RaycastHit hit, this.range))
            {
                door.Open();
            }

            Debug.DrawRay(ray.origin, ray.direction * range, Color.red, range);
        }
    }
}
