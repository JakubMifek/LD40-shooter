using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FixedJoint)), RequireComponent(typeof(Collider)), RequireComponent(typeof(SteamVR_TrackedController)), RequireComponent(typeof(LevelSwitch))]
public class WandController : MonoBehaviour
{
    private LevelSwitch levelSwitch;

    private SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device Controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
    private SteamVR_TrackedController controller;

    public float rotateSpeed = 0.25f;

    private Valve.VR.EVRButtonId trigger = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
    public bool triggerUp = false;
    public bool triggerDown = false;
    public bool triggerPressed = false;

    public GameObject pickupable;
    private FixedJoint fJoint;

    private bool throwing;
    public Rigidbody throwable;
    public ThrownEffect effect;
    public LevelInfo info;

    // Use this for initialization
    void Start()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        fJoint = GetComponent<FixedJoint>();
        controller = GetComponent<SteamVR_TrackedController>();
        levelSwitch = GetComponent<LevelSwitch>();

        controller.MenuButtonClicked += Controller_MenuButtonClicked;
        controller.PadClicked += Controller_PadClicked;
    }

    private void Controller_PadClicked(object sender, ClickedEventArgs e)
    {
        if (info == null) return;

        levelSwitch.level = "Menu";

        if (PlayerPrefs.GetInt("scoreable", 0) == 1)
            levelSwitch.level = info.nextLevel;

        levelSwitch.Switch();
    }

    private void Controller_MenuButtonClicked(object sender, ClickedEventArgs e)
    {
        levelSwitch.level = "Menu";
        levelSwitch.Switch();
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

            float slow = (float)Math.Pow(throwable.mass, 1.0 / 3.0);
            if (effect != null)
            {
                effect.Throw();
                effect = null;
            }

            if (origin != null)
            {
                throwable.velocity = origin.TransformVector(Controller.velocity / slow);
                throwable.angularVelocity = origin.TransformVector(Controller.angularVelocity * rotateSpeed / slow);
            }
            else
            {
                throwable.velocity = (Controller.velocity / slow);
                throwable.angularVelocity = (Controller.angularVelocity * rotateSpeed / slow);
            }

            throwable.maxAngularVelocity = throwable.velocity.magnitude;
        }
        throwing = false;
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
            var fabricator = (Fabricatable)(pickupable.GetComponent("Fabricatable"));
            if (pickupable.GetComponent<Rigidbody>() == null)
            {
                var tMesh = fabricator.TDtext.GetComponent<TextMesh>();
                var rest = int.Parse(tMesh.text);
                if (rest == 0)
                    return;

                tMesh.text = (rest - 1).ToString();
                var position = pickupable.transform.position;
                var size = pickupable.transform.lossyScale;

                pickupable = Instantiate(fabricator.Fabricator);
                fabricator = (Fabricatable)(pickupable.GetComponent("Fabricatable"));
                tMesh = fabricator.TDtext.GetComponent<TextMesh>();

                tMesh.text = (rest).ToString();

                pickupable.transform.localPosition = position;
                pickupable.transform.localScale = size;
            }

            fJoint.connectedBody = pickupable.GetComponent<Rigidbody>();
            effect = pickupable.GetComponent<ThrownEffect>();
            if (effect != null)
                effect.bonus = fabricator.bonus;
            throwing = false;
            throwable = null;
        }
        else
        {
            fJoint.connectedBody = null;
        }
    }
}
