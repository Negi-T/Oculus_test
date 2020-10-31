using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class cube : MonoBehaviour
{
    // Start is called before the first frame update

    UnityEngine.XR.InputDevice deviceL;
    UnityEngine.XR.InputDevice deviceR;

    initVR initVR;

    GameObject initializer;

    private GameObject player;

    bool triggerValue;
    Quaternion angularValueR;
    private bool isActiveBike;
    Renderer a;
    void Start()
    {
        initializer = GameObject.Find("initializerVR");
        initVR = initializer.GetComponent<initVR>();

        deviceL = initVR.deviceL;
        deviceR = initVR.deviceR;

        a = GetComponent<Renderer>();
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
        if (other.gameObject.tag == "test")
        {
            isActiveBike = false;
        }
    }

    private void RideBike(Collider other)
    {
        isActiveBike = true;

        //Collider Off 

        //ride_animation

        //parent
        //   player.transform.SetParent();

        if (deviceR.TryGetFeatureValue(CommonUsages.deviceRotation, out angularValueR) && angularValueR != Quaternion.identity)
        {
            Debug.Log(angularValueR.z);
        }

    }

}