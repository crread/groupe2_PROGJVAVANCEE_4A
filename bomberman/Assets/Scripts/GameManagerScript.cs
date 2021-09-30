
using System.Collections;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    private static int _playerCount;

    private bool _anotherDeath;

    private readonly WaitForSeconds _waitForPlayer = new WaitForSeconds(1f);
        
    public static GameManagerScript Instance;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            Debug.Log("Il existe deja une instance de GameManager");
        }
        
    }

    //function to update the player count
    public static void AddPlayer()
    {
        _playerCount++;
    }

    //remove a player from the list and check remaining players for the end of the game
    public void RemovePlayer()
    {
        _playerCount--;
        //no more player = draw
        if (_playerCount <= 0)
        {
            Instance._anotherDeath = true;
            Draw();
            return;
        }
        
        if (_playerCount > 1) return;
        //only 1 player remaining = victory
        if (_playerCount != 1) return;
        StartCoroutine(WaitForPlayer());
        

    }

    private static void Draw()
    {
        _playerCount = 0;
        Debug.Log("Draw !");
        Time.timeScale = 0f;
        EndGameHandle.DrawScreen();
    }

    private static void Victory()
    {
        _playerCount = 0;
        Debug.Log("victory !");
        Time.timeScale = 0f;
        EndGameHandle.VictoryScreen();
    }

    private IEnumerator WaitForPlayer()
    {
        yield return _waitForPlayer;
        if (Instance._anotherDeath)
        {
            yield break;
        }
        Victory();
    }
        
}