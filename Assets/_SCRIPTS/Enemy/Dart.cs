using System;
using System.Collections.Generic;
using System.Collections;
using _SCRIPTS.Signals;
using UnityEngine;
using UnityEngine;

namespace _SCRIPTS.Enemy
{
    public class Dart : MonoBehaviour
    {
        private void Start()
        {
            StartCoroutine(SetInActiceDart());
        }
        
        private IEnumerator SetInActiceDart()
        {
            yield return new WaitForSeconds(5);
            gameObject.SetActive(false);
            
        }
    }
}
