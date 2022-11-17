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
    public Lever DirectionalLever;
    public CurrentState DroneState;
    public float MaxSpeed;

    private float DirectionalAcceleration;
    private float RotationalAcceleration;
    public float AccelerationRate;
    public int UpperLeverLimit, LowerLeverLimit;


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
        Vector3 debugVector = Vector3.forward * DirectionalLever.getTilt(); //* Time.deltaTime * BaseMoveSpeed;
        Drone.transform.position += Vector3.forward * DirectionalLever.getTilt(); //* Time.deltaTime * BaseMoveSpeed;
        Debug.Log("forward backward lever value reads:" + DirectionalLever.getTilt() + "drone transform change should add:" + debugVector );
    }

    private void calculateRotationalMovement()
    {

        Drone.transform.Rotate(Vector3.forward * RotationalLever.getTilt() * Time.deltaTime * BaseTurnSpeed);
        Vector3 DebugVector = Vector3.forward * RotationalLever.getTilt() * Time.deltaTime * BaseTurnSpeed;
        Debug.Log("value being added to drone quaternion = "+ DebugVector);
    }

    public void FailSafeMovement()
    {
        switch(DirectionalLever.CurrentRead)
        {
            case Lever.LeverState.Forward:
                Debug.Log("accelerating");
                DirectionalAcceleration += AccelerationRate;
                break;
            case Lever.LeverState.Backward:
                Debug.Log("decelerating");
                DirectionalAcceleration -= AccelerationRate;
                break;
            case Lever.LeverState.Zero:
                if (DirectionalAcceleration > 0) { DirectionalAcceleration -= AccelerationRate; }
                if (DirectionalAcceleration < 0) { DirectionalAcceleration += AccelerationRate; }
                if (DirectionalAcceleration == 0) { Debug.Log("do nothing"); }
                break;
        }
        DirectionalAcceleration = Mathf.Clamp(DirectionalAcceleration, LowerLeverLimit, UpperLeverLimit);
        Drone.transform.position += Vector3.forward * DirectionalAcceleration * Time.deltaTime * BaseMoveSpeed;

        switch (RotationalLever.CurrentRead)
        {
            case Lever.LeverState.Forward:
                Debug.Log("accelerating");
                RotationalAcceleration += AccelerationRate;
                break;
            case Lever.LeverState.Backward:
                Debug.Log("decelerating");
                RotationalAcceleration -= AccelerationRate;
                break;
            case Lever.LeverState.Zero:
                if (RotationalAcceleration > 0) { RotationalAcceleration -= AccelerationRate; }
                if (RotationalAcceleration < 0) { RotationalAcceleration += AccelerationRate; }
                if (RotationalAcceleration == 0) { Debug.Log("do nothing"); }
                break;
        }
        RotationalAcceleration = Mathf.Clamp(RotationalAcceleration, LowerLeverLimit, UpperLeverLimit);
        Drone.transform.Rotate(Vector3.forward * Mathf.Clamp(DirectionalAcceleration * Time.deltaTime * BaseMoveSpeed,-1*MaxSpeed,MaxSpeed));

    }

}
