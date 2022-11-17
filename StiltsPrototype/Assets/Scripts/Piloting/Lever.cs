using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Lever : MonoBehaviour
{
    //if all else fails we could get just the state and accelerate it forward/backward
    public enum LeverState{
        Forward,
        Backward,
        Zero
    }
    //make sure to lock joystick handle on z and y axis
    public GameObject JoyStickHandle;

    public float ForwardBackwardTilt;
    
    public string PlayerHandTag;

    public LeverState CurrentRead;
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
                //keep in postive range between 1,10
                ForwardBackwardTilt = Mathf.Abs(ForwardBackwardTilt-360) / 10;
                Debug.Log("Reading Angle from lever: " + ForwardBackwardTilt);
                return ForwardBackwardTilt;
            }
            else if (ForwardBackwardTilt> 4 && ForwardBackwardTilt < 74)
            {
                //keep in - range between 1 and 10
                CurrentRead = LeverState.Backward;
                ForwardBackwardTilt = ForwardBackwardTilt / 10;
                Debug.Log("Reading Angle from lever: " + ForwardBackwardTilt);
                return ForwardBackwardTilt;
            }
            CurrentRead = 0;
            Debug.Log("Reading Angle from lever: roughly zero");
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
