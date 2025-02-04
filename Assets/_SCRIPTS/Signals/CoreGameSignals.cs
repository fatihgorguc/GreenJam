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
        
        
        public UnityAction OnSetGetAttack = delegate {  };
        
        //public UnityAction<MainMenuStates> OnMainMenuUIManagement = delegate {  };
    }
}
