using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private GameObject PanelPause;

    private void Start()
    {
        PanelPause = GameObject.FindWithTag("Pause");
    }
    public void PindahScene(string NamaScene)
    {
        SceneManager.LoadScene(NamaScene);
        Time.timeScale = 1f;
    }
    public void Keluar()
    {
        Application.Quit();
    }
    public void Pause()
    {
        PanelPause.SetActive(true);
        Time.timeScale = 0f;
    }
    public void Resume()
    {
        PanelPause.SetActive(false);
        Time.timeScale = 1f;
    }
}
