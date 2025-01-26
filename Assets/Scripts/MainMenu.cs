using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Button startButton;
    [SerializeField] string playSceneName;
    // Start is called before the first frame update
    void Start()
    {
        startButton.onClick.AddListener(() => SceneMover.instance.PindahScene(playSceneName));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
