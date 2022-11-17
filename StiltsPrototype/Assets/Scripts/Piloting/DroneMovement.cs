using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneMovement : MonoBehaviour
{
    public enum CurrentState{
        Active,
        Inactive

    }

    public GameObject Drone;
    public float BaseMoveSpeed = 2;
    public float BaseTurnSpeed = 2;
    public Lever RotationalLever;
    public Lever ForwardMovementLever;
    public CurrentState DroneState;

  

    

    private void FixedUpdate()
    {
        if(DroneState == CurrentState.Active)
        {
            calculateForwardBackwardMovement();
            calculateRotationalMovement();
        }
    }

    private void calculateForwardBackwardMovement()
    {
        Vector3 debugVector = Vector3.forward * ForwardMovementLever.getTilt() * Time.deltaTime * BaseMoveSpeed;
        Drone.transform.position += Vector3.forward  * ForwardMovementLever.getTilt() * Time.deltaTime * BaseMoveSpeed;
        Debug.Log("forward backward lever value reads:" + ForwardMovementLever.getTilt() + "drone transform change should add:" );
    }

    private void calculateRotationalMovement()
    {

        Drone.transform.Rotate(Vector3.forward * RotationalLever.getTilt() * Time.deltaTime * BaseTurnSpeed);
        Vector3 DebugVector = Vector3.forward * RotationalLever.getTilt() * Time.deltaTime * BaseTurnSpeed;
        Debug.Log("value being added to drone quaternion = "+DebugVector);
    }

}
