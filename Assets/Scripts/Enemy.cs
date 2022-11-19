using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 4f;
    private Player _player;
    private Animator _anim;
    [SerializeField] AudioClip _explosionSound;
    

    
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();

        if(_player == null)
        {
            Debug.LogError("The Player is NULL.");
        }
        
        
        _anim = GetComponent<Animator>();

        if (_anim == null)
        {
            Debug.LogError("Animtor is NULL.");
        }
    }

    
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
    
        if (transform.position.y <= -6.40f)
        {   
            float randomX = Random.Range(9.45f, -9.45f);
            transform.position = new Vector3(randomX, 6.50f, 0);
        }

    }

   private void OnTriggerEnter2D(Collider2D other)
   {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            
            if (player != null)
            {
                player.Damage();
            }

            _anim.SetTrigger("OnEnemyDeath");
            _speed = 3;
            _speed = 2;
            _speed = 1;
            AudioSource.PlayClipAtPoint(_explosionSound, transform.position);
            Destroy(this.gameObject, 2.8f);
        }

        if (other.tag == "Laser")
        {

            Destroy(other.gameObject);

            if (_player != null)
            {
                _player.AddScore(10);
            }

            _anim.SetTrigger("OnEnemyDeath");
            _speed = 3;
            _speed = 2;
            _speed = 1;
            AudioSource.PlayClipAtPoint(_explosionSound, transform.position);

            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject, 2.8f);
        }
   }

}
