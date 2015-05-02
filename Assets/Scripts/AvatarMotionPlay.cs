using UnityEngine;
using System.Collections;

public enum AnimationToPlay
{
    None,
    Handshake,
    DrinkWine,
    Kiss,
    Eat
}

public class AvatarMotionPlay : MonoBehaviour
{

    public float PlaySpeedForward = 0;
    public float PlaySpeedBackward = 0;
    public float AnimatorSpeed;
    public float ImpulseMultiplier = 2;
    public float DecreaseMultiplier = 1;
    public float ImpulsePressingCooldownThreshold = 0.2f;
    public AnimationToPlay CurrentAnimation = AnimationToPlay.None;

	public SpasticForce head, leftArm, rightArm, leftLeg, rightLeg;

    protected Animator animator;
    private float ImpulsePressingCooldown;
    private bool IsSendingImpulse;

    // Use this for initialization
    private void Start()
    {
        animator = GetComponent<Animator>();
        CurrentAnimation = AnimationToPlay.None;
        animator.speed = 1;

    }

    public void SendImpulse(AnimationToPlay anim, Zones zone)
	{
        IsSendingImpulse = true;
		if(zone == ZoneManager.Instance.correctZone)
		{
			if (CurrentAnimation != anim)
			{
				animator.SetTrigger(anim.ToString());
				CurrentAnimation = anim;
				//Debug.Log("New animation set");
			}
		}
		else
		{
			switch(zone)
			{
			case Zones.LeftArm:
				leftArm.AddImpulseForce();
				break;
			case Zones.RightArm:
				rightArm.AddImpulseForce();
				break;
			case Zones.LeftLeg:
				leftLeg.AddImpulseForce();
				break;
			case Zones.RightLeg:
				rightLeg.AddImpulseForce();
				break;
			case Zones.LeftHead:
				head.AddImpulseForce();
				break;
			case Zones.RightHead:
				head.AddImpulseForce();
				break;
			};
		}
        
        
    }

    // Update is called once per frame
    private void Update()
    {
        return;
        /*if (Input.GetKeyDown(KeyCode.A))
            SendImpulse(AnimationToPlay.Eat);

        if (Input.GetKeyDown(KeyCode.S))
            SendImpulse(AnimationToPlay.Handshake);*/

        if (IsSendingImpulse)
        {

            PlaySpeedForward += Time.deltaTime * ImpulseMultiplier;

            animator.speed = PlaySpeedForward;

            ImpulsePressingCooldown += Time.deltaTime;

            if (ImpulsePressingCooldown > ImpulsePressingCooldownThreshold)
            {
                ImpulsePressingCooldown = 0;

                IsSendingImpulse = false;
            }
        }
        else if (!IsSendingImpulse)
        {
            PlaySpeedBackward += Time.deltaTime * DecreaseMultiplier;
            PlaySpeedForward -= Time.deltaTime;

            animator.speed = -PlaySpeedBackward;
        }

        PlaySpeedBackward = Mathf.Clamp(PlaySpeedBackward, 0f, 1f);
        PlaySpeedForward = Mathf.Clamp(PlaySpeedForward, 0f, 1f);

        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1) // animation end
            animator.ForceStateNormalizedTime(1f);
        else if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0) // animation start
            animator.ForceStateNormalizedTime(0f);

        AnimatorSpeed = animator.speed;



    }
}
