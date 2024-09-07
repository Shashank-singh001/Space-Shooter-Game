using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _ScoreText;
    [SerializeField]
    private Image lives_img;
    [SerializeField]
    private Text _Gameover;
    // Start is called before the first frame update
    [SerializeField]
    private Sprite[] _lives;
    [SerializeField]
    private Text _restart;
    private gameManager _gameManager;
    void Start()
    {
        _ScoreText.text = "Score: " + 0;
        _Gameover.gameObject.SetActive(false);
        _gameManager = GameObject.Find("Game_Manager").GetComponent<gameManager>();
        if(_gameManager==null)
        {
            Debug.Log("Gamemanager is Null");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Updatescore(int playerscore)
    {
        _ScoreText.text = "Score: " + playerscore.ToString();
        
    }
    public void Updatelives(int currentlives)
    {
        lives_img.sprite = _lives[currentlives];
        if (currentlives < 1)
        {
           
            gameOverSeq();
        }
    }
    void gameOverSeq()
    {
        _gameManager.gameOver();
        _Gameover.gameObject.SetActive(true);
        _restart.gameObject.SetActive(true);
        StartCoroutine(GameOverFlicker());
        

    }
    IEnumerator GameOverFlicker()
    {
        while (true)
        {
            _Gameover.text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);
            _Gameover.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }

}
