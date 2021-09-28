using System;
using System.Collections.Generic;
using UnityEngine;

internal readonly struct BombManager
{
    public GameObject Bomb { get; }
    public BombScript BombScript { get; }
        

    public BombManager(GameObject bomb)
    {
        Bomb = bomb;
        Bomb.SetActive(false);
        BombScript = Bomb.GetComponent<BombScript>();
        BombScript.Pooled = false;
    }
}

public class BombPoolManagerScript : MonoBehaviour
{
    [SerializeField] private int length;

    private readonly BombManager[] _bombPool = new BombManager[10];

    [SerializeField] private GameObject bombPrefab;

    // Start is called before the first frame update
    void Start()
    {
        for (var i = 0; i < length; i++)
        {
            _bombPool[i] = new BombManager(Instantiate(bombPrefab));
        }
    }

    public GameObject DePooling()
    {
        for (var i = 0; i < length; i++)
        {
            var bombManager = _bombPool[i];
            if (!bombManager.BombScript.Pooled)
            {
                bombManager.BombScript.Pooled = true;
                return bombManager.Bomb;
            }
        }
        return default;
    }
        
}