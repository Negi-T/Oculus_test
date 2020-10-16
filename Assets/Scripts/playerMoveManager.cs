using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class playerMoveManager : MonoBehaviour
{
    bool triggerValue;

    Vector2 primaryAxisValue;
    Vector3 userPos;
    UnityEngine.XR.InputDevice deviceL;
    UnityEngine.XR.InputDevice deviceR;

    void Start()
    {
        var inputDevices = new List<UnityEngine.XR.InputDevice>();
        var leftHandDevices = new List<UnityEngine.XR.InputDevice>();
        var rightHandDevices = new List<UnityEngine.XR.InputDevice>();

        InputDevices.GetDevices(inputDevices);
        UnityEngine.XR.InputDevices.GetDevicesAtXRNode(UnityEngine.XR.XRNode.LeftHand, leftHandDevices);
        UnityEngine.XR.InputDevices.GetDevicesAtXRNode(UnityEngine.XR.XRNode.RightHand, rightHandDevices);

        userPos = transform.position;

        if (leftHandDevices.Count == 1)
        {
            deviceL = leftHandDevices[0];
            Debug.Log("Left hand found");
        }

        if (rightHandDevices.Count == 1)
        {
            deviceR = rightHandDevices[0];
        }

    }

    // Update is called once per frame
    void Update()
    {
        userPos = transform.position;
        if (deviceL.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primary2DAxis, out primaryAxisValue) && primaryAxisValue != Vector2.zero)
        {
            transform.position += transform.forward * primaryAxisValue.y + transform.right * primaryAxisValue.x;
        }

        if (deviceR.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primary2DAxis, out primaryAxisValue) && primaryAxisValue != Vector2.zero)
        {
            if (primaryAxisValue.x > 0.5)
                transform.Rotate(new Vector3(0, 1, 0), 5);

            if (primaryAxisValue.x < -0.5) transform.Rotate(new Vector3(0, 1, 0), -5);

        }

    }
}