using UnityEngine;
using TMPro;
using System;
using System.Collections;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public TextMeshProUGUI timerText;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI scoreText;
    public GameObject loseText;
    public GameObject loseText2;
    public GameObject winText;
    public Camera cam;
    
    private int timeLeft;
    private int coinCount = 0;
    private int scoreCount = 0;

    void Start()
    {
        loseText.SetActive(false);
        loseText2.SetActive(false);
        winText.SetActive(false);
    }

    void Awake(){
        Instance = this;
    }   


    // Update is called once per frame
    void Update()
    {
        timeLeft = 100 - (int)(Time.time);
        if(timeLeft <= 0){
            timerText.text = $"Time: 0";
            loseText.SetActive(true);
        }
        else{
            timerText.text = $"Time: {timeLeft}";
        }
    }

    public void GameOver(){
        loseText2.SetActive(true);
    }

    public void GameWon(){
        scoreCount += 1000;
        winText.SetActive(true);
    }

    public void AddScore(int points){
        scoreCount += points;
        scoreText.text = $"Mario: {scoreCount}";
    }

    public void AddCoin()
    {
        coinCount++;
        coinText.text = $"x{coinCount}";
        scoreText.text = $"Mario: {scoreCount}";
    }
}
