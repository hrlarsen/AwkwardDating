using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UserMovement : MonoBehaviour {

	public int userID = 16;
	public Color32 myColor;
	public ZoneTrigger myLastZone;
	private Renderer myRenderer;
	// Use this for initialization
	void Start () {
		DatabaseInput.Instance.userDataCallbacks += OnCallback;
		myRenderer = GetComponent<Renderer>();
	}

	void OnTriggerEnter(Collider trigger)
	{
		//Debug.Log("The trigger has been triggered:" + trigger.name);
		
		trigger.GetComponent<ZoneTrigger>().counter++;
		
		myLastZone = trigger.GetComponent<ZoneTrigger>();
		
	}
	void OnTriggerExit(Collider trigger)
	{
		trigger.GetComponent<ZoneTrigger>().counter--;
		//Debug.Log("The trigger has been NOT triggered:" + trigger.name);
		
		//myLastZone = null;
	}
	
	void OnCallback(List<UserMovementDataObject> data)
	{
		for(int i = 0; i < data.Count; i++)
		{
			if(data[i].id == userID)
			{
//				Debug.Log(data[i].movement);
				//transform.position += new Vector3(data[i].movement.x, data[i].movement.y,0) * 0.05f;
				if(data[i].movement != null)
					GetComponent<Rigidbody>().AddForce(new Vector3(data[i].movement.x, data[i].movement.y,0) * 10, ForceMode.Force);
				//if(data[i].ticks > 0)
				//	Debug.Log("TICKS: " + data[i].ticks);
//				Debug.Log(myLastZone);
				if(data[i].ticks > 0 && myLastZone != null)
				{
					myLastZone.impulseCounter += data[i].ticks;
					// SPAWN SOMETHING
					myRenderer.material.color = Color.white;
				}
				else
				{
					myRenderer.material.color = myColor;
				}

				// whatever use data[i].ticks 

				break;
			}
		}
	}

	void OnDestroy()
	{
		DatabaseInput.Instance.userDataCallbacks -= OnCallback;
	}
}
