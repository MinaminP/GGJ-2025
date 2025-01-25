using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    private GameObject PanelPause;
    [SerializeField] Button backToMenuButton;

    private void Start()
    {
        //PanelPause = GameObject.FindWithTag("Pause");
        backToMenuButton.onClick.AddListener(() => SceneMover.instance.PindahScene("Main Menu"));
    }
    
    public void Pause()
    {
        PanelPause.SetActive(true);
    }
    public void Resume()
    {
        PanelPause.SetActive(false);
    }
}