using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooty : MonoBehaviour
{

    float mouseY, shootSpeed;
    public float lookSpeed, shotMod = 1;
    public GameObject bulletPrefab;
    public Transform bulletSpawn;

    // Use this for initialization
    void Start () {

       
		
	}
	
	// Update is called once per frame
	void Update () {

        mouseY = Input.GetAxis("Mouse Y") * -lookSpeed;

        transform.Rotate(mouseY, 0, 0);

        if (Input.GetMouseButton(0))
        {
            shootSpeed += shotMod;
            Debug.Log("Power = " + shootSpeed);
        }

        if (Input.GetMouseButtonUp(0))
        {
            Fire();
        }	
	}

    void Fire()
    {
        // Create the Bullet from the Bullet Prefab
        GameObject bullet = Instantiate(
            bulletPrefab,
            bulletSpawn.position,
            bulletSpawn.rotation);

        // Add velocity to the bullet
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * shootSpeed;

        // Destroy the bullet after 2 seconds
        Destroy(bullet, 2.0f);
        shootSpeed = 0;
    }
}
