using System;
using _SCRIPTS.Enums;
using _SCRIPTS.Extensions;
using UnityEngine;
using UnityEngine.Events;

namespace _SCRIPTS.Signals
{
    public class CoreGameSignals : MonoSingleton<CoreGameSignals>
    {
        public Func<Vector3> OnGetMovementDirection= () => default;
        public Func<bool> OnGetCanAttack= () => default;
        public UnityAction OnResetSoulMeter = delegate {  };
        public Func<int> OnGetKillCount = () => 0;
        public UnityAction OnIncreaseKillCount = delegate {  };
        public UnityAction OnIncreaseScore = delegate {  };
        public UnityAction<float> OnIncreaseSoulMeter = delegate {  };
        public Func<int> OnGetScore = () => 0;
        public UnityAction OnDie = delegate {  };
        public UnityAction OnAttack = delegate {  };
        public UnityAction OnDash = delegate {  };
        public UnityAction OnRestart = delegate {  };
        public UnityAction OnScoreUIManagement = delegate {  };


        public UnityAction OnSetGetAttack = delegate {  };
        public UnityAction OnSetIsExitTrue = delegate {  };
        public UnityAction OnSetIsExitFalse = delegate {  };
        public Func< string, Vector3, Quaternion, GameObject> OnSpawnFromPool = (o, s, arg3) => default;
    }
}
