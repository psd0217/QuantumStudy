/**
 * This code was auto-generated by a tool, every time
 * the tool executes this code will be reset.
 **/

using System;
namespace Quantum.Prototypes.Unity {
  [System.Serializable]
  public class ProjectileSpec_Prototype : Quantum.IPrototypeAdapter<Quantum.Prototypes.ProjectileSpec_Prototype> {
    public Quantum.PlayerRef Owner;
    public Photon.Deterministic.FP Power;
    [Quantum.LocalReference]
    public global::EntityPrototype Attacker;

    public Quantum.Prototypes.ProjectileSpec_Prototype Convert(EntityPrototypeConverter converter) {
      var result = new Quantum.Prototypes.ProjectileSpec_Prototype();
      result.Owner = this.Owner;
      result.Power = this.Power;
      converter.Convert(this.Attacker, out result.Attacker);
      return result;
    }
  }

}
