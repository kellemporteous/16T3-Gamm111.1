using UnityEngine;
using System.Collections;

public class Camera_Controller : MonoBehaviour {

    public GameObject player;
    public Vector3 offset;

	// Use this for initialization
	void Start ()
    {
        offset = transform.position - player.transform.position;
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = player.transform.position + offset;
	}
}
