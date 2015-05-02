using UnityEngine;
using System.Collections;

public class ZoneManager : MonoBehaviour {
	public static ZoneManager Instance;


	public AnimationToPlay correctAnimation;
	public Zones correctZone;
	// Use this for initialization
	void Start () {
		if(Instance == null)
			Instance = this;
	}
	

}
