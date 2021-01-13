using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Level : MonoBehaviour
{
    public void LoadStartMenu()
    {
        /*
         The load scene method is found in the scene manager class which is found in the scene management library.
        To use the scene management library we need to import it
         */
        SceneManager.LoadScene(0);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("LaserDefender");
    }

    public void LoadGameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void QuitGame()
    {
        print("game quitting");
        Application.Quit();
    }
}
