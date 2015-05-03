using UnityEngine;
using System.Collections;

public class ZoneTrigger : MonoBehaviour {
    public int counter;
	public AvatarMotionPlay avatar;
    public int impulseCounter;
	public Zones zone;
	public AnimationToPlay anim;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(impulseCounter > 0){
			avatar.SendImpulse(anim,zone);
			impulseCounter = 0;
		}
	}
}

public enum Zones{
	LeftHead,
	RightHead,
	LeftArm,
	RightArm,
	LeftLeg,
	RightLeg
}
