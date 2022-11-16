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
    public SteamVR_Input RequiredInput;
    


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
            if (ForwardBackwardTilt < 350 && ForwardBackwardTilt > 290)
            {
                ForwardBackwardTilt = Mathf.Abs(ForwardBackwardTilt-360);
                return ForwardBackwardTilt;
            }
            else if (ForwardBackwardTilt> 5 && ForwardBackwardTilt < 74)
            {
                return ForwardBackwardTilt;
            }
            Debug.Log("issue in calculation");
            return 0;

        }

    }

    private void OnTriggerStay(Collider other)
    {
        if ((other.CompareTag(PlayerHandTag)))
        {
            JoyStickHandle.transform.LookAt(other.transform.position, JoyStickHandle.transform.up);
        }
    }
}
