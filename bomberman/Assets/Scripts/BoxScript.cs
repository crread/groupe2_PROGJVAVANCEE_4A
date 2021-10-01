using UnityEngine;

namespace Scrips
{
    public class BoxScript : MonoBehaviour
    {
        [SerializeField]
        private GameObject balise;
        
        [SerializeField]
        private GameObject box;
         
        [SerializeField]
        private GameObject hitbox;
        
        [SerializeField]
        private bool boxActive;
        
        [SerializeField]
        private bool baliseActive;

        public bool playerOn;
        public Vector3 currentPositionBox;
        
        void Start()
        {
            balise.SetActive(baliseActive);

            if (!hitbox.GetComponent<BoxCollider>().isTrigger)
            {
                hitbox.GetComponent<BoxCollider>().isTrigger = true;
            }
            box.SetActive(boxActive);

            currentPositionBox = transform.position;
        }

        public void AddPlayerOn()
        {
            playerOn = true;
        }

        public void RemovePlayerOn()
        {
            playerOn = false;
        }
        // void Update()
        // {
        //     if (!box.activeSelf && !balise.activeSelf)
        //     {
        //         balise.SetActive(true);
        //     }
        // }
        
        public bool GetBaliseActive()
        {
            return balise.activeSelf;
        }
        
        public bool GetBoxActive()
        {
            return box.activeSelf;
        }
    }
}
