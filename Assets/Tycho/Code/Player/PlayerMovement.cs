using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Movement")]
        private Vector2                             inputAxis;
        [SerializeField] private float              xMovementSpeed;
        [SerializeField] private float              yMovementSpeed;

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
        [SerializeField] private bool               canMove;
        [SerializeField] private bool               canJump;




        private void Start()
        {
            canJump = true;
        }


        private void Update()
        {
            #region input
            // Get walking input
            inputAxis = new Vector2(Input.GetAxisRaw("Horizontal"),
                                    Input.GetAxisRaw("Vertical"));

            // Get jump input.
            if (canJump)
            {
                if (Input.GetButtonDown("Jump")) StartCoroutine(Jump(inputAxis.x));
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
            if (transform.position.y >= tempHeightBounds.x) canMoveUp   = false;
            if (transform.position.y <= tempHeightBounds.y) canMoveDown = false;
            if (transform.position.x <= tempWidthBounds.x) canMoveLeft  = false;
            if (transform.position.x >= tempWidthBounds.y) canMoveRight = false;
            // TEMPORARY //





            if (canMove) {
                if (inputAxis.x != 0 || inputAxis.y != 0) {

                    //------- Animation Walking -------\\

                    // Move left.
                    if (inputAxis.x < 0 && canMoveLeft)  transform.position += (new Vector3(inputAxis.x * xMovementSpeed, 0, 0) * Time.deltaTime);

                    // Move Right.
                    if (inputAxis.x > 0 && canMoveRight) transform.position += (new Vector3(inputAxis.x * xMovementSpeed, 0, 0) * Time.deltaTime);

                    // Move Up.
                    if (inputAxis.y > 0 && canMoveUp)    transform.position += (new Vector3(0, inputAxis.y * yMovementSpeed, 0) * Time.deltaTime);

                    // Move Down.
                    if (inputAxis.y < 0 && canMoveDown)  transform.position += (new Vector3(0, inputAxis.y * yMovementSpeed, 0) * Time.deltaTime);
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

            //if (transform.position.x + direction * horizontalVelocity <= tempWidthBounds.x || 
            //    transform.position.x + direction * horizontalVelocity >= tempWidthBounds.y) { 
            //    direction = 0; 
            //}

                //------- Animation Startjump -------\\

                // Move up.
                while (transform.position.y < startHeight + jumpHeight) {
                transform.position += (new Vector3(direction * horizontalVelocity,
                                                   accelerationUp.Evaluate((transform.position.y - startHeight) / jumpHeight) * JumpForce,
                                                   0) * Time.deltaTime);

                // Clamp position.
                transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, startHeight, startHeight + jumpHeight), transform.position.z);

                yield return new WaitForEndOfFrame();
            }


            //------- Animation EndJump -------\\

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

            // Unlock movement.
            canJump = true;
            canMove = true;
        }
    }
}

