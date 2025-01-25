using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectLevelStar : MonoBehaviour
{
    [SerializeField] private GameObject[] stars;
    [SerializeField] private int levelNumber;
    // Start is called before the first frame update
    void Start()
    {
        int starGet = PlayerPrefs.GetInt("level "+levelNumber+" star");

        for (int i = 0; i < stars.Length; i++)
        {
            if (i + 1 > starGet)
            {
                stars[i].SetActive(false);
            }
        }
    }
}
