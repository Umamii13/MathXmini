using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MinigameSelect : MonoBehaviour
{
    public TextMeshProUGUI minigamenameUI;
    public Button minigamesprite;
    public TextMeshProUGUI highscore;

    public int currentminigame;
    public MinigameChoice[] minigame;

    private void Awake()
    {
        UpdateMinigame();
        LoadHighScore();
    }
    public void Nextminigame()
    {
        currentminigame += 1;
        currentminigame = currentminigame %minigame.Length;
        UpdateMinigame();
        
    }
    public void beforeminigame()
    {
        currentminigame -= 1;
        currentminigame = currentminigame % minigame.Length;
        UpdateMinigame();

    }

    public void UpdateMinigame()
    {
        minigamenameUI.text = minigame[currentminigame].minigamename;
        minigamesprite.image.sprite = minigame[currentminigame].image;
        highscore.text = minigame[currentminigame].highscore.ToString("D5");
    }

    public void Loadscenegame()
    {
        SceneManager.LoadScene(minigame[currentminigame].minigamename + "Scene");
    }

    public void LoadHighScore()
    {
        for (int i = 0;i < minigame.Length;i++)
        {
            minigame[i].highscore = PlayerPrefs.GetInt(minigame[i].minigamename + "highscore");
        }
    }
}
[Serializable]public class MinigameChoice
{
    public string minigamename;
    public int highscore;
    public Sprite image;
}
