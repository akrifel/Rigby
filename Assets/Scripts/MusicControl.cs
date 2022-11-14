using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class MusicControl : MonoBehaviour
{
    public static MusicControl instance;
    private Scene currentScene;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);


        if (instance == null)
        {
            instance = this;
        }

        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "StartMenu")
        {
            Destroy(gameObject);
        }


    }



}
