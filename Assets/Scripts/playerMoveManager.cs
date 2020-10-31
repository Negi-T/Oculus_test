using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class playerMoveManager : MonoBehaviour
{
    bool triggerValue;

    Vector2 primaryAxisValueL;
    Vector2 primaryAxisValueR;
    Vector3 userPos;
    UnityEngine.XR.InputDevice deviceL;
    UnityEngine.XR.InputDevice deviceR;

    initVR initVR;

    GameObject initializer;
    void Start()
    {
        initializer = GameObject.Find("initializerVR");
        initVR = initializer.GetComponent<initVR>();

        deviceL = initVR.deviceL;
        deviceR = initVR.deviceR;
    }

    // Update is called once per frame
    void Update()
    {
        userPos = transform.position;
        playerMovement();

    }

    private void playerMovement()
    {
        if (deviceL.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primary2DAxis, out primaryAxisValueL) && primaryAxisValueL != Vector2.zero)
        {
            transform.position += Vector3.Scale((transform.forward * primaryAxisValueL.y  + transform.right * primaryAxisValueL.x), new Vector3(0.05f,0.05f,0.05f));
        }

        if (deviceR.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primary2DAxis, out primaryAxisValueR) && primaryAxisValueR != Vector2.zero)
        {
            if (primaryAxisValueR.x > 0.5)
                transform.Rotate(new Vector3(0, 1, 0), 5);

            if (primaryAxisValueR.x < -0.5) transform.Rotate(new Vector3(0, 1, 0), -5);

        }
    }
}