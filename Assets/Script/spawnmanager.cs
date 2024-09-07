using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnmanager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    private bool _isalive = true;
    //create container objct to store enemies to make it tiedier.
    [SerializeField]
    private GameObject _EnemyContainer;
    // Start is called before the first frame update
    [SerializeField]
    private GameObject[] _powerups;

    public void startSpawning()
    {
        StartCoroutine(spawnroutineEnemy());
        StartCoroutine(spawnroutineTripleshot());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //IEnumerator provides us with yield keyword that gives us the authority
    //to delay function by any t seconds.
    IEnumerator spawnroutineEnemy()
    {
        yield return new WaitForSeconds(3.0f);
        //while loop to provide infinte spawn of enemy.
        while(_isalive)
        {
            //spawning position insatiate and wait for 5 seconds.
            Vector3 posToSpawn = new Vector3(Random.Range(-5.6f, 5.5f), 7.7f, 0);
            GameObject EnemyCont = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
            //Changing .parent to another object which is also of .transform type
            EnemyCont.transform.parent = _EnemyContainer.transform;
            yield return new WaitForSeconds(5.0f);
        }
    }

    IEnumerator spawnroutineTripleshot()
    {
        yield return new WaitForSeconds(3.0f);
        while (_isalive)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-5.6f, 5.5f), 7.7f, 0);
            int randompowerup = Random.Range(0, 3);
            Instantiate(_powerups[randompowerup], posToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3, 8));
        }
    }
    public void onPlayerDeath()
    {
        _isalive = false;
    }
}
