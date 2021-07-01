using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Photon.Deterministic;

namespace Quantum {
  unsafe partial class Frame {
    public FP ElapsedTime {
      get {
        return DeltaTime * (Number - SessionConfig.RollbackWindow);
      }
    }
  }
}
