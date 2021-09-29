
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

    public static void AddPlayer()
    {
        _playerCount++;
    }

    public void RemovePlayer()
    {
        _playerCount--;
        
        if (_playerCount <= 0)
        {
            Instance._anotherDeath = true;
            Draw();
            return;
        }

        if (_playerCount > 1) return;
        if (_playerCount != 1) return;
        StartCoroutine(WaitForPlayer());
        if (Instance._anotherDeath)
        {
            return;
        }
        Victory();

    }

    private static void Draw()
    {
        Debug.Log("Draw !");
        Time.timeScale = 0f;
        EndGameHandle.DrawScreen();
    }

    private static void Victory()
    {
        Debug.Log("victory !");
        Time.timeScale = 0f;
        EndGameHandle.VictoryScreen();
    }

    private IEnumerator WaitForPlayer()
    {
        yield return _waitForPlayer;
    }
        
}