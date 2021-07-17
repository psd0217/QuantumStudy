using System;
using System.Collections;
using System.Collections.Generic;
using Quantum;
using UnityEngine;

public class WeaponSpawner : MonoBehaviour
{
    [SerializeField] private EntityPrototypeAsset projectilePrototype = null;
    //private PlayerRef _playerRef;

    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        //QuantumEvent.Subscribe<EventCreateProjectile>(listener: this, handler: CreateProjectile);
    }

    private void CreateProjectile(EventCreateProjectile eventCreate)
    {
        Debug.Log("CreateProjectile : " + eventCreate);
        SpawnProjectile();
    }
    

    public void SpawnProjectile() {
        //CommandSpawnProjectile command = new CommandSpawnProjectile()
        //{
        //    projectilePrototypeGUID = projectilePrototype.Settings.Guid.Value,
        //};
        //QuantumRunner.Default.Game.SendCommand(command);
    }
}
