using UnityEngine;
using System;
using System.Collections;

public class BallLauncher : MonoBehaviour
{
    public GameObject BBall;
    public float oTT, uTT;

    private float spinStartX, spinStopX, deltaSpinX, spinStartY, spinStopY, deltaSpinY, throwForce;
    private double distanceToHoopZ, distanceToHoopX, distanceToHoop;
    private GameObject ballInstance, hands, target, camera;
    private Rigidbody rb;
    private Vector3 velocity, sourceToTargetVector; 
    private Quaternion throwDirection;

    void Start()
    {
        ballInstance = Instantiate(BBall);
        hands = GameObject.Find("Hands");
        target = GameObject.Find("Basket_Detector_1");
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        ballInstance.transform.parent = hands.transform;
        rb = ballInstance.GetComponentInChildren<Rigidbody>();
        rb.isKinematic = true;
        ballInstance.transform.position = transform.position;
    }

    void Update()
    {
        if (GvrController.TouchUp)
        { 
            takeMeasurements();
            throwBall();
            addTorqueToBall();
        }

        if (GvrController.TouchDown)
        {
            rb.transform.position = transform.position;
            ballInstance.transform.parent = hands.transform;
            rb.isKinematic = true;
        }
    }

    private void throwBall()
    {
        rb.isKinematic = false;
        ballInstance.transform.parent = null;

        //Magic number offset for calculating the perfect throw from any distance under gravity
        float velocityFactor = 5.5f + (float)distanceToHoop / 20;
        sourceToTargetVector = sourceToTargetVector + new Vector3(0, velocityFactor, 0);

        //TODO refactor this statement: 
        if (velocity.x < sourceToTargetVector.x + 1.5f && velocity.x > sourceToTargetVector.x - 1.5f && (velocity.y + velocity.z) < (sourceToTargetVector.y + sourceToTargetVector.z + oTT) && velocity.y + velocity.z > (sourceToTargetVector.y + sourceToTargetVector.z - uTT))
        { // guided throw
            rb.isKinematic = false;
            ballInstance.transform.parent = null;
            rb.velocity = sourceToTargetVector;
        }
        else
        { // normal throw
            velocity = throwDirection * new Vector3(0, throwForce/3, throwForce);//(new Vector3(0, throwArc * throwForce, throwForce)/2);
            Vector3 throwDirectionVector = throwDirection * new Vector3(1, 1, 1);
            rb.velocity = velocity;
        }

        //rb.AddForce((target.transform.position - transform.position) * 2f);
        GameManager.ballThrown++;
    }

    private void takeMeasurements()
    {
        //Setup for Torque
        spinStartX = GvrController.TouchPos.x;
        spinStartY = GvrController.TouchPos.y;

        sourceToTargetVector = (target.transform.position - hands.transform.position);
        throwDirection = GvrController.Orientation;
        throwDirection.x += 0.35f; // controller x axis rotation correct
        throwForce = Math.Abs(Math.Max(GvrController.Gyro.x, GvrController.Gyro.y)) * 0.55f; // gyro force
        velocity = throwDirection * new Vector3(0, 0, throwForce);

        distanceToHoopZ = Math.Abs(camera.transform.position.z - (GameObject.Find("Basket_Detector_1").transform.position.z));
        distanceToHoopX = Math.Abs(camera.transform.position.x - (GameObject.Find("Basket_Detector_1").transform.position.x));
        distanceToHoop = Math.Sqrt(Math.Pow(distanceToHoopX, 2) + Math.Pow(distanceToHoopZ, 2));
    }

    private void addTorqueToBall()
    {
        spinStopX = GvrController.TouchPos.x;
        spinStopY = GvrController.TouchPos.y;
        deltaSpinX = (spinStartX - spinStopX);
        deltaSpinY = -(spinStartY - spinStopY);

        rb.AddTorque(new Vector3(-deltaSpinY, deltaSpinX, 0f), ForceMode.Impulse);
        rb.maxAngularVelocity = 50;
    }
}