﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FixedJoint)), RequireComponent(typeof(Collider))]
public class WandController : MonoBehaviour
{
    private SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device Controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }

    public float rotateSpeed = 0.25f;

    private Valve.VR.EVRButtonId trigger = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
    public bool triggerUp = false;
    public bool triggerDown = false;
    public bool triggerPressed = false;

    private GameObject pickupable;
    private FixedJoint fJoint;

    private bool throwing;
    private Rigidbody throwable;

    // Use this for initialization
    void Start()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        fJoint = GetComponent<FixedJoint>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Controller == null)
        {
            Debug.Log("Controller not initialized.");
            return;
        }

        triggerDown = Controller.GetPressDown(trigger);
        triggerUp = Controller.GetPressUp(trigger);
        triggerPressed = Controller.GetPress(trigger);

        if (triggerDown)
        {
            PickUpObject();
        }

        if (triggerUp)
        {
            DropObject();
        }
    }

    private void FixedUpdate()
    {
        if (throwing)
        {
            Transform origin;
            if (trackedObj.origin != null)
                origin = trackedObj.origin;
            else
                origin = trackedObj.transform.parent;

            if (origin != null)
            {
                throwable.velocity = origin.TransformVector(Controller.velocity);
                throwable.angularVelocity = origin.TransformVector(Controller.angularVelocity * rotateSpeed);
            }
            else
            {
                throwable.velocity = (Controller.velocity);
                throwable.angularVelocity = (Controller.angularVelocity * rotateSpeed);
            }

            throwable.maxAngularVelocity = throwable.velocity.magnitude;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "pickupable")
        {
            pickupable = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        pickupable = null;
    }

    private void DropObject()
    {
        throwable = fJoint.connectedBody;
        fJoint.connectedBody = null;
        throwing = true;
    }

    private void PickUpObject()
    {
        if (pickupable != null)
        {
            fJoint.connectedBody = gameObject.GetComponent<Rigidbody>();
            throwing = false;
            throwable = null;
        }
        else
        {
            fJoint.connectedBody = null;
        }
    }
}
