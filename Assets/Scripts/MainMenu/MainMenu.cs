using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void LoadScene(string str) {
        SceneManager.LoadScene(str);
    }

    public void Quit() {
        Application.Quit();
    }
}
