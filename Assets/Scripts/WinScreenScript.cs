using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WinScreenScript : MonoBehaviour
{

    public float score = 0.0f;
    public Text resultText;
    public Text descriptionText;
    public Text endTimeText;

    // Use this for initialization
    void Start()
    {
        Cursor.visible = true;
        score = PlayerPrefs.GetFloat("score");
        resultText.text = "You Win!";
        descriptionText.text = "You live to race another day";
        endTimeText.text = "Your Time: " + score;

    }

    // Update is called once per frame
    void Update()
    {


    }
}