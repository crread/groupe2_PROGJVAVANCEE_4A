using System;
using UnityEngine;

//Collision with the explosion of the bomb
public class ExplosionColliderScript : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        //collision with the player
        if (other.gameObject.CompareTag("Player"))
        {
            var characterController = other.gameObject.GetComponentInParent<CharacterControllerScript>();
            characterController.Death();
        }
        //Collision with a breakable block
        if (other.gameObject.CompareTag("Box"))
        {
            other.gameObject.SetActive(false);
        }
        //Collision with another bomb for chain reaction
        if (!other.gameObject.CompareTag("Bomb")) return;
        var bomb = other.gameObject.GetComponentInParent<BombScript>();
        bomb.Trigger();
    }
}