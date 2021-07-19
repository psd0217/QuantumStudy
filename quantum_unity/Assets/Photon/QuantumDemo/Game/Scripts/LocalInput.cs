using System;
using Photon.Deterministic;
using Quantum;
using UnityEngine;
using BitStream = Photon.Deterministic.BitStream;

public class LocalInput : MonoBehaviour
{

  public CNJoystick leftStick;
  public CNJoystick rightStick;

  public int SelectedWeaponSlot;
    public FPVector2 moveDirection;
    public FPVector2 rotateDirection;
    public bool isAttack;

  //private float _rotateX;
  //private float _rotateY;
  
  private void OnEnable() {
    QuantumCallback.Subscribe(this, (CallbackPollInput callback) => PollInput(callback));
  }

  public void PollInput(CallbackPollInput callback) {
    Quantum.Input i = new Quantum.Input();

        //var moveX = leftStick.GetAxis(leftStick.AxisNameX);
        //var moveY = leftStick.GetAxis(leftStick.AxisNameY);

        //if (rightStick.GetAxis(rightStick.AxisNameX) != 0 || rightStick.GetAxis(rightStick.AxisNameY) != 0)
        //{
        //  _rotateX = rightStick.GetAxis(rightStick.AxisNameX);
        //  _rotateY = rightStick.GetAxis(rightStick.AxisNameY);
        //  i.isAttack = true;
        //}
        //else
        //{
        //  i.isAttack = false;
        //}
        i.isAttack = isAttack;
        i.MoveDirection = moveDirection;// new Vector2(moveX, moveY).ToFPVector2();
        i.RotateDirection = rotateDirection;// new Vector2(_rotateX, _rotateY).ToFPVector2();

        i.currentWeaponSlot = SelectedWeaponSlot;
    
    callback.SetInput(i, DeterministicInputFlags.Repeatable);
  }
}
