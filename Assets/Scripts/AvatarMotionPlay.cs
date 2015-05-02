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

    public void SendImpulse(AnimationToPlay anim)
    {
        IsSendingImpulse = true;

        string animationName = "";
        switch (anim)
        {
            case AnimationToPlay.Handshake:
                animationName = "PlayHandShakeTrigger";
                break;

            case AnimationToPlay.DrinkWine:
                animationName = "PlayWineTrigger";
                break;

            case AnimationToPlay.Kiss:
                animationName = "PlayKissTrigger";
                break;

            case AnimationToPlay.Eat:
                animationName = "PlayEatTrigger";
                break;
        }

        
        if (CurrentAnimation != anim)
        {
            animator.SetTrigger(animationName);
            CurrentAnimation = anim;
            //Debug.Log("New animation set");
        }
    }

    // Update is called once per frame
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.A))
            SendImpulse(AnimationToPlay.Eat);

        if (Input.GetKeyDown(KeyCode.S))
            SendImpulse(AnimationToPlay.Handshake);

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
