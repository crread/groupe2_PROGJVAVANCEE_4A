using System;
using System.Collections.Generic;
using UnityEngine;

public readonly struct BombManager
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

    public BombManager[] BombPool { get; set; }

    [SerializeField] private GameObject bombPrefab;

    // Start is called before the first frame update
    void Start()
    {
        BombPool = new BombManager[length];
        for (var i = 0; i < length; i++)
        {
            BombPool[i] = new BombManager(Instantiate(bombPrefab));
        }
    }

    public bool DePooling(out GameObject bomb)
    {
        for (var i = 0; i < length; i++)
        {
            var bombManager = BombPool[i];
            if (bombManager.BombScript.Pooled) continue;
            bombManager.BombScript.Pooled = true;
            bomb =  bombManager.Bomb;
            return true;
        }
        bomb = null;
        return false;
    }
        
}