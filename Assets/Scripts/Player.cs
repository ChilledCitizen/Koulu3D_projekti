using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float movSpeed;
    public float rotSpeed;
    public float jumpForce;
    public bool jump;
    public float lookSpeed = 1;
    float x, z;
    float mouseInputX;
    public Camera myCam;
    Vector3 look;
    PhotonView photonView;

    void Start()
    {
        photonView = GetComponent<PhotonView>();

        if (photonView.isMine)
        {
            myCam.enabled = true;
        }



    }
    void Update()
    {
        if (photonView.isMine)
        {

            x = Input.GetAxis("Horizontal") * Time.deltaTime * rotSpeed;
            z = Input.GetAxis("Vertical") * Time.deltaTime * movSpeed;

            if (Input.GetKeyDown(KeyCode.Space) && jump == false)
            {
                GetComponent<Rigidbody>().AddForce(0, jumpForce, 0, ForceMode.Impulse);
                jump = true;
            }

            mouseInputX = Input.GetAxis("Mouse X") * lookSpeed;

            look = new Vector3(0, mouseInputX, 0);



            transform.Rotate(look);
            transform.Translate(x, 0, z);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            jump = false;
        }
    }

   

}
