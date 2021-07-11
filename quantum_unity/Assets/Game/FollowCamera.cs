using System;
using System.Collections;
using System.Collections.Generic;
using Quantum;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{

    public Transform target;
    public int cameraY = 10;

     private void Update()
     {
        if (target == null)
            return;

        transform.position = new Vector3(target.position.x, cameraY, target.position.z);
     }
}
