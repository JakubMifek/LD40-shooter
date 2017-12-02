using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FixedJoint)), RequireComponent(typeof(Collider))]
public class WandController : MonoBehaviour
{
    private SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device Controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }

    public float rotateSpeed = 0.25f;
    public float mass = 1.0f;

    private Valve.VR.EVRButtonId trigger = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
    public bool triggerUp = false;
    public bool triggerDown = false;
    public bool triggerPressed = false;

    public GameObject pickupable;
    private FixedJoint fJoint;

    private bool throwing;
    public Rigidbody throwable;

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
        if (throwing && throwable != null)
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
            throwing = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pickupable")
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
            if (pickupable.GetComponent<Rigidbody>() == null)
            {
                var fabricator = (Fabricatable)(pickupable.GetComponent("Fabricatable"));
                var position = pickupable.transform.position;
                var size = pickupable.transform.lossyScale;
                pickupable = Instantiate(fabricator.Fabricator);
                pickupable.transform.localPosition = position;
                pickupable.transform.localScale = size;
                var body = pickupable.AddComponent<Rigidbody>();
                body.mass = mass;
            }

            fJoint.connectedBody = pickupable.GetComponent<Rigidbody>();
            throwing = false;
            throwable = null;
        }
        else
        {
            fJoint.connectedBody = null;
        }
    }
}
