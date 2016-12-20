using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreUpdater : MonoBehaviour {

	// Update is called once per frame
	void Update () {

        GameObject go = GameObject.Find("GameManager");
        if (go == null)
        {
            Debug.LogError("Failed to find object named 'GameManager'");
            this.enabled = false;
            return;
        }

        GameManager gm = go.GetComponent<GameManager>();

        GetComponent<Text>().text = "Counter: " + gm.counter + "\n" + "Time: " + gm.maxlifetime;
    }
}
