﻿using UnityEngine;
using System;
using System.Collections;

public class BallLauncher : MonoBehaviour
{

    public GameObject BBall;
    public float oTT, uTT, sTT;

    private float spinStartX, spinStopX, deltaSpinX, spinStartY, spinStopY, deltaSpinY, throwForce, mix;
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
        if (GvrController.ClickButtonUp)
        { 
            takeMeasurements();
            throwBall();
            addTorqueToBall();
        }

        if (GvrController.ClickButtonDown)
        {
            //ballInstance = Instantiate(BBall);
            //rb = ballInstance.GetComponentInChildren<Rigidbody>();

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
        float velocityFactor = 3.5f + (float)distanceToHoop *0.5f;
        sourceToTargetVector = sourceToTargetVector + new Vector3(0, velocityFactor, 0);

        //TODO refactor :
        if (velocity.x < sourceToTargetVector.x + sTT && velocity.x > sourceToTargetVector.x - sTT && (velocity.y + velocity.z) < (sourceToTargetVector.y + sourceToTargetVector.z + oTT) && velocity.y + velocity.z > (sourceToTargetVector.y + sourceToTargetVector.z - uTT))
        { // guided throw
            rb.isKinematic = false;
            ballInstance.transform.parent = null;
            rb.velocity = sourceToTargetVector + new Vector3(UnityEngine.Random.Range(-0.1f, 0.1f), UnityEngine.Random.Range(-0.1f, 0.1f), UnityEngine.Random.Range(-0.25f, 0.25f));
        }
        else
        { // normal throw
            velocity = throwDirection * new Vector3(0, throwForce/3, throwForce);//(new Vector3(0, throwArc * throwForce, throwForce)/2);
            Vector3 throwDirectionVector = throwDirection * new Vector3(1, 1, 1);
            rb.velocity = velocity;
        }

        //rb.AddForce((target.transform.position - transform.position) * 2f);
        GameManagerMain.totalShotsTaken++;
    }

    private void takeMeasurements()
    {
        //Setup for Torque
        spinStartX = GvrController.TouchPos.x;
        spinStartY = GvrController.TouchPos.y;

        sourceToTargetVector = (target.transform.position - hands.transform.position);
        throwDirection = new Quaternion(GvrController.Orientation.x + 0.18f, GvrController.Orientation.y, GvrController.Orientation.z, GvrController.Orientation.w); 
        //throwDirection.x = 0;
        //throwDirection.x += 0.3f;  // controller x axis rotation correct

        throwForce = (Math.Max(Math.Abs(GvrController.Gyro.x), Math.Abs(GvrController.Gyro.y))) * 0.35f + GvrController.Accel.y *0.35f; // gyro force
        velocity = throwDirection * new Vector3(0, 0, throwForce);

        distanceToHoopZ = Math.Abs(camera.transform.position.z - target.transform.position.z);
        distanceToHoopX = Math.Abs(camera.transform.position.x - target.transform.position.x);
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