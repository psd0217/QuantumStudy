using Photon.Deterministic;

namespace Quantum
{
    public unsafe class ProjectileSystem: SystemMainThreadFilter<ProjectileSystem.Filter>
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
            filter.Transform->Position += filter.ProjectileSpec->MoveDirection.XOY * f.DeltaTime;
            //Log.Debug(filter.ProjectileSpec->Owner + "  " + filter.ProjectileSpec->Power);
        }
    }
}