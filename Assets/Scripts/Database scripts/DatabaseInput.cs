using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DatabaseInput : MonoBehaviour 
{
	private readonly string secret =  "PCJzWZKr*k!#%hYX2gMp";
	private string getURL = "http://tunnelvisiongames.com/twitchplaysoctodad/GetUserData.php";
	private string getUsersURL = "http://tunnelvisiongames.com/twitchplaysoctodad/GetUsers.php";
	public delegate void UserDataDelegate(List<UserMovementDataObject> userDataCollection);
	public delegate void UsersDelegate(List<UserInfo> users);
	public UserDataDelegate userDataCallbacks;
	public static DatabaseInput Instance;
	public float updateRate = 0.25f;
	private float currentTimer = 0;
	// Use this for initialization
	void Awake () {
		if(Instance == null)
			Instance = this;

		StartCoroutine(GetUserDataDB());
	}

	void Update()
	{
		if(userDataCallbacks == null)
			return;
		if(currentTimer > updateRate)
		{
			StartCoroutine(GetUserDataDB());
			currentTimer = 0;
		}
		currentTimer += Time.deltaTime;
	}

	public void GetUsers(UsersDelegate callback)
	{
		StartCoroutine(GetUsersDB(callback));
	}

	private IEnumerator GetUsersDB(UsersDelegate callback)
	{
		WWWForm form = new WWWForm();
		form.AddField("secret", secret);
		WWW www = new WWW(getUsersURL, form);
		yield return www;
		if(www.error == null)
		{
			List<UserInfo> users = new List<UserInfo>();
			JSONObject jsonObj = new JSONObject(www.text);

			for(int i=0; i < jsonObj.Count; i++)
			{
				UserInfo user = new UserInfo();
				user.id = (int)jsonObj[i].GetField("id").n;
				user.color = HexToColor(jsonObj[i].GetField("color").str);
				users.Add(user);
			}
			if(callback != null)
				callback(users);
		}

	}

	private IEnumerator GetUserDataDB()
	{
		WWWForm form = new WWWForm();
		form.AddField("secret", secret);
		WWW www = new WWW(getURL,form);
		yield return www;

		if(www.error == null)
		{
			List<UserMovementDataObject> data = new List<UserMovementDataObject>();
			JSONObject jsonObj = new JSONObject(www.text);
			//Debug.Log(jsonObj.Count);
			int currentIndex = -1;
			for(int i = 0; i < jsonObj.Count; i++)
			{

				//Debug.Log(jsonObj[i].ToString());
				int id = (int)jsonObj[i].GetField("id").n;

				Vector2 movementVector = new Vector2();
				movementVector.x = jsonObj[i].GetField("x").n;
				movementVector.y = -jsonObj[i].GetField("y").n; // reverse y
				movementVector.Normalize();
				if(data.Count > 0)
				{
					if(data[currentIndex].id == id)
					{
						if(movementVector == Vector2.zero)
							data[currentIndex].ticks++;
						else
							data[currentIndex].movement += movementVector;
					}
					else
					{
						UserMovementDataObject user = new UserMovementDataObject();
						user.id = id;
						user.movement = movementVector;
						data.Add(user);
						currentIndex++;
					}
				}else
				{
					UserMovementDataObject user = new UserMovementDataObject();
					user.id = id;
					user.movement = movementVector;
					data.Add(user);
					currentIndex++;
				}
				
			}
			//if(data.Count > 0)
				//Debug.Log(data[0].id);
			if(userDataCallbacks != null)
			{
				userDataCallbacks(data);

			}
		}
	}

	Color HexToColor(string hex)
	{
		byte r = byte.Parse(hex.Substring(0,2), System.Globalization.NumberStyles.HexNumber);
		byte g = byte.Parse(hex.Substring(2,2), System.Globalization.NumberStyles.HexNumber);
		byte b = byte.Parse(hex.Substring(4,2), System.Globalization.NumberStyles.HexNumber);
		return new Color32(r,g,b, 255);
	}
}
