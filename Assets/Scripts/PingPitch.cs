using UnityEngine;
using System.Collections;

public class PingPitch : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
	    GetComponent<AudioSource>().pitch = Random.Range(-3, 3);
	    GetComponent<AudioSource>().Play();

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
