using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
namespace bikeManager
{
    public class bikeManager : MonoBehaviour
    {

        UnityEngine.XR.InputDevice deviceR;

        initVR initVR;

        GameObject initializer;

        playerMoveManager pMM;
        private GameObject player;
        Transform bikeTransform;
        bool triggerValue;
        Quaternion angularValueR;
        Vector3 positionValueR;
        Vector2 primaryAxisValueR;
        Vector3 handPositionRight;
        //private bool isActiveBike;
        //    Renderer a;
        void Start()
        {
            initializer = GameObject.FindGameObjectWithTag("initVR");
            initVR = initializer.GetComponent<initVR>();
            deviceR = initVR.deviceR;

            bikeTransform = GetComponent<Transform>();
            player = GameObject.Find("XRRig");
            pMM = player.GetComponent<playerMoveManager>();

        }
        // Update is called once per frame
        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                if (deviceR.TryGetFeatureValue(CommonUsages.triggerButton, out triggerValue) && triggerValue)
                {
                    RideBike(other);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            GameObject.Find("Bike").transform.parent = null;
            pMM.enabled = true;
        }

        private void RideBike(Collider other)
        {
            Debug.Log("hoge");
            //set new parent
            GameObject.Find("Bike").transform.SetParent(player.transform);
            pMM.enabled = false;
            //ride_animation
            if (deviceR.TryGetFeatureValue(CommonUsages.deviceRotation, out angularValueR) && angularValueR != Quaternion.identity)
            {
                if (angularValueR.z < 0)
                {
                    player.transform.position += Vector3.Scale((transform.forward * -angularValueR.z), new Vector3(0.25f, 0.25f, 0.25f));
                }
            }
            //to be updated in the future for better immersive steerings.
            // if (deviceR.TryGetFeatureValue(CommonUsages.devicePosition, out positionValueR) && positionValueR != null)
            // {
            //     float steerAbs = Vector3.Distance(handPositionRight, positionValueR);
            //  //   Debug.Log(steerAbs);
            //     //   if (steerAbs > -0.78f) player.transform.Rotate(new Vector3(0, 1, 0), -steerAbs);
            //     // else if (steerAbs < -0.7f) player.transform.Rotate(new Vector3(0, 1, 0), -steerAbs);
            // }

            if (deviceR.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primary2DAxis, out primaryAxisValueR) && primaryAxisValueR != Vector2.zero)
            {
                if (primaryAxisValueR.x > 0.5)
                    player.transform.Rotate(new Vector3(0, 1, 0), 5);
                if (primaryAxisValueR.x < -0.5)
                    player.transform.Rotate(new Vector3(0, 1, 0), -5);
            }
        }
    }
}