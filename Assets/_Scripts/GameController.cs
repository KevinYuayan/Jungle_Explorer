using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// File Name: GameController.cs
/// Author: Kevin Yuayan
/// Last Modified by: Kevin Yuayan
/// Date Last Modified: Nov. 3, 2019
/// Description: Controller for the GameController Object
/// Revision History:
/// </summary>
public class GameController : MonoBehaviour
{
    [Header("UI")]
    public Text livesLabel;
    public Text scoreLabel;
    public Text highScoreLabel;
    public Text gameOverScoreLabel;
    public Text gameOverLabel;
    public GameObject restartButton;
    //public Text highScoreLabel;
    //public Text endScoreLabel;

    //public GameObject scoreBoard;
    [Header("Game Objects")]
    public GameObject player;
    public Transform activeCheckpoint;
    public Transform firstCheckPoint;
    public GameObject scoreBoard;

    [Header("Sounds")]
    public AudioSource hitSound;
    public AudioSource MainBG;

    [Header("Scoreboard")]
    [SerializeField]
    private int _lives;

    [SerializeField]
    private int _score;

    public int Lives
    {
        get
        {
            return _lives;
        }

        set
        {
            if(value < _lives)
            {
                player.transform.position = activeCheckpoint.position;
                hitSound.Play();
            }
            _lives = value;
            if (_lives < 1)
            {
                DontDestroyOnLoad(scoreBoard);
                SceneManager.LoadScene("GameOver");
            }
            else
            {
                livesLabel.text = "Lives: " + _lives.ToString();
            }
        }
    }

    public int Score
    {
        get
        {
            return _score;
        }

        set
        {
            _score = value;
            if (scoreBoard.GetComponent<Score>().highScore < _score)
            {
                scoreBoard.GetComponent<Score>().highScore = _score;
            }
            scoreBoard.GetComponent<Score>().score = _score;
            scoreLabel.text = "Score: " + _score.ToString();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        scoreBoard = GameObject.Find("ScoreBoard");
        SceneConfiguration();
    }

    private void SceneConfiguration()
    {

        switch (SceneManager.GetActiveScene().name)
        {
            case "Start":
                scoreLabel.enabled = false;
                livesLabel.enabled = false;
                highScoreLabel.enabled = false;
                //endScoreLabel.enabled = false;
                //endLabel.SetActive(false);
                restartButton.SetActive(false);
                //activeSoundClip = SoundClip.BG_MUSIC;
                break;
            case "Main":
                highScoreLabel.enabled = false;
                gameOverScoreLabel.enabled = false;
                gameOverLabel.enabled = false;
                //startLabel.SetActive(false);
                //startButton.SetActive(false);
                //endLabel.SetActive(false);
                restartButton.SetActive(false);
                MainBG.Play();
                MainBG.loop = true;
                break;
            case "GameOver":
                scoreLabel.enabled = false;
                livesLabel.enabled = false;
                //startLabel.SetActive(false);
                //startButton.SetActive(false);
                //activeSoundClip = SoundClip.GAME_OVER_MUSIC;
                //highScoreLabel.text = "High Score: " + highScore.GetComponent<Score>().highScore;
                //gameOverScoreLabel.text = "Score: " + highScore.GetComponent<Score>().score;
                break;
        }

        Lives = 5;
        Score = 0;

        //if ((activeSoundClip != SoundClip.NONE) && (activeSoundClip != SoundClip.NUM_OF_CLIPS))
        //{
        //    AudioSource activeSoundSource = audioSources[(int)activeSoundClip];
        //    activeSoundSource.playOnAwake = true;
        //    activeSoundSource.loop = true;
        //    activeSoundSource.volume = 0.5f;
        //    activeSoundSource.Play();
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnRestartButtonClick()
    {
        SceneManager.LoadScene("Main");
    }
}
