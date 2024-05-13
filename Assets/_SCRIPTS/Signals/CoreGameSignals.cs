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
        public UnityAction OnIncreaseSoulMeter = delegate {  };
        public Func<int> OnGetKillCount = () => 0;
        public UnityAction OnIncreaseKillCount = delegate {  };
        public UnityAction OnScoreManagement = delegate {  };
        public Func<int> OnGetScore = () => 0;
        public UnityAction Die = delegate {  };


        public UnityAction OnSetGetAttack = delegate {  };
        public UnityAction OnSetIsExitTrue = delegate {  };
        public UnityAction OnSetIsExitFalse = delegate {  };
        
        //public UnityAction<MainMenuStates> OnMainMenuUIManagement = delegate {  };
    }
}
