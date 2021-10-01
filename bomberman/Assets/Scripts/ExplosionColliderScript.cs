using System;
using UnityEngine;

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

        if (!other.gameObject.CompareTag("Bomb")) return;
        
        var bomb = other.gameObject.GetComponentInParent<BombScript>();
        bomb.Trigger();
    }
}