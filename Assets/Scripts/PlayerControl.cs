using UnityEngine;
using System.Collections;
using UnityEngine.UI; //with canvas
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour {
    public float speed;
    private int counter;
    public Text txt; // UI text
    public Text endText;
    //private float seconds = 0.0f;
    private float maxlifetime = 15.0f;
    private float checkpointtime = 15.0f;
    private bool isdead = false;
	// Use this for initialization
	void Start () {
        counter = 0;
        txt.text = "Count: " + counter.ToString();
	}
	
	// Update is called once per frame
	void Update () {
        if (maxlifetime > 0)
        {
            maxlifetime -= Time.deltaTime;
        }
        txt.text = "Count: " + counter.ToString() + "\n" + maxlifetime.ToString();
        if (maxlifetime <= 0)
        {
            isdead = true;
        }
        if (isdead == true)
        {
            SceneManager.LoadScene("first3dGame");
        }
        if (maxlifetime > 0 && counter == 29)
        {
            SceneManager.LoadScene("gameOver");
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            SceneManager.LoadScene("first3dGame");
        }
        
	}

    void FixedUpdate()
    {
        float moveVertical;
        float moveHorizontal;
        moveVertical = Input.GetAxis("Vertical");
        moveHorizontal = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        GetComponent<Rigidbody>().AddForce(movement * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "notice")
        {
            Debug.Log("Yay!");
            other.gameObject.SetActive(false);
        }
        if(maxlifetime > 0.0f)
        {
            maxlifetime = checkpointtime + maxlifetime;
        }
        counter++;
        txt.text = "Count: " + counter.ToString();
        if(counter == 29)
        {
            SceneManager.LoadScene("gameOver");
        } 
    }
}
