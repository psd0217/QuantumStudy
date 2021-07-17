using System;
using System.Diagnostics;
using Photon.Deterministic;

namespace Quantum
{
    public unsafe class MovementSystem : SystemMainThreadFilter<MovementSystem.Filter>, ISignalOnPlayerDataSet
    {
        private static readonly string WEAPON_PROTOTYPE = "Resources/DB/Weapon00|EntityPrototype";

        public struct Filter
        {
            public EntityRef Entity;
            public CharacterController3D* KCC;
            public Transform3D* Transform;
            public PlayerLink* Link;
            public PlayerSpec* Spec;
        }

        
        
        public override void Update(Frame f, ref Filter filter)
        {
            
            var input = f.GetPlayerInput(filter.Link->Player);

            if (input->MoveDirection.SqrMagnitude > 1)
            {
                input->MoveDirection = input->MoveDirection.Normalized;
            }
            filter.KCC->Move(f, filter.Entity, input->MoveDirection.XOY);
            
            // if (input->RotateDirection.SqrMagnitude > 1)
            // {
                 input->RotateDirection = input->RotateDirection.Normalized;
            // }

            if (input->RotateDirection != default)
            {
                filter.Transform->Rotation = FPQuaternion.LookRotation(input->RotateDirection.XOY);
            }

            filter.Spec->Shot = input->isAttack;
            filter.Spec->RotateDirection = input->RotateDirection;

            if(filter.Spec->CurrentWeaponSlot != input->currentWeaponSlot)
			{
                filter.Spec->CurrentWeaponSlot = input->currentWeaponSlot;
            }

        }

        public void OnPlayerDataSet(Frame f, PlayerRef player) //플레이어 초기값 셋팅
        {
            var data = f.GetPlayerData(player);
            var prototype = f.FindAsset<EntityPrototype>(data.CharacterPrototype.Id);
            var e = f.Create(prototype);
            
            //var inputDirection = FPVector2.Zero;

            PlayerLink* playerLink = f.Unsafe.GetPointer<PlayerLink>(e);
            Transform3D* transform3D = f.Unsafe.GetPointer<Transform3D>(e);
            PlayerSpec* playerSpec = f.Unsafe.GetPointer<PlayerSpec>(e);
            playerLink->Player = player;
            playerSpec->HP = FP._10;
            playerSpec->CurrentWeaponSlot = 0;

            var weaponPrototype = f.FindAsset<EntityPrototype>(WEAPON_PROTOTYPE);
            var weaponCreation = f.Create(weaponPrototype);
            Transform3D* weaponTransform = f.Unsafe.GetPointer<Transform3D>(weaponCreation);
            weaponTransform->Position = transform3D->Position;
            weaponTransform->Rotation = transform3D->Rotation;
            WeaponSpec* weaponSpec = f.Unsafe.GetPointer<WeaponSpec>(weaponCreation);
            //weaponSpec->LastAttackTime = f.ElapsedTime;
            //weaponSpec->Power = FP._1;
            weaponSpec->Attacker = e;
            weaponSpec->CurrentWeaponSlot = 0;
            //최초 위치 값
            transform3D->Position.X = f.RNG->Next(-70, 70);
           transform3D->Position.Z = f.RNG->Next(-70, 70);
            Log.Debug("OnPlayerDataSet entity : " + e + " player : " + player + " position : " + transform3D->Position + " position.x : " + transform3D->Position.X);
            
        }

    }
    
}