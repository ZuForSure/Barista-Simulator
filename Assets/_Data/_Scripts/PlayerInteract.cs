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
            Ray ray = new(Camera.main.transform.position, Camera.main.transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, this.range))
            {
                Debug.Log("Hit: " + hit.collider.gameObject.name);
            }

            Debug.DrawRay(ray.origin, ray.direction * range, Color.red, range);
        }
    }
}
