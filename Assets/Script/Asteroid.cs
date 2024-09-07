using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _rotate=15f;
    [SerializeField]
    private GameObject _explosionPrefab;
    private spawnmanager _spawnManager;
    // Start is called before the first frame update
    private void Start()
    {
        _spawnManager = GameObject.Find("spawn_manager").GetComponent<spawnmanager>();
    }


    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * _rotate * Time.deltaTime);
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Lazer")
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            _spawnManager.startSpawning();
            Destroy(this.gameObject,0.25f);

        }
    }
}
