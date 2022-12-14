using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private float _rotateSpeed = 3.0f;
    [SerializeField] private GameObject _explosionPrefab;
    private SpawnManager _spawnManager;
    private AudioSource _audioSource;
    [SerializeField] private AudioClip _asteroidExplosion;
    
    void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
    }

    void Update()
    {
        transform.Rotate(new Vector3(0, 0, 10) * _rotateSpeed * Time.deltaTime);
    }

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            Instantiate (_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            _spawnManager.StartSpawning();
            AudioSource.PlayClipAtPoint(_asteroidExplosion, transform.position);
            Destroy(this.gameObject, 0.2f);
        }

    }

}
