using Photon.Deterministic;

namespace Quantum
{
    public class CommandSpawnProjectile : DeterministicCommand
    {
        public long projectilePrototypeGUID;
        
        public override void Serialize(BitStream stream)
        {
            stream.Serialize(ref projectilePrototypeGUID);
        }

        public void Execute(Frame f)
        {
            var projectilePrototype = f.FindAsset<EntityPrototype>(projectilePrototypeGUID);
            var entity = f.Create(projectilePrototype);
        }
    }
}