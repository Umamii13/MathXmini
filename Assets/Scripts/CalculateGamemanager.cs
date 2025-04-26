using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CalculateGamemanager : MonoBehaviour
{
    public TextMeshProUGUI questionText;
    public TextMeshProUGUI scoreText;
    int TimeMin;
    int TimeSec;
    public TextMeshProUGUI Timetext;
    public choicebtn[] choice;
    
    [Header("System")]
    public string question;
    public int trueanswer; 
    public int score;

    public float gametime;
    public bool gameStart;
    void Start()
    {
        gameStart = true;
        RandomQuestion();
        choice[0].choiceButton.onClick.AddListener(() => { CheckAnswer(choice[0].answer); });
        choice[1].choiceButton.onClick.AddListener(() => { CheckAnswer(choice[3].answer); });
        choice[2].choiceButton.onClick.AddListener(() => { CheckAnswer(choice[3].answer); });
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

                if(score > PlayerPrefs.GetInt("CalculateXhighscore"))
                {
                    PlayerPrefs.SetInt("CalculateXhighscore", score);
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
                question = num1 + " / " + num2 + " =";
                trueanswer = num1 / num2;
                questionText.text = question;
            }
            RandomChoice();
        }
        while (trueanswer % 1 != 0);

    }

    public void RandomChoice()
    {
        int truechoicepos = UnityEngine.Random.Range(0, choice.Length);
        print(truechoicepos);
        choice[truechoicepos].answer = trueanswer;
        
        for (int i = 0; i < choice.Length; i++)
        {
            if (choice[i].answer == trueanswer)
            {
                choice[i].answer = trueanswer;
                choice[i].choicetext.text = choice[i].answer.ToString();
            }
            else
            {
                int ran;
                do
                {
                    print("new");
                     ran = UnityEngine.Random.Range(trueanswer - 100, trueanswer + 100);
                }
                while(ran == trueanswer);

                if (i != 0 && choice[i].answer == choice[(i-1)].answer)
                {
                    int neww;
                    do
                    {
                        print ("new2");
                        neww = UnityEngine.Random.Range(choice[(i - 1)].answer - 100, choice[(i - 1)].answer + 100);

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
            score += 1;
            RandomQuestion();
        }
        else
        {
            RandomQuestion();
        }
        scoreText.text = score.ToString("D5");
    }
}

[Serializable]public class choicebtn
{
    public TextMeshProUGUI choicetext;
    public Button choiceButton;
    public int answer;
}
