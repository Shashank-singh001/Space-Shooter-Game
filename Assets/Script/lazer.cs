using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lazer : MonoBehaviour
{
    [SerializeField]
    private float _speed = 8f;
    void Update()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);
        if(transform.position.y>7.8)
        {

            if(transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
            //Destroy(this.gameObject, 5);
            //destroys object after 5 s.
        } 
    }
}
