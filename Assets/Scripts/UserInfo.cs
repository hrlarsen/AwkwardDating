using UnityEngine;
using System.Collections;

public class UserInfo  {

	public int id;
	public Color32 color;

	public override bool Equals (object obj)
	{
		UserInfo item = obj as UserInfo;

		return id.Equals(item.id);
	}

	public override int GetHashCode(){
		return id.GetHashCode();
	}
}
