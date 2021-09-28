using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomControllerScript : MonoBehaviour
{
    [SerializeField] private CharacterControllerScript player;

    // Update is called once per frame
    private void Update()
    {
        if (Time.timeScale == 0f)
        {
            return;
        }
        switch (Random.Range(1, 6))
        {
            case 1: 
                player.MoveForward();
                break;
            case 2:
                player.MoveBackward();
                break;
            case 3:
                player.MoveLeft();
                break;
            case 4:
                player.MoveRight();
                break;
            case 5:
                player.PlaceBomb();
                break;
        }
    }
}
