using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] private float              yWalkSpeed;
        [SerializeField] private float              xWalkSpeed;
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
        [SerializeField] private float              movingJumpHeight;


        [Header("Debug")]
        public bool                                 canMove;
        private Animator                            animator;
        private Vector3                             startLocalScale;

        public int LookDirection { get; private set; } = 1;

        private void Awake()
        {
            animator        = GetComponent<Animator>();
            startLocalScale = transform.localScale;
        }


        private void Update()
        {
            #region input
            // Get walking input
            InputAxis = new Vector2(Input.GetAxisRaw("Horizontal"),
                                    Input.GetAxisRaw("Vertical"));

            // Get jump input.
            if (canMove)
            {
                if (Input.GetButtonDown("Jump")) StartCoroutine(Jump(InputAxis.x));
            }
            #endregion


            #region Animation
            // Trigger walking animation and set its speed
            animator.SetBool ("Walking",       InputAxis.x == 0 ? false : true);
            animator.SetFloat("Walking Speed", xWalkSpeed);

            // Set rotation
            if (InputAxis.x != 0f && canMove)
            {
                LookDirection = InputAxis.x > 0 ? 1 : -1;
                transform.localScale = new Vector3(startLocalScale.x * LookDirection, transform.localScale.y, transform.localScale.z);
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
               
                    if (InputAxis.x != 0 || InputAxis.y != 0)
                    {

                        // Move left.
                        if (InputAxis.x < 0 && canMoveLeft) transform.position += (new Vector3(InputAxis.x * xWalkSpeed, 0, 0) * Time.deltaTime);

                        // Move Right.
                        if (InputAxis.x > 0 && canMoveRight) transform.position += (new Vector3(InputAxis.x * xWalkSpeed, 0, 0) * Time.deltaTime);

                        // Move Up.
                        if (InputAxis.y > 0 && canMoveUp) transform.position += (new Vector3(0, InputAxis.y * yWalkSpeed, 0) * Time.deltaTime);

                        // Move Down.
                        if (InputAxis.y < 0 && canMoveDown) transform.position += (new Vector3(0, InputAxis.y * yWalkSpeed, 0) * Time.deltaTime);
                    }
                
                
            }
            #endregion
        }


        private IEnumerator Jump(float direction)
        {
            // Lock movement.
            canMove             = false;


            float startHeight   = transform.position.y;
            direction           = direction == 0 ? 0 : LookDirection; 


            // Set jumpheight according to direction
            float               modifiedJumpHeight;
            if (direction == 0) modifiedJumpHeight = jumpHeight;
            else                modifiedJumpHeight = movingJumpHeight;


            //Set player animation.
            animator.SetTrigger("Jump");


            // Move up.
            while (transform.position.y < startHeight + modifiedJumpHeight) {
                transform.position += (new Vector3(direction * horizontalVelocity,
                                                   accelerationUp.Evaluate((transform.position.y - startHeight) / modifiedJumpHeight) * JumpForce,
                                                   0) * Time.deltaTime);

                // Clamp position.
                transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, startHeight, startHeight + modifiedJumpHeight), transform.position.z);

                yield return new WaitForEndOfFrame();
            }


            //Set player animation.
            animator.SetTrigger("Fall");


            // Move down
            while (transform.position.y > startHeight)
            {
                transform.position += (new Vector3(direction* horizontalVelocity,
                                                   -accelerationDown.Evaluate(1 - (transform.position.y - startHeight) / modifiedJumpHeight) * gravity,
                                                    0) * Time.deltaTime);

                // Clamp position.
                transform.position  = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, startHeight, startHeight + modifiedJumpHeight), transform.position.z);

                yield return new WaitForEndOfFrame();
            }

            //Set player animation.
            if (animator)
            {
                animator.SetTrigger("Land");
            }


            // Unlock movement.
            canMove = true;
        }
    }
}

