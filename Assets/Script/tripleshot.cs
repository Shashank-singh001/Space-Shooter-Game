using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tripleshot : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3;
    [SerializeField]
    private int powerupID;
    [SerializeField]
    private AudioClip _clip;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if(transform.position.y<-1.8f)
        {
            Destroy(this.gameObject);
        }
        //leave screen then derstroy
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="Player")
        {
            AudioSource.PlayClipAtPoint(_clip, transform.position);
            player player = other.transform.GetComponent<player>();
            if (player != null)
            {
                switch(powerupID)
                {
                    case 0:
                        player.TripleShotActive();
                        break;
                    case 1:
                       // Debug.Log("Collected Speed Powerup");
                        player.SpeedpowerActive();
                        break;
                    case 2:
                        player.ShieldPowerup();
                        Debug.Log("Collected Shield powerup");
                        break;
                    default:
                        Debug.Log("Error Powerup");
                        break;
                }
            }
            Destroy(this.gameObject);
        }
    }

}