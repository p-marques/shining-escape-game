using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMainMenu : MonoBehaviour
{
    private const string PLAY_SCENE_NAME = "Floor_1";

    public void OnPressPlay()
    {
        SceneManager.LoadScene(PLAY_SCENE_NAME);
    }

    public void OnPressExit()
    {
        Application.Quit();
    }
}
