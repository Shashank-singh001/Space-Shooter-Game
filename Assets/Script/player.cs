using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5f;
    private float _speedMultiplier = 2f;
    [SerializeField]
    private GameObject _lazerprefab;
    [SerializeField]
    private GameObject _tripleshotprefab;
    [SerializeField]
    private float _firerate = 0.5f;
    private float _canfire = -1f;
    [SerializeField]
    private int _lives = 3;
    private spawnmanager _spawnman;
    private bool _istripleshotactive = false;
    private bool _isshieldActive = false;
    // private bool _isspeedpowerup = false;
    [SerializeField]
    private GameObject _shieldVisualizer;
    [SerializeField]
    private int _Score;
    private UIManager _uiHandle;
    [SerializeField]
    private GameObject rightengine;
    [SerializeField]
    private GameObject leftengine;
    [SerializeField]
    private AudioClip audio;
    private AudioSource _audioSource;
    

    void Start()
    {
        //transform position of player
        transform.position = new Vector3(0, 0, 0);
        _spawnman = GameObject.Find("spawn_manager").GetComponent<spawnmanager>();
        
        if(_spawnman == null)
        {
            Debug.LogError("The Spawn Manager is ERROR");
        }
        _uiHandle = GameObject.Find("Canvas").GetComponent<UIManager>();
        if(_uiHandle==null)
        {
            Debug.Log("UI Manager is Null.");
        }
        _audioSource = GetComponent<AudioSource>();
        if(_audioSource==null)
        {
            Debug.Log("Audio Source is Null");
        }
        else
        {
            _audioSource.clip = audio;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        movement();
        //Lazer Shooting Function
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canfire)
            shootlazer();
    }

    void movement()
    {
        float HorizontalInput = Input.GetAxis("Horizontal");
        float VerticalInput = Input.GetAxis("Vertical");
        //transform.Translate(Vector3.right * HorizontalInput * _speed * Time.deltaTime);
        //transform.Translate(Vector3.up * VerticalInput * _speed * Time.deltaTime);
        //Another optimiza way to do movements
        Vector3 direction = new Vector3(HorizontalInput, VerticalInput, 0);
        transform.Translate(direction * _speed * Time.deltaTime);
        float a = transform.position.x;
        float b = transform.position.y;

        if (b >= 1.52)
        {
            transform.position = new Vector3(a, 1.52f, 0);
        }
        else if (b <= -0.52)
        {
            transform.position = new Vector3(a, -0.52f, 0);
        }
        //Easier approach
        // transform.position = new Vector3(a, Mathf.Clamp(b, 0, 3), 0);
        if (a >= 8)
        {
            transform.position = new Vector3(-8f, b, 0);
        }
        else if (a <= -8)
        {
            transform.position = new Vector3(8f, b, 0);
        }
    }

    void shootlazer()
    {
        _canfire = Time.time + _firerate;
        if (_istripleshotactive)
        {
            Instantiate(_tripleshotprefab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_lazerprefab, transform.position + new Vector3(0, 0.85f, 0), Quaternion.identity);
        }
        _audioSource.Play();
    }

    public void damage()
    {
        if(_isshieldActive == false)
        {
            _lives--;
            _uiHandle.Updatelives(_lives);
        }
        else
        {
            _isshieldActive = false;
            _shieldVisualizer.SetActive(false);
        }
        if(_lives==2)
        {
            rightengine.SetActive(true);
        }
        else if(_lives==1)
        {
            leftengine.SetActive(true);
        }
        
        if (_lives < 1)
        {
            _spawnman.onPlayerDeath();
            Destroy(this.gameObject);
           
        }
    }

    public void TripleShotActive()
    {
        _istripleshotactive = true;
        StartCoroutine(tripleshotpowerdown());
        //start power down coroutine
    }

    IEnumerator tripleshotpowerdown()
    {
        yield return new WaitForSeconds(5.0f);
        _istripleshotactive = false;
    }
    public void SpeedpowerActive()
    {
        _speed *= _speedMultiplier;
        StartCoroutine(speedpowerdown());
    }
    IEnumerator speedpowerdown()
    {
        yield return new WaitForSeconds(5.0f);
        _speed /= _speedMultiplier;
    }
    public void ShieldPowerup()
    {
        _isshieldActive = true;
        _shieldVisualizer.SetActive(true);
    }
    public void AddScore(int points)
    {
        _Score += points;
        _uiHandle.Updatescore(_Score);
        

    }

}
