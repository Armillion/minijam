using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIFunctons : MonoBehaviour
{
    [SerializeField] private List<string> sceneNames;

    public void MainMenu()
    {
        SceneManager.LoadScene(sceneNames[0]);
    }

    public void Restart()
    {
        SceneManager.LoadScene(sceneNames[1]);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
