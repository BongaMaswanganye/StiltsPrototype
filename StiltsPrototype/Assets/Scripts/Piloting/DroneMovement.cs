using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneMovement : MonoBehaviour
{
    public enum CurrentState{
        Active,
        FailSafe,
        Inactive

    }

    public GameObject Drone;
    public float BaseMoveSpeed = 2;
    public float BaseTurnSpeed = 2;
    public Lever RotationalLever;
    public Lever ForwardMovementLever;
    public CurrentState DroneState;

    private float Acceleration;
    public float AccelerationRate;
    public int UpperLimit, LowerLimit;


    private void Update()
    {
        if(DroneState == CurrentState.Active)
        {
            calculateForwardBackwardMovement();
            calculateRotationalMovement();
        }
        if (DroneState == CurrentState.FailSafe)
        {
            FailSafeMovement();
        }
    }

    private void calculateForwardBackwardMovement()
    {
        Vector3 debugVector = Vector3.forward * ForwardMovementLever.getTilt(); //* Time.deltaTime * BaseMoveSpeed;
        Drone.transform.position += Vector3.forward * ForwardMovementLever.getTilt(); //* Time.deltaTime * BaseMoveSpeed;
        Debug.Log("forward backward lever value reads:" + ForwardMovementLever.getTilt() + "drone transform change should add:" + debugVector );
    }

    private void calculateRotationalMovement()
    {

        Drone.transform.Rotate(Vector3.forward * RotationalLever.getTilt() * Time.deltaTime * BaseTurnSpeed);
        Vector3 DebugVector = Vector3.forward * RotationalLever.getTilt() * Time.deltaTime * BaseTurnSpeed;
        Debug.Log("value being added to drone quaternion = "+ DebugVector);
    }

    public void FailSafeMovement()
    {
        switch(ForwardMovementLever.CurrentRead)
        {
            case Lever.LeverState.Forward:
                Debug.Log("accelerating");
                Acceleration += AccelerationRate;
                break;
            case Lever.LeverState.Backward:
                Debug.Log("decelerating");
                Acceleration -= AccelerationRate;
                break;
            case Lever.LeverState.Zero:
                if (Acceleration > 0) { Acceleration -= AccelerationRate; }
                if (Acceleration < 0) { Acceleration += AccelerationRate; }
                if (Acceleration == 0) { Debug.Log("do nothing"); }
                break;
        }
        Acceleration = Mathf.Clamp(Acceleration, LowerLimit, UpperLimit);
        Drone.transform.position += Vector3.forward * Acceleration * Time.deltaTime * BaseMoveSpeed;

    }

}
