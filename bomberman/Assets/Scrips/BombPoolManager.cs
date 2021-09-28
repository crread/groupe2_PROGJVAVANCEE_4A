using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scrips
{
    internal struct BombManager
    {
        public GameObject Bomb { get; }
        public bool Pooled { get; set; }

        public BombManager(GameObject bomb, int index, bool pooled, BombPoolManager poolManager)
        {
            Bomb = bomb;
            Bomb.SetActive(false);
            Bomb.GetComponent<BombScript>().PoolManager = poolManager;
            Pooled = pooled;
        }
    }

    public class BombPoolManager : MonoBehaviour
    {
        [SerializeField] private int length;

        [SerializeField] private int lastPooled;

        private readonly BombManager[] _bombPool = new BombManager[10];

        [SerializeField] private GameObject bombPrefab;

        // Start is called before the first frame update
        void Start()
        {
            for (var i = 0; i < length; i++)
            {
                _bombPool[i] = new BombManager(Instantiate(bombPrefab), i, false, this);
            }
        }

        public GameObject Pooling()
        {
            if (lastPooled == length - 1)
            {
                lastPooled = 0;
            }

            for (var i = 0; i < length; i++)
            {
                var bombManager = _bombPool[lastPooled];
                if (!bombManager.Pooled)
                {
                    Debug.Log("bomb pooled");
                    bombManager.Pooled = true;
                    lastPooled++;
                    return bombManager.Bomb;
                }

                lastPooled++;
            }
            return null;
        }
    }
}
