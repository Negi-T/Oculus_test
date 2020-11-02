using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class playerMoveManager : MonoBehaviour
{
 public   Vector2 primaryAxisValueL;
    Vector2 primaryAxisValueR;
    Vector3 userPos;
  public  UnityEngine.XR.InputDevice deviceML;
  public  UnityEngine.XR.InputDevice deviceMR;

  public  initVR initVR;

  public  GameObject initializer;
    void Start()
    {
        initializer = GameObject.Find("initVR");
        initVR = initializer.GetComponent<initVR>();

       // deviceML = initVR.deviceL;
     //   deviceMR = initVR.deviceR;

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
            deviceML = leftHandDevices[0];
            Debug.Log(string.Format("Device name '{0}' with role '{1}'", deviceML.name, deviceML.characteristics.ToString()));
        }
        else if (leftHandDevices.Count > 1)
        {
            Debug.Log("Found more than one left hand!");
        }

        UnityEngine.XR.InputDevices.GetDevicesAtXRNode(UnityEngine.XR.XRNode.RightHand, rightHandDevices);

        if (rightHandDevices.Count == 1)
        {
            deviceMR = rightHandDevices[0];
            Debug.Log(string.Format("Device name '{0}' with role '{1}'", deviceMR.name, deviceMR.characteristics.ToString()));
        }
        else if (rightHandDevices.Count > 1)
        {
            Debug.Log("Found more than one right hand!");
        }

    }

    // Update is called once per frame
    void Update()
    {
        userPos = transform.position;
        playerMovement();
    }

   private void playerMovement()
    {
        if (deviceML.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primary2DAxis, out primaryAxisValueL) && primaryAxisValueL != Vector2.zero)
        {
          
            transform.position += Vector3.Scale((transform.forward * primaryAxisValueL.y  + transform.right * primaryAxisValueL.x), new Vector3(0.05f,0.05f,0.05f));
        }

        if (deviceMR.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primary2DAxis, out primaryAxisValueR) && primaryAxisValueR != Vector2.zero)
        {
            if (primaryAxisValueR.x > 0.5)
                transform.Rotate(new Vector3(0, 1, 0), 5);

            if (primaryAxisValueR.x < -0.5) transform.Rotate(new Vector3(0, 1, 0), -5);

        }
    }
}