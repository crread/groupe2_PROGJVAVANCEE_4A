using UnityEngine;

namespace Scrips
{
    public class PlayerControllerScript : MonoBehaviour
    {
        [SerializeField]
        private Transform playerTransform;
        [SerializeField]
        private Transform playerMesh;

        [SerializeField] private BombPoolManager bombPool;
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        private void Update()
        {
            if (Input.GetKey(KeyCode.Z))
            {
                playerTransform.position += Vector3.forward * (float) (Time.deltaTime * 1.8);
                playerMesh.rotation = Quaternion.AngleAxis(0, Vector3.up);
            }
            else if (Input.GetKey(KeyCode.Q))
            {
                playerTransform.position -= Vector3.right * (float) (Time.deltaTime * 1.8);
                playerMesh.rotation = Quaternion.AngleAxis(-90, Vector3.up);
                
            }
            else if (Input.GetKey(KeyCode.D))
            {
                playerTransform.position += Vector3.right * (float) (Time.deltaTime * 1.8);
                playerMesh.rotation = Quaternion.AngleAxis(90, Vector3.up);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                playerTransform.position -= Vector3.forward * (float) (Time.deltaTime * 1.8);
                playerMesh.rotation = Quaternion.AngleAxis(180, Vector3.up);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("bomb placed");
                var bomb = bombPool.Pooling();
                var position = transform.position;
                bomb.transform.position = new Vector3(Mathf.Round(position.x), 0, Mathf.Round(position.z));
                bomb.SetActive(true);
            }
        }
    }
}
