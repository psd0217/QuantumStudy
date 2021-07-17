

using Photon.Deterministic;

namespace Quantum
{
	public unsafe class WeaponSystem : SystemMainThreadFilter<WeaponSystem.Filter>
	{
        private static readonly string PROJECTILE_PROTOTYPE = "Resources/DB/Projectile|EntityPrototype";
        private static readonly string WEAPON_PROTOTYPE = "Resources/DB/Weapon{0}|EntityPrototype";
        public struct Filter
		{
			public EntityRef Entity;
			public Transform3D* Transform;
			public WeaponSpec* Weapon;
		}

		public override void Update(Frame f, ref Filter filter)
		{

            var attacker = f.Unsafe.GetPointer<PlayerSpec>(filter.Weapon->Attacker);
            var playerTransform = f.Unsafe.GetPointer<Transform3D>(filter.Weapon->Attacker);

            filter.Transform->Position = playerTransform->Position;
            filter.Transform->Rotation = playerTransform->Rotation;


            if (filter.Weapon->LastAttackTime + filter.Weapon->AttackSpeed <= f.ElapsedTime && attacker->Shot)
            {
                //Log.Debug("create projectile! " + f.ElapsedTime);
                //생성
                var projectilePrototype = f.FindAsset<EntityPrototype>(PROJECTILE_PROTOTYPE);
                var projectileCreation = f.Create(projectilePrototype);
                //총알 스팩
                f.Add<ProjectileSpec>(projectileCreation);
                var projectileSpec = f.Unsafe.GetPointer<ProjectileSpec>(projectileCreation);
                projectileSpec->Attacker = filter.Entity;
                projectileSpec->Power = FP._1;
                projectileSpec->Distance = FP._10;
                projectileSpec->MoveDirection = attacker->RotateDirection;

                //총알 위치
                var transform = f.Unsafe.GetPointer<Transform3D>(projectileCreation);
                transform->Position = filter.Transform->Position + attacker->RotateDirection.XOY;
                projectileSpec->StartPosition = transform->Position.XZ;

                filter.Weapon->LastAttackTime = f.ElapsedTime;
            }


            if (filter.Weapon->CurrentWeaponSlot != attacker->CurrentWeaponSlot)
            {
                Log.Debug("Change Weapon!! " + filter.Weapon->CurrentWeaponSlot + "->" + attacker->CurrentWeaponSlot);

                var weaponPrototype = f.FindAsset<EntityPrototype>(string.Format(WEAPON_PROTOTYPE, attacker->CurrentWeaponSlot.ToString("00")));
                var weaponCreation = f.Create(weaponPrototype);
                WeaponSpec* weaponSpec = f.Unsafe.GetPointer<WeaponSpec>(weaponCreation);
                //weaponSpec->LastAttackTime = f.ElapsedTime;
                //weaponSpec->Power = FP._1;
                weaponSpec->Attacker = filter.Weapon->Attacker;
                weaponSpec->CurrentWeaponSlot = attacker->CurrentWeaponSlot;

                f.Destroy(filter.Entity);
            }

            

        }
		
    }
}