using UnityEngine;

namespace _SCRIPTS.UI
{
    public class GhostUIAnimation : MonoBehaviour
    {
        #region Serialize Field

        [SerializeField] private Animator animator;

        #endregion

        #region Awake

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            animator.Play("Ghost");
        }

        #endregion
    
    }
}
