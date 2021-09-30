using System;
using System.Linq;
using UnityEngine;


public class CharacterControllerScript : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
        
    [SerializeField] private Transform playerMesh;

    [SerializeField] private float playerSpeed;

    [SerializeField] private BombPoolManagerScript bombPool;

    //cooldown between placing 2 bombs
    [SerializeField] private float bombCooldown;

    private float _currentBombCooldown;

    private void Start()
    {
        GameManagerScript.AddPlayer();
    }

    private void Update()
    {
        
        //update bomb cooldown
        if (_currentBombCooldown > 0)
        {
            _currentBombCooldown -= Time.deltaTime;
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
        if (_currentBombCooldown > 0) return;
        _currentBombCooldown = bombCooldown;
        var position = transform.position;
        //position the bomb at the center of the tile
        var bombPosition = new Vector3(Mathf.Round(position.x), 0, Mathf.Round(position.z));
        //check if a bomb is already on the tile
        if (bombPool.BombPool
            .Any(manager => manager.Bomb.transform.position == bombPosition && manager.Bomb.activeSelf))
        {
            return;
        }
        //try to retrieve a bomb from the pool
        var pooled = bombPool.DePooling(out var bomb);
        if (!pooled) return;
        //place the bomb and active it
        bomb.transform.position = bombPosition;
        bomb.SetActive(true);
    }

    public void Death()
    {
        gameObject.SetActive(false);
        GameManagerScript.Instance.RemovePlayer();
    }
}