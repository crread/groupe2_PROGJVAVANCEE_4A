
using System.Collections;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    private static int PlayerCount { get; set; }

    private bool _anotherDeath = false;

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
        PlayerCount++;
    }

    public void RemovePlayer()
    {
        PlayerCount--;
        
        if (PlayerCount <= 0)
        {
            Instance._anotherDeath = true;
            Draw();
            return;
        }
        StartCoroutine(WaitForPlayer());
        if (PlayerCount != 1) return;
        if (Instance._anotherDeath) return;
        
        Victory();
    }

    private static void Draw()
    {
        Debug.Log("Draw !");
        Time.timeScale = 0f;
    }

    private static void Victory()
    {
        Debug.Log("victory !");
        Time.timeScale = 0f;
    }

    private IEnumerator WaitForPlayer()
    {
        yield return _waitForPlayer;
    }
        
}