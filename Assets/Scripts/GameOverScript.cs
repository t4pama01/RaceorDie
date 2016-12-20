using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour {

    public float score = 0.0f;
    public Text resultText;
    public Text descriptionText;

    // Use this for initialization
    void Start () {

        Cursor.visible = true;
        resultText.text = "You Lost!";
        descriptionText.text = "All you had to do was to follow the road!";

	}
	
	// Update is called once per frame
	void Update () {
	
       
	}

    public void ButtonClick()
    {
        SceneManager.LoadScene("CarDemo");
    }
    public void ButtonClick2()
    {
        SceneManager.LoadScene("MenuScreen");
    }
}
