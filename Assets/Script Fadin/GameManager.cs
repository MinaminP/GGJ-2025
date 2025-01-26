using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject PanelPause, PanelFinish, PanelGameOver;
    public static bool isPaused;
    public static bool isFinished;
    public static bool isFailed;
    public static int starCount;
    private const int MAX_STAR = 3;

    public GameObject[] starSprite;

    [SerializeField] Button[] backToMenuButton;
    [SerializeField] Button pauseButton;

    public int currentlevel;
    public int nextLevel;

    private void Start()
    {
        for (int i = 0; i < backToMenuButton.Length; i++)
        {
            backToMenuButton[i].onClick.AddListener(() => SceneMover.instance.PindahScene("Main Menu"));
        }
        isFinished = false;
        isFailed = false;
        isPaused = false;
        starCount = 0;
    }

    private void Update()
    {
        CheckLevelFailed();
        CheckLevelFinish();
    }

    public void CheckLevelFinish()
    {
        if (isFinished)
        {
            PanelFinish.SetActive(true);
            PlayerPrefs.SetInt("levelDone", nextLevel);

            int totalStar = PlayerPrefs.GetInt("level " + currentlevel + " star");

            if (starCount < totalStar)
            {
                PlayerPrefs.SetInt("level " + currentlevel + " star", starCount);
            }
        }
    }

    public void CheckLevelFailed()
    {
        if (isFailed)
        {
            PanelGameOver.SetActive(true);
        }
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Pause()
    {
        PanelPause.SetActive(true);
        isPaused = true;
    }

    public void Resume()
    {
        PanelPause.SetActive(false);
        isPaused = false;
    }

    public void NextLevel()
    {
        SceneManager.LoadScene("Level " + nextLevel);
    }
}