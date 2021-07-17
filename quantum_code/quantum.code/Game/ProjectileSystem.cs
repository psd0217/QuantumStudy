using Photon.Deterministic;

namespace Quantum
{
    public unsafe class ProjectileSystem: SystemMainThreadFilter<ProjectileSystem.Filter>, ISignalOnTriggerEnter3D
    {
        public struct Filter
        {
            public EntityRef Entity;
            public Transform3D* Transform;
            public Projectile* Projectile;
            public ProjectileSpec* ProjectileSpec;
        }

        public override void Update(Frame f, ref Filter filter)
        {
            filter.Transform->Position += filter.ProjectileSpec->MoveDirection.XOY * f.DeltaTime * FP._8;

            if (FPVector2.Distance(filter.Transform->Position.XZ, filter.ProjectileSpec->StartPosition.XY) > filter.ProjectileSpec->Distance)
                f.Destroy(filter.Entity);


            //Log.Debug(filter.ProjectileSpec->Owner + "  " + filter.ProjectileSpec->Power);
        }


        public void OnTriggerEnter3D(Frame frame, TriggerInfo3D info)
        {
            if(frame.Has<ProjectileSpec>(info.Entity) == false) return;
            if(frame.Has<PlayerSpec>(info.Other) == false) return;
            

            var projectileSpec = frame.Unsafe.GetPointer<ProjectileSpec>(info.Entity);
            var playerSpec = frame.Unsafe.GetPointer<PlayerSpec>(info.Other);
            playerSpec->HP -= projectileSpec->Power;
            
            Log.Debug("Trigger hp : " +  playerSpec->HP);
            
            frame.Destroy(info.Entity);
            if (playerSpec->HP <= FP._0)
            {
                frame.Destroy(info.Other);
            }
        }
    }
}