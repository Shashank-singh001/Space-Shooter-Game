using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
   // [SerializeField]
    private bool _gameover;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R) && _gameover==true)
        {
           SceneManager.LoadScene(1);//Current Game Scene
        }
        
    }
    public void gameOver()
    {
       // Debug.Log("GameManager::GameOver is called");
        _gameover = true;
        
    }
}
