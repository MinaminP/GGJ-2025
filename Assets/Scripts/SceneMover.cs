using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMover : MonoBehaviour
{
    public static SceneMover instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
            instance = this;
            DontDestroyOnLoad(gameObject);
    }

    public void PindahScene(string NamaScene)
    {
        SceneManager.LoadScene(NamaScene);
    }
    public void Keluar()
    {
        Application.Quit();
    }
}
