using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] private float              yMovementSpeed;
        [field: SerializeField] public float        xMovementSpeed { get; private set; }
        [field: SerializeField] public Vector2      InputAxis       { get; private set; }


        [Header("RayChecks")]
        [SerializeField] private Vector2            tempHeightBounds;
        [SerializeField] private Vector2            tempWidthBounds;


        [SerializeField] private LayerMask          Walkable;

        [SerializeField] private float              distanceToCheckTop;
        [SerializeField] private float              distanceToCheckBottom;
        [SerializeField] private float              distanceToCheckLeft;
        [SerializeField] private float              distanceToCheckRight;

        [Header("Jump settings")]
        [SerializeField] private float              jumpHeight;
        [SerializeField] private float              JumpForce;                      // speedMulitplierUp
        [SerializeField] private float              gravity;                        // speedMulitplierDown
        [SerializeField] private AnimationCurve     accelerationUp;
        [SerializeField] private AnimationCurve     accelerationDown;
        [SerializeField] private float              horizontalVelocity;


        [Header("Debug")]
        public bool               canMove;
        [SerializeField] private bool               canJump;
        private PlayerAnimations                    animator;
        private Animator unityAnimator;

        public int LookDirection { get; private set; } = 1;

        private void Awake()
        {
            unityAnimator = GetComponent<Animator>();
        }
        private void Start()
        {
            animator = this.gameObject.GetComponent<PlayerAnimations>();
            canJump  = true;
        }


        private void Update()
        {
            #region input
            // Get walking input
            InputAxis = new Vector2(Input.GetAxisRaw("Horizontal"),
                                    Input.GetAxisRaw("Vertical"));
            if (InputAxis.x != 0f)
            {
                LookDirection = InputAxis.x > 0 ? 1 : -1;
            }
            // Get jump input.
            if (canJump)
            {
                if (Input.GetButtonDown("Jump")) StartCoroutine(Jump(InputAxis.x));
            }
            #endregion


            #region CollisionDetection
            bool canMoveLeft  = true;
            bool canMoveRight = true;
            bool canMoveUp    = true;
            bool canMoveDown  = true;

            RaycastHit2D raycastLeft   = Physics2D.Raycast(transform.position, Vector2.left,  distanceToCheckLeft);
            RaycastHit2D raycastRight  = Physics2D.Raycast(transform.position, Vector2.right, distanceToCheckRight);
            RaycastHit2D raycastBottom = Physics2D.Raycast(transform.position, Vector2.down,  distanceToCheckBottom);
            RaycastHit2D raycastTop    = Physics2D.Raycast(transform.position, Vector2.up,    distanceToCheckTop);
            #endregion


            #region Movement




            // TEMPORARY //
            if (transform.position.y >= tempHeightBounds.x) canMoveUp    = false;
            if (transform.position.y <= tempHeightBounds.y) canMoveDown  = false;
            if (transform.position.x <= tempWidthBounds.x)  canMoveLeft  = false;
            if (transform.position.x >= tempWidthBounds.y)  canMoveRight = false;
            // TEMPORARY //





            if (canMove) {
                if (!unityAnimator.GetCurrentAnimatorStateInfo(0).IsName("Light Attack") && !unityAnimator.GetCurrentAnimatorStateInfo(0).IsName("Heavy Attack"))
                {
                    if (InputAxis.x != 0 || InputAxis.y != 0)
                    {

                        // Move left.
                        if (InputAxis.x < 0 && canMoveLeft) transform.position += (new Vector3(InputAxis.x * xMovementSpeed, 0, 0) * Time.deltaTime);

                        // Move Right.
                        if (InputAxis.x > 0 && canMoveRight) transform.position += (new Vector3(InputAxis.x * xMovementSpeed, 0, 0) * Time.deltaTime);

                        // Move Up.
                        if (InputAxis.y > 0 && canMoveUp) transform.position += (new Vector3(0, InputAxis.y * yMovementSpeed, 0) * Time.deltaTime);

                        // Move Down.
                        if (InputAxis.y < 0 && canMoveDown) transform.position += (new Vector3(0, InputAxis.y * yMovementSpeed, 0) * Time.deltaTime);
                    }
                }
                
            }
            #endregion
        }


        private IEnumerator Jump(float direction)
        {
            // Lock movement.
            canMove = false;
            canJump = false;

            float startHeight = transform.position.y;

            // Snap the direction to an absolute.
                 if (direction < 0) direction = -1;
            else if (direction > 0) direction =  1;


            //Set player animation.
            animator?.SetJumpState(Jumpstate.jumping);


            // Move up.
            while (transform.position.y < startHeight + jumpHeight) {
                transform.position += (new Vector3(direction * horizontalVelocity,
                                                   accelerationUp.Evaluate((transform.position.y - startHeight) / jumpHeight) * JumpForce,
                                                   0) * Time.deltaTime);

                // Clamp position.
                transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, startHeight, startHeight + jumpHeight), transform.position.z);

                yield return new WaitForEndOfFrame();
            }


            //Set player animation.
            animator?.SetJumpState(Jumpstate.falling);


            // Move down
            while (transform.position.y > startHeight)
            {
                transform.position += (new Vector3(direction* horizontalVelocity,
                                                   -accelerationDown.Evaluate(1 - (transform.position.y - startHeight) / jumpHeight) * gravity,
                                                    0) * Time.deltaTime);

                // Clamp position.
                transform.position  = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, startHeight, startHeight + jumpHeight), transform.position.z);

                yield return new WaitForEndOfFrame();
            }

            //Set player animation.
            if (animator)
            {
                animator.SetJumpState(Jumpstate.landing);
                yield return new WaitForSeconds(animator.AnimationDuration() * animator.LandSpeed);
                animator.SetJumpState(Jumpstate.grounded);
            }


            // Unlock movement.
            canJump = true;
            canMove = true;
        }
    }
}

