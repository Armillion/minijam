using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PistolRotate : MonoBehaviour
{
    private Camera cam;

    public GameObject projectile;
    public Transform trackPoint;

    private void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        //rotation
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 5.23f;

        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        //shooting
        if(Input.GetButton("Fire"))
        {

        }
    }

    void Shoot()
    {
        Instantiate(projectile, transform.position, transform.rotation);
    }
}
