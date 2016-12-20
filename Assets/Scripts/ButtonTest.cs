using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ButtonTest : MonoBehaviour {

    public void Awake()
    {
        AudioListener.pause = false;
        AudioListener.volume = 1;
    }

    public void ButtonClick()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("CarDemo");
    }
public void ButtonClick2()
    {
        SceneManager.LoadScene("MenuScreen");
    }
public void ButtonClick3()
    {
        AudioListener.pause = false;
        AudioListener.volume = 1;
        SceneManager.LoadScene("MenuScreen");
        Application.Quit();
    }

}
