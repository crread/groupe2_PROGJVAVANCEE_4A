using System;
using System.Collections;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public static int PlayerCount { get; set; }
        
    private readonly WaitForSeconds _waitForPlayer = new WaitForSeconds(2);
        
    public static GameManagerScript instance;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            Debug.Log("Il existe deja une instance de GameManager");
        }
    }

    public void AddPlayer()
    {
        PlayerCount++;
    }

    public void RemovePlayer(int playerNumber)
    {
        PlayerCount--;
        StartCoroutine(WaitForPlayer());
        if (PlayerCount <= 0)
        {
            Draw();
        }

        if (PlayerCount == 1)
        {
            Victory(playerNumber);
        }
    }

    private void Draw()
    {
            
    }

    private void Victory(int playerNumber)
    {
            
    }

    private IEnumerator WaitForPlayer()
    {
        yield return _waitForPlayer;
    }
        
}