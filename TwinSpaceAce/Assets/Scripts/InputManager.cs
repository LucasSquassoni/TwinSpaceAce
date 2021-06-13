using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour
{
    public ShipMover shipMover;
    public ShipShooter shipShooter;

    public void OnMove1(InputValue input)
    {
        Vector3 inputVec = input.Get<Vector2>();
        if (inputVec.magnitude > 0)
        {
            if(shipMover.NumberOfControllers < 2)
                shipMover.NumberOfControllers++;

            shipMover.MoveVec1 = new Vector3(inputVec.x, 0, inputVec.y);
        }
        else
        {
            shipMover.NumberOfControllers--;
            shipMover.MoveVec1 = Vector3.zero;
        }
    }

    public void OnFire1()
    {
        shipShooter.Fire1();
    }

    public void OnMove2(InputValue input)
    {
        Vector3 inputVec = input.Get<Vector2>();
        if (inputVec.magnitude > 0)
        {
            if (shipMover.NumberOfControllers < 2)
                shipMover.NumberOfControllers++;
            shipMover.MoveVec2 = new Vector3(inputVec.x, 0, inputVec.y);
        }
        else
        {
            shipMover.NumberOfControllers--;
            shipMover.MoveVec2 = Vector3.zero;
        }
    }

    public void OnFire2()
    {
        shipShooter.Fire2();
    }

    public void OnEscape()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
