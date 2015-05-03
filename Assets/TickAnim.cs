using UnityEngine;
using System.Collections;

public class TickAnim : MonoBehaviour {

	Vector3 targetSize = new Vector3(2f,2f,2f);
	// Use this for initialization
	void Start () {
		transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
	}

    private float timer;

	// Update is called once per frame
	void Update ()
	{
	    timer+= Time.deltaTime;

        if (timer > 0.3f)
			Destroy(this.gameObject);

		if(transform.localScale.x < targetSize.x - 0.2f)
			transform.localScale = Vector3.Lerp(transform.localScale, targetSize, Time.deltaTime * 8);
	}
}
