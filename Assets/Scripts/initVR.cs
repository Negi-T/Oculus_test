using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
public class initVR : MonoBehaviour
{
    
    
    public UnityEngine.XR.InputDevice deviceL;
    public UnityEngine.XR.InputDevice deviceR;
    public void Start()
    {

        var inputDevices = new List<UnityEngine.XR.InputDevice>();
        var leftHandDevices = new List<UnityEngine.XR.InputDevice>();
        var rightHandDevices = new List<UnityEngine.XR.InputDevice>();

        InputDevices.GetDevices(inputDevices);

        foreach (var device in inputDevices)
        {
            Debug.Log(string.Format("Device found with name '{0}' and role '{1}'", device.name, device.characteristics.ToString()));
        }

        UnityEngine.XR.InputDevices.GetDevicesAtXRNode(UnityEngine.XR.XRNode.LeftHand, leftHandDevices);

        if (leftHandDevices.Count == 1)
        {
            deviceL = leftHandDevices[0];
            Debug.Log(string.Format("Device name '{0}' with role '{1}'", deviceL.name, deviceL.characteristics.ToString()));
        }
        else if (leftHandDevices.Count > 1)
        {
            Debug.Log("Found more than one left hand!");
        }

        UnityEngine.XR.InputDevices.GetDevicesAtXRNode(UnityEngine.XR.XRNode.RightHand, rightHandDevices);

        if (rightHandDevices.Count == 1)
        {
            deviceR = rightHandDevices[0];
            Debug.Log(string.Format("Device name '{0}' with role '{1}'", deviceR.name, deviceR.characteristics.ToString()));
        }
        else if (rightHandDevices.Count > 1)
        {
            Debug.Log("Found more than one right hand!");
        }

    }

}
