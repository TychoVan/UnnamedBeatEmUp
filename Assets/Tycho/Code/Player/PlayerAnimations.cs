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
        [SerializeField] private PlayerState    animationState;

        [Header("Idle")]
        [SerializeField] private float          idleAnimSpeed;

        [Header("Walking")]
        [SerializeField] private float          walkAnimSpeed               = 1;
        [SerializeField] private bool           multiplyAnimMoveSpeed;

        [Header("Jumping")]
        [SerializeField] private float          jumpSpeed                   = 1;
        [SerializeField] private float          fallSpeed                   = 1;
        [SerializeField] private AnimationClip  landingAnimation;
        [field: SerializeField] public float    LandSpeed                   = 1;

        // Private 
        private Jumpstate                       jumpstate;

        private Animator                        animator;
        private PlayerMovement                  movementScript;

        private Vector3                         startScale;     // the scale of this object at the start of the game.
        private AnimationClip                   currentAnimation;




        public float AnimationDuration() { return currentAnimation ? landingAnimation.length : 0; }

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
            // Flip the Player sprite to movement direction and play walking animation.
            if (movementScript.canMove && movementScript.InputAxis.x != 0 || movementScript.canMove && movementScript.InputAxis.y != 0) {
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
                        PlayAnimation("AnimationState", 0, idleAnimSpeed);
                        break;

                    case PlayerState.walking:
                        if (multiplyAnimMoveSpeed) animator.speed = walkAnimSpeed * movementScript.xMovementSpeed;
                        else                       animator.speed = walkAnimSpeed;
                        PlayAnimation("AnimationState", 1, walkAnimSpeed);
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
                    PlayAnimation("JumpState", 1, jumpSpeed);
                    break;

                case Jumpstate.falling:
                    PlayAnimation("JumpState", 2, fallSpeed);
                    break;

                case Jumpstate.landing:
                    PlayAnimation("JumpState", 3, LandSpeed);
                    break;

                default:
                    animator.SetInteger("JumpState", 0);
                    animationState = PlayerState.idle;
                    break;
            }
        }


        /// <summary>
        /// Play animation in animator.
        /// </summary>
        /// <param name="animationType">Name of integer parameter.</param>
        /// <param name="animationNumber">Integer number of animation.</param>
        /// <param name="speed">Speed of the animation being played.</param>
        public void PlayAnimation(string animationType, int animationNumber, float speed)
        {
            animator.speed = speed;
            animator.SetInteger(animationType, animationNumber);
        }

    }
}
