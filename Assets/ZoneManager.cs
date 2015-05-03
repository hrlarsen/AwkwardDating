using UnityEngine;
using System.Collections;

public class ZoneManager : MonoBehaviour {
	public static ZoneManager Instance;


	public AnimationToPlay correctAnimation;
	public Zones correctZone;

    public Transform Spawner;

    public GameObject MainCamera;

    public float PlayerSpeed = 5f;
    public float CameraMoveSpeed = 5f;

	// Use this for initialization
	void Start () {
		if(Instance == null)
			Instance = this;
	}
	

}
