using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIFunctons : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Restart(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Unpause()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
    }
}
