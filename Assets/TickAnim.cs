using UnityEngine;
using System.Collections;

public class TickAnim : MonoBehaviour {

	Vector3 targetSize = new Vector3(1f,1f,1f);
	// Use this for initialization
	void Start () {
		transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(transform.localScale.x > targetSize.x - 0.2f)
			Destroy(this.gameObject);
		else
			transform.localScale = Vector3.Lerp(transform.localScale, targetSize, Time.deltaTime * 8);
	}
}
