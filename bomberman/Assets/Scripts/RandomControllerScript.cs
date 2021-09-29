using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomControllerScript : MonoBehaviour
{
    [SerializeField] private CharacterControllerScript player;
    [SerializeField] private float actionCooldown;
    [SerializeField] private float startCooldown;

    private float _currentStartCooldown;
    private float _currentActionCooldown;

    private void Start()
    {
        _currentActionCooldown = actionCooldown;
        _currentStartCooldown = startCooldown;
    }

    // Update is called once per frame
    private void Update()
    {
        if (_currentStartCooldown > 0)
        {
            _currentStartCooldown -= Time.deltaTime;
            return;
        }
        if (!player.gameObject.activeSelf) return;
        if (_currentActionCooldown > 0)
        {
            _currentActionCooldown -= Time.deltaTime;
            return;
        }

        _currentActionCooldown = actionCooldown;
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
