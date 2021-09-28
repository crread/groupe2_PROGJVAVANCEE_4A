using UnityEngine;

namespace Scrips
{
    public class CharacterControllerScript : MonoBehaviour
    {
        [SerializeField]
        private Transform playerTransform;
        [SerializeField]
        private Transform playerMesh;

        [SerializeField] private float playerSpeed;

        [SerializeField] private BombPoolManager bombPool;
        
        public void MoveForward()
        {
            playerTransform.position += Vector3.forward * (float) (Time.deltaTime * playerSpeed);
            playerMesh.rotation = Quaternion.AngleAxis(0, Vector3.up);
        }

        public void MoveBackward()
        {
            playerTransform.position -= Vector3.forward * (float) (Time.deltaTime * playerSpeed);
            playerMesh.rotation = Quaternion.AngleAxis(180, Vector3.up);
        }

        public void MoveLeft()
        {
            playerTransform.position -= Vector3.right * (float) (Time.deltaTime * playerSpeed);
            playerMesh.rotation = Quaternion.AngleAxis(-90, Vector3.up);
        }

        public void MoveRight()
        {
            playerTransform.position += Vector3.right * (float) (Time.deltaTime * playerSpeed);
            playerMesh.rotation = Quaternion.AngleAxis(90, Vector3.up);
        }

        public void PlaceBomb()
        {
            var bomb = bombPool.DePooling();
            var position = transform.position;
            bomb.transform.position = new Vector3(Mathf.Round(position.x), 0, Mathf.Round(position.z));
            bomb.SetActive(true);
        }

        public void Death()
        {
            Debug.Log("Player died");
        }
    }
}
