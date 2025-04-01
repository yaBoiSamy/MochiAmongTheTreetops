using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneLoading : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit();
    }

    public void loadLevel1()
    {
        SceneManager.LoadScene("Level1");
    }
}