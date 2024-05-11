using System;
using _SCRIPTS.Extensions;
using UnityEngine;
using UnityEngine.Events;

namespace _SCRIPTS.Signals
{
    public class InputSignals : MonoSingleton<InputSignals>
    {
        //public UnityAction<AttackCombo> OnSwordAttack= delegate {  };
        public UnityAction OnArchering = delegate {  };
        public UnityAction CanDash =delegate {  };
        public Func<bool> OnGetCanDash = () => false;
        public UnityAction<float,float> OnMovementAndRotation=delegate {  };
        //public Func<AnimationStates> OnGettingAnimationState=delegate { return AnimationStates.Idle; };
        public Func<Animator> OnGettingAnimator = () => null;
    }
}
