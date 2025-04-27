using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class SymboGamemanager : MonoBehaviour
{
    public TextMeshProUGUI questionText;
    public TextMeshProUGUI scoreText;
    int TimeMin;
    int TimeSec;
    public TextMeshProUGUI Timetext;
    public choicebtn[] choice;

    public float gametime;
    public bool gameStart;

    public int score;

    [Header("System")]
    public string question;
    public float resualt;
    public int trueanswer;
    void Start()
    {
        gameStart = true;
        RandomQuestion();
        choice[0].choiceButton.onClick.AddListener(() => { CheckAnswer(choice[0].answer); });
        choice[1].choiceButton.onClick.AddListener(() => { CheckAnswer(choice[1].answer); });
        choice[2].choiceButton.onClick.AddListener(() => { CheckAnswer(choice[2].answer); });
        choice[3].choiceButton.onClick.AddListener(() => { CheckAnswer(choice[3].answer); });
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStart)
        {
            gametime -= 1 * Time.deltaTime;
            float TimetextMin = (gametime / 60);
            float TimetextSec = (gametime % 60);
            TimeMin = Convert.ToInt32(TimetextMin);
            TimeSec = Convert.ToInt32(TimetextSec);
            Timetext.text = TimeMin.ToString("D2") + " : " + TimeSec.ToString("D2");
            if (gametime <= 0)
            {

                if (score > PlayerPrefs.GetInt("SymboXhighscore"))
                {
                    PlayerPrefs.SetInt("SymboXhighscore", score);
                }
                //game over panel
                gameStart = false;
            }
        }
    }

    public void RandomQuestion()
    {
        do
        {
            print("Loop");
            int num1 = UnityEngine.Random.Range(0, 100);
            int num2 = UnityEngine.Random.Range(0, 100);
            int symbo = UnityEngine.Random.Range(0, 4);

            if (symbo == 0)
            {
                question = num1 + " _ " + num2 + " =" + Convert.ToInt32(num1+num2);
                resualt = num1 + num2;
                questionText.text = question;
            }
            else if (symbo == 1)
            {
                question = num1 + " _ " + num2 + " =" + Convert.ToInt32(num1 - num2);
                resualt = num1 - num2;
                questionText.text = question;
            }
            else if (symbo == 2)
            {
                question = num1 + " _ " + num2 + " =" + Convert.ToInt32(num1 * num2);
                resualt = num1 * num2;
                questionText.text = question;
            }
            else
            {
                question = num1 + " _ " + num2 + " =" + Convert.ToInt32(num1 / num2);
                resualt = (float)num1 / num2;
                questionText.text = question;
            }
            trueanswer = symbo;
        }

        while (resualt % 1 != 0 || resualt > 500);
    }

    public void CheckAnswer(int selectchoice)
    {
        if(trueanswer == selectchoice)
        {
            AudiosourceManager.instance.PlayCorrectSE();
            score += 1;
            RandomQuestion();
        }
        else
        {
            AudiosourceManager.instance.PlayCorrectSE();
            RandomQuestion();
        }
        scoreText.text = score.ToString("D5");
    }
}
