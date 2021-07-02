using System;
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

            // f.DeltaTime 지역변수 받아쓰면 안됨!!!!
            if (filter.Link->LastAttackTime + 1 <= f.ElapsedTime && input->isAttack)
            {
                Log.Debug("create projectile! " + f.ElapsedTime);
                //생성
                var projectilePrototype = f.FindAsset<EntityPrototype>(PROJECTILE_PROTOTYPE);
                var projectileCreation = f.Create(projectilePrototype);
                //총알 스팩
                f.Add<ProjectileSpec>(projectileCreation);
                var projectileSpec = f.Unsafe.GetPointer<ProjectileSpec>(projectileCreation);
                projectileSpec->Attacker = filter.Entity;
                projectileSpec->Owner = filter.Link->Player;
                projectileSpec->Power = FP._1;
                projectileSpec->MoveDirection = input->RotateDirection;
                //총알 위치
                var transform = f.Unsafe.GetPointer<Transform3D>(projectileCreation);
                transform->Position = filter.Transform->Position + input->RotateDirection.XOY;
               // f.Events.CreateProjectile();
                //createProjectileTimer = 0;
                filter.Link->LastAttackTime = f.ElapsedTime;
            }
            
            // for (int i = 0; i < f.PlayerCount; i++)
            // {
            //     var command = f.GetPlayerCommand(i) as CommandSpawnProjectile;
            //     command?.Execute(f);
            // }
        }

        public void OnPlayerDataSet(Frame f, PlayerRef player) //플레이어 초기값 셋팅
        {
            var data = f.GetPlayerData(player);
            var prototype = f.FindAsset<EntityPrototype>(data.CharacterPrototype.Id);
            var e = f.Create(prototype);
            
            //var inputDirection = FPVector2.Zero;

            PlayerLink* playerLink = f.Unsafe.GetPointer<PlayerLink>(e);
            Transform3D* transform3D = f.Unsafe.GetPointer<Transform3D>(e);
            playerLink->Player = player;
            playerLink->LastAttackTime = f.ElapsedTime;
            playerLink->HP = FP._10;

           //최초 위치 값
            transform3D->Position.X = player._index;
            Log.Debug("OnPlayerDataSet entity : " + e + " player : " + player + " position : " + transform3D->Position + " position.x : " + transform3D->Position.X);
            
        }

        // 퀀텀은 결정론적 방식을 사용함으로 롤백이 일어나기때문에 시스템 시간을 사용하면 안된다!
        // private DateTime lastTime = DateTime.Now;
        // double GetDeltaTime()
        // {
        //     double dT = (DateTime.Now - lastTime).Milliseconds; // / 1000
        //     lastTime = DateTime.Now;
        //     return dT;
        // }
    }
    
}