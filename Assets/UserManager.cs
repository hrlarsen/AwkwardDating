using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UserManager : MonoBehaviour 
{
	public static UserManager Instance;
	[SerializeField]
	private GameObject playerPrefab;
	public Transform spawnPoint;
	public float updateRate = 5.0f;
	private float currentTime = 0;
	List<UserInfo> currentActiveUsers = new List<UserInfo>();
	// Use this for initialization
	void Start () {
		if(Instance == null)
			Instance = this;

	}
	
	// Update is called once per frame
	void Update () {
		if(currentTime > updateRate)
		{
			DatabaseInput.Instance.GetUsers(OnReceivedUsers);
			currentTime = 0;
		}
		currentTime += Time.deltaTime;
	}

	void OnReceivedUsers(List<UserInfo> users)
	{
		for(int i=0; i < users.Count; i++)
		{
			if(!currentActiveUsers.Contains(users[i]))
			{
				currentActiveUsers.Add(users[i]);
				SpawnUser(users[i]);
			}
		}
		List<int> indexesToRemove = new List<int>();
		for(int i = 0; i < currentActiveUsers.Count; i++)
		{
			if(!users.Contains(currentActiveUsers[i]))
			{
				Destroy(GameObject.Find("User_" + currentActiveUsers[i].id));
				indexesToRemove.Add(i);

			}
		}

		for(int i = 0; i < indexesToRemove.Count; i++)
		{

			currentActiveUsers.RemoveAt(indexesToRemove[i]);

		}


	}

	void SpawnUser(UserInfo info)
	{
		GameObject go = Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity) as GameObject;
		go.name = "User_" + info.id;
		go.GetComponent<UserMovement>().userID = info.id;
		go.GetComponent<UserMovement>().myColor = info.color;
		go.GetComponent<Renderer>().material.color = info.color;
	}
}
