using Photon.Deterministic;

namespace Quantum
{
    public class CallbackSystem : SystemSignalsOnly, ISignalOnDamage
    {
        public void OnDamage(Frame f, FP damage, EntityRef entity)
        {
            Log.Debug("damage : " + damage);
            //f.Unsafe.GetPointer<TriggerInfo3D>(entity);
        }
    }
}