using System;
using UnityEngine;

namespace Scrips
{
    public class BombColliderScript : MonoBehaviour
    {
        public void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            var component = GetComponent<Collider>();
            component.isTrigger = false;
        }
    }
}
