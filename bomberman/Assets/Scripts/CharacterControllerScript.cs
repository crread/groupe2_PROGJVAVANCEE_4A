using System;
using UnityEngine;

public class CharacterControllerScript : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
        
    [SerializeField] private Transform playerMesh;

    [SerializeField] private int playerNumber;

    [SerializeField] private float playerSpeed;

    [SerializeField] private BombPoolManagerScript bombPool;

    private void Start()
    {
        GameManagerScript.instance.AddPlayer();
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
        var bomb = bombPool.DePooling();
        var position = transform.position;
        bomb.transform.position = new Vector3(Mathf.Round(position.x), 0, Mathf.Round(position.z));
        bomb.SetActive(true);
    }

    public void Death()
    {
        gameObject.SetActive(false);
        GameManagerScript.instance.RemovePlayer(playerNumber);
    }
}