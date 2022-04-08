using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System.IO;
using System;

public class UIControl : MonoBehaviour {

    public static UIControl instance;

    public GameObject mainUI;
    public GameObject loseUI;
    public GameObject pauseUI;

    //public GameObject clickButton;

    //public GameObject ballStartButton;
    //public GameObject ballSprite;

    //public TextMeshProUGUI levelText;
    //public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bestScoreText;
    public TextMeshProUGUI currentScoreText;
    //public TextMeshProUGUI timeText;

    //public Sprite[] defaultImage;

    //private int level;
    private int score = 0;
    private int loseSoundCounter = 0;
    private string savePath;
    private int time = 0;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    void Start() {
        mainUI.SetActive(true);
        loseUI.SetActive(false);
        pauseUI.SetActive(false);
        InitiatValue();
        ShowLevel();
        ShowScore(0);
        //InvokeRepeating("AddTime", 0, 1);
    }

    private void InitiatValue() {
        GlobalValue.isGameOver = false;
        GlobalValue.isGamePause = false;
        GlobalValue.playerMoveSpeed = 2;
        GlobalValue.wheelMoveSpeed = 3;
        GlobalValue.wheelRotadSpeed = 8;
    }

    public void GameOver() {
        if (SoundControl.Instance && loseSoundCounter==0) {
            loseSoundCounter = 1;
            SoundControl.Instance.Vibration();
            SoundControl.Instance.PlayLose();
        }
        GlobalValue.isGameOver = true;
        GlobalValue.playerMoveSpeed = 0;
        GlobalValue.wheelMoveSpeed = 0;
        GlobalValue.wheelRotadSpeed = 0;
        ShowBestScore();
        loseUI.SetActive(true);
    }

    public void OnRestart() {
        if (SoundControl.Instance) {
            SoundControl.Instance.PlayClick();
        }
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnMenu() {
        if (SoundControl.Instance) {
            SoundControl.Instance.PlayClick();
        }
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    private string SecondIntToString(int totalSecond) {
        string secondInString;
        int minutes = totalSecond / 60;
        int second = totalSecond % 60;
        if (second <= 9) {
            secondInString = minutes + ":0" + second;
        } else {
            secondInString = minutes + ":" + second;
        }
        return secondInString;
    }

    /*private void AddTime() {
        if (!GlobalValue.isGameOver) {
            time = time + 1;
            timeText.text = SecondIntToString(time);
        }
    }*/

    public void ShowLevel() {
        //levelText.text = level.ToString();
    }

    /*public void ShowScore(int status) {
        if (status == 0) {
            scoreText.text = score.ToString();
        } else {
            score = score - 1;
            scoreText.text = this.score.ToString();
            if (score == 0) {
                //Player win;
                //stop player movement;
                //PlayerControl.instance.StatusChange(false);
                //Ball.instance.GoalHit();
                //clickButton.SetActive(false);
            }
        }
    }*/

    /*public void ShowScore(int score) {
        this.score = score;
        scoreText.text = score.ToString();
        
        if(score%10==0 && score>=10 && score <= 100) {
            int result = score / 10;
            float incrementValue = (float)result / 10;
            GlobalValue.playerMoveSpeed = 2 + incrementValue;
        }
    }*/

    public void ShowScore(int value) {
        if (!GlobalValue.isGameOver) {
            //score += value;
            score = value;
            //scoreText.text = "Score: " + score.ToString();
        }
    }

    /*public void ShowScore(int value) {
        if (SoundControl.Instance && value>0) {
            //SoundControl.Instance.PlayBonus();
        }
        score += value;
        scoreText.text =score.ToString();
    }*/

    public void OnPause() {
        if (SoundControl.Instance) {
            SoundControl.Instance.PlayClick();
        }
        Time.timeScale = 0;
        GlobalValue.isGamePause = true;
        pauseUI.SetActive(true);
    }

    //onPausestop
    public void OnReturn() {
        if (SoundControl.Instance) {
            SoundControl.Instance.PlayClick();
        }
        Time.timeScale = 1;
        GlobalValue.isGamePause = false;
        pauseUI.SetActive(false);
    }

    private void ShowBestScore() {
        int bestScore = PlayerPrefs.GetInt(ConstantValue.bestScore, 0);
        if (bestScore < score) {
            bestScore = score;
            PlayerPrefs.SetInt(ConstantValue.bestScore, bestScore);
        }
        bestScoreText.text = "BEST SCORE: " + bestScore.ToString();
        currentScoreText.text = "SCORE: " + score.ToString();

    }


   


    public int GetScore() {
        return score;
    }


    public void ClickButton() {
        //BallControl.instance.ClickButton();
    }


}
