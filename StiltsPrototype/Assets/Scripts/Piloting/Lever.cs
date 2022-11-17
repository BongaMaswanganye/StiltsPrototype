using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Lever : MonoBehaviour
{
    //make sure to lock joystick handle on z and y axis
    public GameObject JoyStickHandle;

    public float ForwardBackwardTilt;
    
    public string PlayerHandTag;

    //for later
    public SteamVR_Action_Boolean Grab;

    public bool requireGrabPress;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float getTilt()
    {
        ForwardBackwardTilt = JoyStickHandle.transform.rotation.x;
        {
            if (ForwardBackwardTilt < 360 && ForwardBackwardTilt > 290)
            {
                ForwardBackwardTilt = Mathf.Abs(ForwardBackwardTilt-360);
                Debug.Log("Reading Angle from lever: " + ForwardBackwardTilt);
                return ForwardBackwardTilt;
            }
            else if (ForwardBackwardTilt> 0 && ForwardBackwardTilt < 74)
            {
                Debug.Log("Reading Angle from lever: " + ForwardBackwardTilt);
                return ForwardBackwardTilt;
            }
            Debug.Log("roughly zero");
            return 0;

        }

    }

    private void OnTriggerStay(Collider other)
    {
        if ((other.CompareTag(PlayerHandTag)))
        {
            transform.LookAt(other.transform.position, transform.up);
        }
    }
}
