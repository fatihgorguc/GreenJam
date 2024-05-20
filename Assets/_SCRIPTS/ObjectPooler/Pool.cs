using Unity.VisualScripting;
using UnityEngine;

namespace _SCRIPTS.ObjectPooler
{
    [System.Serializable]
    public class Pool 
    {
        #region Serialize Field

        [SerializeField] private string pooltag;
        [SerializeField] private GameObject prefab;
        [SerializeField] private int size;

        #endregion

        #region Public Field

        public string PoolTag => pooltag;
        public GameObject Prefab => prefab;
        public int Size => size;

        #endregion




    }
}
