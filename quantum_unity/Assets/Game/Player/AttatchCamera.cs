using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Quantum;

public class AttatchCamera : MonoBehaviour
{
    void Start()
    {
        EntityView playerView = this.GetComponent<EntityView>();

        Quantum.Frame frame = QuantumRunner.Default.Game.Frames.Verified;

        var player = frame.Get<PlayerLink>(playerView.EntityRef);

        if (player.Player == QuantumRunner.Default.Game.GetLocalPlayers()[0])
        {
            if (CameraManager.GetInstance() != null)
            {
                FollowCamera follow = CameraManager.GetInstance().mainCamera.gameObject.GetComponent<FollowCamera>();
                follow.target = this.transform;
            }
        }
    }

    
}
