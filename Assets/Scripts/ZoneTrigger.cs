﻿using UnityEngine;
using System.Collections;

public class ZoneTrigger : MonoBehaviour {
    public int counter;
	public AvatarMotionPlay avatar;
    public int impulseCounter;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(impulseCounter > 0){
			avatar.SendImpulse(AnimationToPlay.Handshake);
			impulseCounter = 0;
		}
	}
}
