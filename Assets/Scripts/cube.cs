﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class cube : MonoBehaviour
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
    private bool isActiveBike;
    Renderer a;
    void Start()
    {
        initializer = GameObject.Find("initializerVR");
        initVR = initializer.GetComponent<initVR>();

        //     deviceL = initVR.deviceL;
        deviceR = initVR.deviceR;

        a = GetComponent<Renderer>();
        bikeTransform = GetComponent<Transform>();
        player = GameObject.FindGameObjectWithTag("test");
        pMM = player.GetComponent<playerMoveManager>();
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
        isActiveBike = false;
        this.transform.parent = null;
        pMM.enabled = true;
    }

    private void RideBike(Collider other)
    {
        isActiveBike = true;

        //parent
        this.transform.SetParent(player.transform);
        pMM.enabled = false;

        //ride_animation
        if (deviceR.TryGetFeatureValue(CommonUsages.deviceRotation, out angularValueR) && angularValueR != Quaternion.identity)
        {
          if(angularValueR.z < 0)   Debug.Log(angularValueR.z);
        }

    }

}