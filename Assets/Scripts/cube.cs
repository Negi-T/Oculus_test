using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class cube : MonoBehaviour
{
    // Start is called before the first frame update
    bool triggerValue;
    UnityEngine.XR.InputDevice deviceL;
    UnityEngine.XR.InputDevice deviceR;

    Renderer a;
    void Start()
    {
        var inputDevices = new List<UnityEngine.XR.InputDevice>();
        var leftHandDevices = new List<UnityEngine.XR.InputDevice>();
        var rightHandDevices = new List<UnityEngine.XR.InputDevice>();
        a = gameObject.GetComponent<Renderer>();
        a.material.color = Color.red;
        InputDevices.GetDevices(inputDevices);
        UnityEngine.XR.InputDevices.GetDevicesAtXRNode(UnityEngine.XR.XRNode.LeftHand, leftHandDevices);
        UnityEngine.XR.InputDevices.GetDevicesAtXRNode(UnityEngine.XR.XRNode.RightHand, rightHandDevices);

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

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "test")
        {
            Debug.Log("In!");
            if (deviceL.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out triggerValue) && triggerValue)
            {
                a.material.color = Color.blue;
            }
        }

        if (deviceL.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out triggerValue) && triggerValue)
            {
                Debug.Log("Hoge");
            }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "test")
        {
            Debug.Log("Enter");
        }
    }
}