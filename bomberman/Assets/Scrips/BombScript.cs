using UnityEngine;

namespace Scrips
{
    public class BombScript : MonoBehaviour
    {
        public BombPoolManager PoolManager { get; set; }

        [SerializeField] private float explosionCooldown;

        private float _currentCooldown;

        // Start is called before the first frame update
        void OnEnable()
        {
            _currentCooldown = explosionCooldown;
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}
