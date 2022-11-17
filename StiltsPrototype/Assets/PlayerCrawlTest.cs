using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class PlayerCrawlTest : MonoBehaviour
{
    public SteamVR_Action_Vector2 input;
    public SteamVR_Action_Boolean grabGripAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("GrabGrip");
    public float moveSpeed = 1;
    public Transform currentPlayerPos;
    public GameObject righthandPosition;
    

    Vector3 rightHandPosVector;
    //Vector3 crawlingDistance;
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
        CrawlingAction();
        TrackPadMoving();
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawCube(crawlingDistance, Vector3.one);
    //}

    public void CrawlingAction()
    {
        if(grabGripAction.GetState(SteamVR_Input_Sources.Any))// get the input down from right hand grip
        {
            
            Vector3 crawlingDir = (rightHandPosVector - transform.position).normalized;//getting the crawling direction
            characterController.Move(crawlingDir * moveSpeed * Time.deltaTime); // crawling
            //crawlingDistance = crawlingDir;
            //Vector3 direction = Player.instance.hmdTransform.TransformDirection(rightHandPosVector);
            //characterController.Move(moveSpeed * Time.deltaTime * Vector3.ProjectOnPlane(direction, Vector3.up));
            Debug.Log("CrawlingWorked");
            

        }

    }
    public void TrackPadMoving() // testing moving with TrackPad 
    {
        Vector3 direction = Player.instance.hmdTransform.TransformDirection(new Vector3(input.axis.x, input.axis.y));
        characterController.Move(moveSpeed * Time.deltaTime * Vector3.ProjectOnPlane(direction, Vector3.up));
        Debug.Log("MovingByTrackPadWork");
    }
}
