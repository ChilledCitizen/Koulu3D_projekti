using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooty : MonoBehaviour
{

    float mouseY, shootSpeed;
    public float lookSpeed, shotMod = 1;
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    PhotonView photonView;
    public GameObject player;
    void Start()
    {

        photonView = player.GetComponent<PhotonView>();


    }
	
	// Update is called once per frame
	void Update () {

        if (photonView.isMine)
        {

            mouseY = Input.GetAxis("Mouse Y") * -lookSpeed;


            transform.Rotate(Mathf.Clamp(mouseY, -30, 20), 0, 0);

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
	}

    void Fire()
    {
        // Create the Bullet from the Bullet Prefab
        /* GameObject bullet = PhotonView.Instantiate(
             bulletPrefab,
             bulletSpawn.position,
             bulletSpawn.rotation);
             */

        

        GameObject bullet = PhotonNetwork.Instantiate("Ball", bulletSpawn.position, bulletSpawn.rotation, 0);

        photonView.RPC("ShootBall", PhotonTargets.Others, bulletSpawn.position, bulletSpawn.rotation, shootSpeed);
        // Add velocity to the bullet
        bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * shootSpeed, ForceMode.Impulse);

        // Destroy the bullet after 2 seconds
       // PhotonNetwork.Destroy(bullet, 2.0f);
        shootSpeed = 0;
    }
}
