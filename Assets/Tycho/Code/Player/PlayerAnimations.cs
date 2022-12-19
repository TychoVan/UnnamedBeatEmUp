using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Player{

    public enum PlayerState { idle, walking, jumping, attack1, attack2 }
    public enum Jumpstate {grounded, jumping, falling, landing}

    [RequireComponent(typeof(PlayerMovement))]
    [RequireComponent(typeof(Animator))]

    public class PlayerAnimations : MonoBehaviour
    {
        [Header("Idle")]
        [SerializeField] private float          idleAnimSpeed;

        [Header("Walking")]
        [SerializeField] private float          walkAnimSpeed               = 1;
        [SerializeField] private bool           multiplyAnimMoveSpeed;

        [Header("Jumping")]
        [SerializeField] private float          jumpSpeed                   = 1;
        [SerializeField] private float          fallSpeed                   = 1;
        [SerializeField] private AnimationClip  landingAnimation;
        [SerializeField] private float          landSpeed                   = 1;

        // Private 
        [SerializeField] private PlayerState                     animationState;
        private Jumpstate                       jumpstate;

        private Animator                        animator;
        private PlayerMovement                  movementScript;

        private Vector3                         startScale;     // the scale of this object at the start of the game.




        public float LandingDuration() { return landingAnimation? landingAnimation.length * landSpeed :  0; }

        private void Awake()
        {
            animator       = this.gameObject.GetComponent<Animator>();
            movementScript = this.gameObject.GetComponent<PlayerMovement>();
            startScale     = transform.localScale;
            jumpstate      = Jumpstate.grounded;
            animationState = PlayerState.idle;
        }

        public float Mirrored() {
            if (movementScript.InputAxis.x < 0) return -1 * startScale.x;
            else                                return  1 * startScale.x;
        }


        private void Update()
        {
            // Flip the 
            if (movementScript.InputAxis.x != 0 || movementScript.InputAxis.y != 0) {
                transform.localScale = new Vector3(Mirrored(),
                                                   transform.localScale.y,
                                                   transform.localScale.z);

                SetAnimationState(PlayerState.walking);
            }
            else SetAnimationState(PlayerState.idle);
        }

        private void SetAnimationState(PlayerState state)
        {
            if (state != animationState && animationState != PlayerState.jumping)
            {
                animationState = state;

                switch (animationState)
                {
                    case PlayerState.idle:
                        animator.speed = idleAnimSpeed;
                        animator.SetInteger("AnimationState", 0);
                        break;

                    case PlayerState.walking:
                        if (multiplyAnimMoveSpeed) animator.speed = walkAnimSpeed * movementScript.xMovementSpeed;
                        else                       animator.speed = walkAnimSpeed;
                        animator.SetInteger("AnimationState", 1);
                        break;

                    default:
                        Debug.LogWarning(this.gameObject.name + " animation state not defined.");
                        break;
                }
            }
        }

        public void SetJumpState(Jumpstate state)
        {
            jumpstate = state;

            switch (jumpstate)
            {
                case Jumpstate.jumping:
                    animator.speed = jumpSpeed;
                    animator.SetInteger("JumpState", 1);
                    break;

                case Jumpstate.falling:
                    animator.speed = fallSpeed;
                    animator.SetInteger("JumpState", 2);
                    break;

                case Jumpstate.landing:
                    animator.SetInteger("JumpState", 3);
                    break;

                default:
                    animator.SetInteger("JumpState", 0);
                    animationState = PlayerState.idle;
                    break;
            }
        }

    }
}
