using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("Level1");

    }

    public void QuitToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
