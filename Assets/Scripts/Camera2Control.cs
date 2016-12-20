using UnityEngine;
using System.Collections;

public class Camera2Control : MonoBehaviour {
    public GameObject Canvas;
    private Vector3 offset;
    // Use this for initialization
    void Start () {
        offset = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    private void LateUpdate()
    {
        transform.position = Canvas.transform.position + offset;
    }
}
