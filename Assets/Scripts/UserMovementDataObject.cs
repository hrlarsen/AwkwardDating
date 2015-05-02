using UnityEngine;
using System.Collections;

public class UserMovementDataObject {
	public int id;
	public Vector2 movement;
	public int ticks;

	public override bool Equals (object obj)
	{
		return id.Equals((int)obj);
	}
}
