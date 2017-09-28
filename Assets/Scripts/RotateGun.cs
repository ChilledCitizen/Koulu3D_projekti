using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateGun : MonoBehaviour {

    public float curAngle;
    public bool rotating;
    public float rotationDur = 5f;
    public float rotateCount;
    private float _xAngle;
    public float xAngle
    {
        get { return _xAngle; }
        set
        {
            _xAngle = value;
            Debug.Log("Chage muzzle dir " + _xAngle);

            rotating = true;
            rotateCount = 0;


        }
    }

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        rotateCount += Time.deltaTime;

        if (rotateCount >= rotationDur && rotating)
        {
            rotating = false;
        }
        else
        {
            curAngle = Mathf.LerpAngle(transform.localRotation.eulerAngles.x, xAngle , rotateCount);

            transform.localEulerAngles = new Vector3(curAngle, 0, 0);
        }

    }
}
