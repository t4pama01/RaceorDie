using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public float maxlifetime = 15.0f;
    //public float score = 0.0f;
    //private float maxlifetime = 15.0f;
    public float checkpointtime = 15.0f;
    public float bestTime = 0.0f;
    //private float bestTime = 0.0f;
    public int counter = 0;
//    public Text DataText;
//    public Text BestTimeText;
    //public Text txt; // UI text
    //public Text besttxt;



    // Use this for initialization
    void Start () {

        //score = PlayerPrefs.GetFloat("score");

    }
	
	// Update is called once per frame
	void Update () {

//        DataText.text = "Counter: " + counter.ToString() + "\n" + "Time: " + maxlifetime.ToString();
//        BestTimeText.text = "Best Time: " + score.ToString();
/*        if (maxlifetime > 0)
        {
            maxlifetime -= Time.deltaTime;
        }
        if (maxlifetime <= 0)
        {
            SceneManager.LoadScene("gameOver");
        }
        if (counter == 10)
        {
            SceneManager.LoadScene("gameOver");
        }
        if (maxlifetime > 0 && counter == 10)
        {
            SceneManager.LoadScene("WinScreen");
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            SceneManager.LoadScene("MenuScreen");
        }*/

    }

/*    private void OnDestroy()
    {
        PlayerPrefs.SetFloat("score", maxlifetime);
    }*/

/*    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "checkpoint")
        {
            Debug.Log("Yay!");
            other.gameObject.SetActive(false);
        }
        if (maxlifetime > 0.0f)
        {
            maxlifetime = checkpointtime + maxlifetime;
        }
        counter++;

        if (counter == 10)
        {
            SceneManager.LoadScene("WinScreen");
        }
    }*/
}
