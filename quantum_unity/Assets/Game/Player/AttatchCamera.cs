using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttatchCamera : MonoBehaviour
{
    void Start()
    {
        EntityComponentPlayerLink player = this.GetComponent<EntityComponentPlayerLink>();
        if (player.Prototype.Player == QuantumRunner.Default.Game.GetLocalPlayers()[0])
        {
            Debug.Log(player.Prototype.Player + "    " + QuantumRunner.Default.Game.GetLocalPlayers()[0]);
            FollowCamera follow = Camera.main.gameObject.GetComponent<FollowCamera>();
            follow.target = this.transform;
        }
    }

    
}
