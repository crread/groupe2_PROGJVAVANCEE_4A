using System;
using System.Collections.Generic;
using System.Linq;
using Scrips;
using UnityEngine;

public class CharacterControllerScript : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
        
    [SerializeField] private Transform playerMesh;

    [SerializeField] private float playerSpeed;

    [SerializeField] private BombPoolManagerScript bombPool;

    [SerializeField] private float bombCooldown;

    public bool MctsIA { get; set; }

    public float CurrentBombCooldown;
    private MctsScript mcts;

    private void Start()
    {
        GameManagerScript.AddPlayer();

        if (MctsIA)
        {
            mcts = GameObject.Find("GameManager").GetComponent<MctsScript>();
        }
    }

    private void Update()
    { 
        if (MctsIA)
        {
            switch (mcts.GetNextMove())
            {                                                                                                                                                           
                case 'l':
                    MoveLeft();
                    break;
                case 'r':
                    MoveRight();
                    break;
                case 'u':
                    MoveForward();
                    break;
                case 'd':
                    MoveBackward();
                    break;
                case 'b':
                    PlaceBomb();
                    break;
                
            }
        }
        
        if (CurrentBombCooldown > 0)
        {
            CurrentBombCooldown -= Time.deltaTime;
            if (CurrentBombCooldown <= 0)
            {
                UI_HUD.SetBombAvailable();
            }
        }
    }

    public void MoveForward()
    {
        playerTransform.position += Vector3.forward * (Time.deltaTime * playerSpeed);
        playerMesh.rotation = Quaternion.AngleAxis(0, Vector3.up);
    }

    public void MoveBackward()
    {
        playerTransform.position -= Vector3.forward * (Time.deltaTime * playerSpeed);
        playerMesh.rotation = Quaternion.AngleAxis(180, Vector3.up);
    }

    public void MoveLeft()
    {
        playerTransform.position -= Vector3.right * (Time.deltaTime * playerSpeed);
        playerMesh.rotation = Quaternion.AngleAxis(-90, Vector3.up);
    }

    public void MoveRight()
    {
        playerTransform.position += Vector3.right * (Time.deltaTime * playerSpeed);
        playerMesh.rotation = Quaternion.AngleAxis(90, Vector3.up);
    }

    public void PlaceBomb()
    {
        if (CurrentBombCooldown > 0) return;
        CurrentBombCooldown = bombCooldown;
        var position = transform.position;
        var bombPosition = new Vector3(Mathf.Round(position.x), 0, Mathf.Round(position.z));
        if (bombPool.BombPool
            .Any(manager => manager.Bomb.transform.position == bombPosition && manager.Bomb.activeSelf))
        {
            return;
        }
        var pooled = bombPool.DePooling(out var bomb);
        if (!pooled) return;
        bomb.transform.position = bombPosition;
        bomb.SetActive(true);
        UI_HUD.SetBombUnavailable();
    }

    public void Death()
    {
        gameObject.SetActive(false);
        GameManagerScript.Instance.RemovePlayer();
    }
}