using UnityEngine;
using TMPro;
using System;
using System.Collections;

public class GameManager : MonoBehaviour
{

    public TextMeshProUGUI timerText;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI scoreText;
    public Camera cam;

    private int coinCount = 0;
    private int scoreCount = 0;

    // Update is called once per frame
    void Update()
    {
        int timeLeft = 300 - (int)(Time.time);
        timerText.text = $"Time: {timeLeft}";

        if(Input.GetMouseButtonDown(0)){

            Vector3 screenPos = Input.mousePosition;
            Ray cursorRay = cam.ScreenPointToRay(screenPos);
            bool rayHitSomething = Physics.Raycast(cursorRay, out RaycastHit screenHitInfo);


            if(rayHitSomething){

                if(screenHitInfo.transform.gameObject.CompareTag("Brick")){
                    Destroy(screenHitInfo.transform.gameObject);
                }
                else if(screenHitInfo.transform.gameObject.CompareTag("QuestionBlock")){
                    coinCount++;
                    scoreCount += 100;
                    if(coinText != null){
                        coinText.text = $"x{coinCount}";
                        scoreText.text = $"Mario: {scoreCount}";
                    }
                    Destroy(screenHitInfo.transform.gameObject);
                }
            }
        }
    }
}
