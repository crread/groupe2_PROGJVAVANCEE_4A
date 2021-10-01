using UnityEngine;

namespace Scrips
{
    public class PlayerControllerScript : MonoBehaviour
    {
        [SerializeField]
        private Transform playerTransform;
        [SerializeField]
        private Transform playerMesh;

        void Update()
        {
            if (Input.GetKey(KeyCode.Z))
            {
                playerTransform.position += Vector3.forward * (float) (Time.deltaTime * 1.5);
                playerMesh.rotation = Quaternion.AngleAxis(0, Vector3.up);
            }
            else if (Input.GetKey(KeyCode.Q))
            {
                playerTransform.position -= Vector3.right * (float) (Time.deltaTime * 1.5);
                playerMesh.rotation = Quaternion.AngleAxis(-90, Vector3.up);
                
            }
            else if (Input.GetKey(KeyCode.D))
            {
                playerTransform.position += Vector3.right * (float) (Time.deltaTime * 1.5);
                playerMesh.rotation = Quaternion.AngleAxis(90, Vector3.up);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                playerTransform.position -= Vector3.forward * (float) (Time.deltaTime * 1.5);
                playerMesh.rotation = Quaternion.AngleAxis(180, Vector3.up);
            }
        }
    }
}
