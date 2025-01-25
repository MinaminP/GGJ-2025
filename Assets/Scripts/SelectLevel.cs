using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectLevel : MonoBehaviour
{
    public Button[] levelButton;
    void Start()
    {
        int unlockedLevel = PlayerPrefs.GetInt("levelDone", 1);

        for (int i = 0; i < levelButton.Length; i++)
        {
            if (i + 1 > unlockedLevel)
            {
                levelButton[i].interactable = false;
            }
        }
    }

    public void SelectLevelScene(int level)
    {
        SceneManager.LoadScene("Level " + level);
    }
}