using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerManager : MonoBehaviour
{
	public CNJoystick leftStick;
	public CNJoystick rightStick;

    public LocalInput QuantumInput;

    private float _rotateX;
    private float _rotateY;

    private void Update()
	{
        var moveX = leftStick.GetAxis(leftStick.AxisNameX);
        var moveY = leftStick.GetAxis(leftStick.AxisNameY);

        if (rightStick.GetAxis(rightStick.AxisNameX) != 0 || rightStick.GetAxis(rightStick.AxisNameY) != 0)
        {
            _rotateX = rightStick.GetAxis(rightStick.AxisNameX);
            _rotateY = rightStick.GetAxis(rightStick.AxisNameY);
            QuantumInput.isAttack = true;
        }
        else
        {
            QuantumInput.isAttack = false;
        }

        QuantumInput.moveDirection = new Vector2(moveX, moveY).ToFPVector2();
        QuantumInput.rotateDirection = new Vector2(_rotateX, _rotateY).ToFPVector2();
    }


}
