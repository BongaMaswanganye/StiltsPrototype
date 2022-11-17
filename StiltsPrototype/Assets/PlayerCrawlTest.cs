using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class PlayerCrawlTest : MonoBehaviour
{
    public SteamVR_Action_Vector2 input;
    public SteamVR_Action_Boolean grabGripAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("GrabGrip");
    public float moveSpeed = 5;
    public Transform currentPlayerPos;
    public GameObject righthandPosition;
    

    Vector3 rightHandPosVector;
    private CharacterController characterController;

    // Start is called before the first frame update
    void Start()
    {
        rightHandPosVector = righthandPosition.transform.position;
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        rightHandPosVector = righthandPosition.transform.position;
        ClawlingAction();
        TrackPadMoving();
    }
    
    public void ClawlingAction()
    {
        if(grabGripAction.GetStateDown(SteamVR_Input_Sources.RightHand))// get the input down from right hand grip
        {
            Vector3 crawlingDir = (rightHandPosVector - currentPlayerPos.transform.position).normalized;//getting the crawling direction
            characterController.Move(crawlingDir * moveSpeed * Time.deltaTime); // crawling
            //Vector3 direction = Player.instance.hmdTransform.TransformDirection(rightHandPosVector);
            //characterController.Move(moveSpeed * Time.deltaTime * Vector3.ProjectOnPlane(direction, Vector3.up));
            Debug.Log("ClawlingWork");
            

        }

    }
    public void TrackPadMoving() // testing moving with TrackPad 
    {
        Vector3 direction = Player.instance.hmdTransform.TransformDirection(new Vector3(input.axis.x, input.axis.y));
        characterController.Move(moveSpeed * Time.deltaTime * Vector3.ProjectOnPlane(direction, Vector3.up));
        Debug.Log("MovingByTrackPadWork");
    }
}
