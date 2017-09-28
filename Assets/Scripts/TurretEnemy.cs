using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretEnemy : MonoBehaviour
{

    public GameObject ammo, ammoSpawn, gunRotator, targetArea;
    public float force;
    public Vector3 gravity;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.LookAt(targetArea.transform.position);
    }


    public void Shoot()
    {
        StartCoroutine(ShootBalls());
    }

    IEnumerator ShootBalls()
    {

        Vector3[] direction = HitTargetBySpeed(ammoSpawn.transform.position, targetArea.transform.position, Physics.gravity, force);

        gunRotator.GetComponent<RotateGun>().xAngle = Mathf.Atan(direction[1].y / direction[1].z)*Mathf.Rad2Deg;

        yield return new WaitUntil(() => gunRotator.GetComponent<RotateGun>().rotating == false);

        GameObject enemyAmmo = Instantiate(ammo, ammoSpawn.transform.position, Quaternion.identity);
        Rigidbody enemAmmoRB = enemyAmmo.GetComponent<Rigidbody>();

        enemAmmoRB.AddForce(direction[1], ForceMode.Impulse);


        yield return new WaitForSeconds(2);


        gunRotator.GetComponent<RotateGun>().xAngle = Mathf.Atan(direction[0].y / direction[0].z) * Mathf.Rad2Deg;

        yield return new WaitUntil(() => gunRotator.GetComponent<RotateGun>().rotating == false);

        GameObject enemyAmmo2 = Instantiate(ammo, ammoSpawn.transform.position, Quaternion.identity);
        Rigidbody enemAmmoRB2 = enemyAmmo2.GetComponent<Rigidbody>();

        enemAmmoRB2.AddForce(direction[0], ForceMode.Impulse);


    }

    public static Vector3[] HitTargetBySpeed(Vector3 startPos, Vector3 targetPos, Vector3 gravityBase, float launchSpd)
    {
        Vector3 AToB = targetPos - startPos;
        Vector3 horizontal = GetHorizontalVector(AToB, gravityBase, startPos);
        Vector3 vertical = GetVerticalVector(AToB, gravityBase, startPos);
        float horizontalDist = horizontal.magnitude;
        float verticalDist = vertical.magnitude;

        float x2 = horizontalDist * horizontalDist;
        float v2 = launchSpd * launchSpd;
        float v4 = launchSpd * launchSpd * launchSpd * launchSpd;
        
        float gravMag = gravityBase.magnitude;

        float launcTest = v4 - (gravMag * ((gravMag * x2) + (2 * verticalDist * v2)));

        Debug.Log("LaunchTest: " + launcTest);

        Vector3[] launch = new Vector3[2];

        if (launcTest < 0 )
        {
            Debug.Log("Not hitting, trying best");
            launch[0] = (horizontal.normalized * launchSpd * Mathf.Cos(45.0f * Mathf.Deg2Rad)) 
                       - (gravityBase.normalized*launchSpd*Mathf.Sin(45.0f * Mathf.Deg2Rad)); 

            launch[1] = (horizontal.normalized * launchSpd * Mathf.Cos(45.0f * Mathf.Deg2Rad))
                       - (gravityBase.normalized * launchSpd * Mathf.Sin(45.0f * Mathf.Deg2Rad));
        }

        else
        {
            Debug.Log("hitting, calculating");

            float[] tanAngle = new float[2];

            tanAngle[0] = (v2 - Mathf.Sqrt(v4 - gravMag * ((gravMag * x2) + 2 * verticalDist * v2))) / (gravMag * horizontalDist);
            tanAngle[1] = (v2 + Mathf.Sqrt(v4 - gravMag * ((gravMag * x2) + 2 * verticalDist * v2))) / (gravMag * horizontalDist);

            float[] finalAngle = new float[2];

            finalAngle[0] = Mathf.Atan(tanAngle[0]);
            finalAngle[1] = Mathf.Atan(tanAngle[1]);

            Debug.Log("Final angles: " + finalAngle[0] * Mathf.Rad2Deg + " and " + finalAngle[1] * Mathf.Rad2Deg);

            launch[0] = (horizontal.normalized * launchSpd * Mathf.Cos(finalAngle[0]))
                       - (gravityBase.normalized * launchSpd * Mathf.Sin(finalAngle[0]));

            launch[1] = (horizontal.normalized * launchSpd * Mathf.Cos(finalAngle[1] ))
                       - (gravityBase.normalized * launchSpd * Mathf.Sin(finalAngle[1] ));

        }

        return launch;
    }

    public static Vector3 GetHorizontalVector(Vector3 AtoB, Vector3 gravityBase, Vector3 start)
    {
        Vector3 outPut;
        Vector3 perpendicular = Vector3.Cross(AtoB, gravityBase);
        perpendicular = Vector3.Cross(gravityBase, perpendicular);
        outPut = Vector3.Project(AtoB, perpendicular);
        Debug.DrawRay(start, outPut, Color.red, 10f);


        return outPut;
    }

    public static Vector3 GetVerticalVector(Vector3 AtoB, Vector3 gravityBase, Vector3 start)
    {
        Vector3 outPut;
        outPut = Vector3.Project(AtoB, gravityBase);
        Debug.DrawRay(start, outPut, Color.blue, 10f);

        return outPut;

    }
}