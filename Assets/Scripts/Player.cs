using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float movSpeed;
    public float rotSpeed;
    public float jumpForce;
    public bool jump;
    float x, z;



    void Update()
    {
        x = Input.GetAxis("Horizontal") * Time.deltaTime * rotSpeed;
        z = Input.GetAxis("Vertical") * Time.deltaTime * movSpeed;

        if (Input.GetKeyDown(KeyCode.Space) && jump == false)
        {
            GetComponent<Rigidbody>().AddForce(0, jumpForce, 0, ForceMode.Impulse);
            jump = true;
        }

        

        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            jump = false;
        }
    }

}
