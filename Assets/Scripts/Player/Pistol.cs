using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Pistol : MonoBehaviour
{
    private Camera cam;
    public PlayerMovement pm;

    public GameObject projectile;
    public Transform trackPoint;

    public float cooldown = 1f;
    private float timer = 1f;

    [SerializeField]
    private SoundSystem ss;

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
        angle *= Time.timeScale;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
       
        //shooting
        if(Input.GetButton("Fire1") && timer <= 0)
        {
            Shoot();
        }

        timer -= Time.deltaTime;
    }

    void Shoot()
    {
        ss.clauses[2] = true;
        Instantiate(projectile, trackPoint.position, transform.rotation);
        timer = cooldown;
    }
}
