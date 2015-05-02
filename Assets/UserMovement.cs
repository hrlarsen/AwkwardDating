using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UserMovement : MonoBehaviour {

	public int userID = 16;
	// Use this for initialization
	void Start () {
		DatabaseInput.Instance.userDataCallbacks += OnCallback;
	}
	
	void OnCallback(List<UserMovementDataObject> data)
	{
		for(int i = 0; i < data.Count; i++)
		{
			if(data[i].id == userID)
			{
//				Debug.Log(data[i].movement);
				//transform.position += new Vector3(data[i].movement.x, data[i].movement.y,0) * 0.05f;
				GetComponent<Rigidbody>().AddForce(new Vector3(data[i].movement.x, data[i].movement.y,0) * 10, ForceMode.Force);
				break;
			}
		}
	}
}
