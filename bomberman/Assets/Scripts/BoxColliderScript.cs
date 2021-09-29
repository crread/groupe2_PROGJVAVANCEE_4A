using System;
using UnityEngine;
using UnityEngine.Events;

namespace Scrips
{
    public class BoxColliderScript : MonoBehaviour
    {
        [SerializeField]
        private UnityEvent addPlayerPosition = new UnityEvent();
        
        [SerializeField]
        private UnityEvent removePlayerPosition = new UnityEvent();
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                addPlayerPosition.Invoke();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                removePlayerPosition.Invoke();
            }
        }
    }
}
