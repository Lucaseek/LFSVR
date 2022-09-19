using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class HandControls : MonoBehaviour
{
    //devices
    List<InputDevice> devices = new List<InputDevice>(), devices2 = new List<InputDevice>();
    InputDeviceCharacteristics rightCC, leftCC;
    private bool test = false;
    private InputDevice targetDevice;

    //configurations
    private VideoProgress videoManager; //video config
    public GameObject user; //user reference

    void Start()
    {
        videoManager = GetComponent<VideoProgress>();

    } 

    // Update is called once per frame
    void Update()
    {
        //connect to devices
        if (devices.Count < 3) //0-Headset -> 1-Left_Controller -> 2-Right_Controller
        {
            InputDevices.GetDevices(devices);
            foreach (var item in devices)
            {
                if (devices.Count == 3)
                { 
                    Debug.Log(item.name + item.characteristics);

                    if (test == false)
                    {
                        SetControllers();
                        test = true;
                    }
                }
            }
        }

        InputTrigger(); //detect inputs triggered from controller
    }

    private void SetControllers() //0-Right
    {
        rightCC = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(rightCC, devices2);
        /*leftCC = InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(leftCC, devices2);*/

        if (devices2.Count > 0) //only call if the are enough devices
        {
            targetDevice = devices2[0]; //righthand device
        }
    }

    private void InputTrigger()
    {
        //Menu button (3 lines at top)
        targetDevice.TryGetFeatureValue(CommonUsages.menuButton, out bool menuButtonValue);
        if (menuButtonValue)
        {
            Debug.Log("Pressing Menu Button");
            videoManager.PauseVideo();

        }

        //Trigger behind holding value
        targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue);
        if (triggerValue > 0.1f)
        {
            Debug.Log("Trigger Pressed " + triggerValue);
        }

        //Trigger button click value
        targetDevice.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggerBtValue);
        if (triggerBtValue)
        {
            user.GetComponent<DisplayInfoBox>().DeactivateBox();
        }

        //Huge circle in the middle - finger position (touchpad/joystick) (-1 to 1, -1 to 1)
        targetDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 primary2DAxisValue);
        if (primary2DAxisValue != Vector2.zero)
        {
            //Debug.Log("Primary Touchpad " + primary2DAxisValue);
        }

        //Click the huge circle
        targetDevice.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out bool primary2DAxisClickValue);
        if (primary2DAxisClickValue)
        {
            Debug.Log("Touchpad Pressed");
            videoManager.PauseVideo();
        }
    }
}
