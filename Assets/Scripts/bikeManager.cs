using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class bikeManager : MonoBehaviour
{
    // Start is called before the first frame update

    //UnityEngine.XR.InputDevice deviceL;
    UnityEngine.XR.InputDevice deviceR;

    initVR initVR;

    GameObject initializer;
   
    playerMoveManager pMM;
    private GameObject player;
    Transform bikeTransform;
    bool triggerValue;
    Quaternion angularValueR;
    Vector3 positionValueR;
    //private bool isActiveBike;
    Renderer a;
    void Start()
    {
        initializer = GameObject.FindGameObjectWithTag("initVR");
        initVR = initializer.GetComponent<initVR>();
        //     deviceL = initVR.deviceL;
        deviceR = initVR.deviceR;
        
        a =GetComponent<Renderer>();
        bikeTransform = GetComponent<Transform>();
        player = GameObject.FindGameObjectWithTag("test");
        pMM = player.GetComponent<playerMoveManager>();

         var inputDevices = new List<UnityEngine.XR.InputDevice>();
        var leftHandDevices = new List<UnityEngine.XR.InputDevice>();
        var rightHandDevices = new List<UnityEngine.XR.InputDevice>();

        InputDevices.GetDevices(inputDevices);

        foreach (var device in inputDevices)
        {
            Debug.Log(string.Format("Device found with name '{0}' and role '{1}'", device.name, device.characteristics.ToString()));
        }

        UnityEngine.XR.InputDevices.GetDevicesAtXRNode(UnityEngine.XR.XRNode.RightHand, rightHandDevices);

        if (rightHandDevices.Count == 1)
        {
            deviceR = rightHandDevices[0];
            Debug.Log(string.Format("Device name '{0}' with role '{1}'", deviceR.name, deviceR.characteristics.ToString()));
        }
    }

    // Update is called once per frame
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "test")
        {
            if (deviceR.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out triggerValue) && triggerValue)
            {
                a.material.color = Color.blue;
                RideBike(other);
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        a.material.color = Color.white;
     //   isActiveBike = false;
        GameObject.Find("Bike").transform.parent = null;
        pMM.enabled = true;
    }

    private void RideBike(Collider other)
    {
     //   isActiveBike = true;

        //parent
        transform.parent.transform.SetParent(player.transform);
        pMM.enabled = false;

        //ride_animation
        if (deviceR.TryGetFeatureValue(CommonUsages.deviceRotation, out angularValueR) && angularValueR != Quaternion.identity)
        {
            if (angularValueR.z < 0)
            {
                //  this.transform.position += Vector3.Scale((transform.forward * -angularValueR.z), new Vector3(0.5f,0.5f,0.5f));
                player.transform.position += Vector3.Scale((transform.forward * -angularValueR.z), new Vector3(0.25f, 0.25f, 0.25f));

            }

            //player.transform.Rotate(new Vector3(0, 1, 0), angularValueR.x);
        }

        if (deviceR.TryGetFeatureValue(CommonUsages.devicePosition, out positionValueR) && positionValueR != null)
        {
            float steerAbs = positionValueR.z - transform.position.z;
            Debug.Log(steerAbs);
            if (Mathf.Abs(steerAbs) > 5f) player.transform.Rotate(new Vector3(0, 1, 0), steerAbs);

        }

    }

}