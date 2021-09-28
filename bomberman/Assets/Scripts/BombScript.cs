using UnityEngine;

public class BombScript : MonoBehaviour
{

    [SerializeField] private GameObject bomb;

    [SerializeField] private GameObject explosion1;

    [SerializeField] private GameObject explosion2;

    [SerializeField] private float explosionCooldown;

    [SerializeField] private float explosionDuration;

    public bool Pooled { get; set; }

    private float _currentBombCooldown;
    private float _currentExplosionCooldown;

    // Start is called before the first frame update
    void OnEnable()
    {
        bomb.SetActive(true);
        _currentBombCooldown = explosionCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentBombCooldown <= 0 && bomb.activeSelf)
        {
            bomb.SetActive(false);
            explosion1.SetActive(true);
            explosion2.SetActive(true);
            _currentExplosionCooldown = explosionDuration;
        }
        else if (_currentExplosionCooldown <= 0 && (explosion1.activeSelf || explosion2.activeSelf))
        {
            explosion1.SetActive(false);
            explosion2.SetActive(false);
            gameObject.SetActive(false);
            Pooled = false;
        }
        else
        {
            _currentExplosionCooldown -= Time.deltaTime;
            _currentBombCooldown -= Time.deltaTime;
        }
    }
}