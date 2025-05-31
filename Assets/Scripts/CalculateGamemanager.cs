using System;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class CalculateGamemanager : MonoBehaviour
{
    public TextMeshProUGUI questionText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI Timetext;
    public choicebtn[] choice;

    public Animator gameendpanel;
    public Animator clock;
    public TextMeshProUGUI scoretext;
    
    [Header("System")]
    public string question;
    public float maxanswer;
    public float minanswer;
    public float trueanswer; 
    public int score;

    public float gametime;
    public bool gameStart;
    void Start()
    {
        gameStart = true;
        string mode = PlayerPrefs.GetString("Mode");
        if(mode == "Easy")
        {
            maxanswer = 50;
            minanswer = 0;
        }
        else if (mode == "Normal")
        {
            maxanswer = 100;
            minanswer = -100;
        }
        else if (mode == "Hard")
        {
            maxanswer = 150;
            minanswer = -150;
        }
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
            int TimeMin = (int)TimetextMin;
            int TimeSec = (int)TimetextSec;
            Timetext.text = TimeMin.ToString("D2") + " : " + TimeSec.ToString("D2");

            if (gametime <= 0)
            {

                if(score > PlayerPrefs.GetInt("CalculateXhighscore"))
                {
                    PlayerPrefs.SetInt("CalculateXhighscore", score);
                }
                //game over panel
                gameendpanel.gameObject.SetActive(true);
                scoretext.text = score.ToString("D6");
                gameendpanel.SetTrigger("In");
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
                question = num1 + " + " + num2 + " =";
                trueanswer = num1 + num2;
                questionText.text = question;
            }
            else if (symbo == 1)
            {
                question = num1 + " - " + num2 + " =";
                trueanswer = num1 - num2;
                questionText.text = question;
            }
            else if (symbo == 2)
            {
                question = num1 + " x " + num2 + " =";
                trueanswer = num1 * num2;
                questionText.text = question;
            }
            else
            {
                if(num2 == 0)
                {
                    RandomQuestion();
                    print("0");
                    return;
                }
                question = num1 + " / " + num2 + " =";
                trueanswer = (float)num1 / num2;
                questionText.text = question;
            }
            RandomChoice();

        }

        while (trueanswer % 1 != 0 || trueanswer > maxanswer || trueanswer < minanswer );
        
    }

    public void RandomChoice()
    {
        int truechoicepos = UnityEngine.Random.Range(0, choice.Length);
        print(truechoicepos);
        choice[truechoicepos].answer = Convert.ToInt32(trueanswer);
        
        for (int i = 0; i < choice.Length; i++)
        {
            if (choice[i].answer == trueanswer)
            {
                choice[i].answer = Convert.ToInt32(trueanswer);
                choice[i].choicetext.text = choice[i].answer.ToString();
            }
            else
            {
                int ran;
                do
                {
                    print("new");
                     ran = UnityEngine.Random.Range(Convert.ToInt32(trueanswer) - (int)maxanswer, Convert.ToInt32(trueanswer) + (int)maxanswer);
                }
                while(ran == trueanswer);

                if (i != 0 && choice[i].answer == choice[(i-1)].answer)
                {
                    int neww;
                    do
                    {
                        print ("new2");
                        neww = UnityEngine.Random.Range(choice[(i - 1)].answer - (int)maxanswer, choice[(i - 1)].answer + (int)maxanswer);

                    }
                    while (neww == trueanswer);
                    choice[i].answer = neww;
                }
                choice[i].answer = ran;
                choice[i].choicetext.text = choice[i].answer.ToString();
            }
            
        }



    }
    public void CheckAnswer(int answer)
    {
        Debug.Log("1");
        if(answer == trueanswer)
        {
            AudiosourceManager.instance.PlayCorrectSE();
            score += 1;
            RandomQuestion();
        }
        else
        {
            AudiosourceManager.instance.PlayFailSE();
            if(score > 0)
            {
                score -= 1;
            }
            else
            {
                gametime -= 5;
            }
            RandomQuestion();
        }
        scoreText.text = score.ToString("D6");
    }
}

[Serializable]public class choicebtn
{
    public TextMeshProUGUI choicetext;
    public Button choiceButton;
    public int answer;
}
