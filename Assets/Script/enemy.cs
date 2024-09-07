using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5f;
    private player _player;
    private Animator _anm;
    private AudioSource _audiosource;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("player").GetComponent<player>();
        if(_player==null)
        {
            Debug.LogError("player is null");
        }

        _anm = GetComponent<Animator>();
        if(_anm == null)
        {
            Debug.LogError("Animator is null");
        }
        _audiosource = GetComponent<AudioSource>();
        if(_audiosource==null)
        {
            Debug.Log("Audio is Null.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        // if enemy is below 1.8 respawn it
        if(transform.position.y < -1.8f)
        {
            float randomX = Random.Range(-6.35f, 6.35f);
            transform.position = new Vector3(randomX, 7.8f, 0);
        }
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
       // Debug.Log("Hit : " + other.transform.name);
       // It gives an output in console.

        if(other.tag == "Player")
        {
            // in unity we cant access player directly so we use getcomponent with transform
            // as transform is the only thing we can access 
            player player = other.transform.GetComponent<player>();
            //we check if player component exists as it provides us if there can be any error
            if(player != null)
            {
                player.damage();
            }
            _anm.SetTrigger("OnEnemyDeath");
            _speed = 0f;
            _audiosource.Play();
            Destroy(this.gameObject,2.8f);
            
        }
        if(other.tag == "Lazer")
        {
            Destroy(other.gameObject);
            if(_player != null)
            {
                _player.AddScore(10); 
            }
            _anm.SetTrigger("OnEnemyDeath");
            _speed = 0f;
            _audiosource.Play();
            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject,2.8f);
        }
    }
}
