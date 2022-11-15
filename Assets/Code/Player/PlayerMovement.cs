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
            RaycastHit2D raycastBottom = Physics2D.Raycast(transform.position, Vector2.down, distanceToCheckBottom);
            if (raycastBottom.collider != null)
            {
                if (raycastBottom.collider.CompareTag("Walkable"))
                {

                }
            }
            #endregion


            #region Movement
            if (canMove) {
                if (inputAxis.x != 0 || inputAxis.y != 0) {

                    //------- Animation Walking -------\\

                    transform.position += (new Vector3(inputAxis.x * xMovementSpeed,
                                                       inputAxis.y * yMovementSpeed,
                                                       0) * Time.deltaTime);
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

            Debug.Log(direction);

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

