using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandController : MonoBehaviour
{
    private SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device Controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }

    private Valve.VR.EVRButtonId trigger = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
    public bool triggerUp = false;
    public bool triggerDown = false;
    public bool triggerPressed = false;

    // Use this for initialization
    void Start()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
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
            Debug.Log("Trigger down.");

        if (triggerUp)
            Debug.Log("Trigger up.");
    }
}
