﻿using System;
using System.Diagnostics;
using Photon.Deterministic;

namespace Quantum
{
    public unsafe class MovementSystem : SystemMainThreadFilter<MovementSystem.Filter>, ISignalOnPlayerDataSet
    {
        private static readonly string PROJECTILE_PROTOTYPE = "Resources/DB/Projectile|EntityPrototype";
        
        public struct Filter
        {
            public EntityRef Entity;
            public CharacterController3D* KCC;
            public Transform3D* Transform;
            public PlayerLink* Link;
        }

        private double createProjectileTime = 1000;
        private double createProjectileTimer = 0;
        
        public override void Update(Frame f, ref Filter filter)
        {
            
            var input = f.GetPlayerInput(filter.Link->Player);

            if (input->MoveDirection.SqrMagnitude > 1)
            {
                input->MoveDirection = input->MoveDirection.Normalized;
            }
            filter.KCC->Move(f, filter.Entity, input->MoveDirection.XOY);
            
            if (input->RotateDirection.SqrMagnitude > 1)
            {
                input->RotateDirection = input->RotateDirection.Normalized;
            }

            if (input->RotateDirection != default)
            {
                filter.Transform->Rotation = FPQuaternion.LookRotation(input->RotateDirection.XOY);
            }

            createProjectileTimer += GetDeltaTime();
            if (createProjectileTimer >= createProjectileTime && input->isAttack)
            {
                var projectilePrototype = f.FindAsset<EntityPrototype>(PROJECTILE_PROTOTYPE);
                var entity = f.Create(projectilePrototype);
               // f.Events.CreateProjectile();
                createProjectileTimer = 0;
            }
            
            // for (int i = 0; i < f.PlayerCount; i++)
            // {
            //     var command = f.GetPlayerCommand(i) as CommandSpawnProjectile;
            //     command?.Execute(f);
            // }
        }

        public void OnPlayerDataSet(Frame f, PlayerRef player)
        {
            var data = f.GetPlayerData(player);
            var prototype = f.FindAsset<EntityPrototype>(data.CharacterPrototype.Id);
            var e = f.Create(prototype);
            
            //var inputDirection = FPVector2.Zero;

            PlayerLink* playerLink = f.Unsafe.GetPointer<PlayerLink>(e);
            Transform3D* transform3D = f.Unsafe.GetPointer<Transform3D>(e);
            playerLink->Player = player;

           //최초 위치 값
            transform3D->Position.X = player._index;
            Log.Debug("OnPlayerDataSet entity : " + e + " player : " + player + " position : " + transform3D->Position + " position.x : " + transform3D->Position.X);
            
        }

        private DateTime lastTime = DateTime.Now;
        double GetDeltaTime()
        {
            double dT = (DateTime.Now - lastTime).Milliseconds; // / 1000
            lastTime = DateTime.Now;
            return dT;
        }
    }
    
}