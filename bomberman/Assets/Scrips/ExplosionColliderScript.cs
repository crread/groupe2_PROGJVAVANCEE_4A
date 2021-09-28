using System;
using UnityEngine;

namespace Scrips
{
    public class ExplosionColliderScript : MonoBehaviour
    {
        public void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                var characterController = other.gameObject.GetComponentInParent<CharacterControllerScript>();
                characterController.Death();
            }

            if (other.gameObject.CompareTag("Box"))
            {
                other.gameObject.SetActive(false);
            }
        }
    }
}
