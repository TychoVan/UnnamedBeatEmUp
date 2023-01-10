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
        [SerializeField] private LayerMask          Walkable;
        private                  int                walkLayer;

        [SerializeField] private float              distanceToCheckTop;
        [SerializeField] private float              distanceToCheckBottom;
        [SerializeField] private float              distanceToCheckLeft;
        [SerializeField] private float              distanceToCheckRight;

        [SerializeField] private bool               drawRaycheckGizmos;
        [field: SerializeField] public bool InBounds { get; private set; }

        private Vector2 pointA;
        private Vector2 pointB;
        private Vector2 pointC;
        private Vector2 pointD;
        private Vector2 pointE;
        private Vector2 pointF;

        [Header("Jump settings")]
        [SerializeField] private float              jumpHeight;
        [SerializeField] private float              JumpForce;                      // speedMulitplierUp
        [SerializeField] private float              gravity;                        // speedMulitplierDown
        [SerializeField] private AnimationCurve     accelerationUp;
        [SerializeField] private AnimationCurve     accelerationDown;
        [SerializeField] private float              horizontalVelocity;
        [SerializeField] private float              maxHorizontalMovement;
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
            float walkLayer = Mathf.Log(Walkable.value, 2);

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
            animator.SetBool ("Walking",       InputAxis.x == 0  ? (InputAxis.y == 0 ? false: true) : true);
            animator.SetFloat("Walking Speed", xWalkSpeed);

            // Set rotation
            if (InputAxis.x != 0f && canMove)
            {
                LookDirection = InputAxis.x > 0 ? 1 : -1;
                transform.localScale = new Vector3(startLocalScale.x * LookDirection, transform.localScale.y, transform.localScale.z);
            }
            #endregion


            #region CollisionDetection
            bool canMoveLeft;
            bool canMoveRight;
            bool canMoveUp;
            bool canMoveDown;

            pointA = new Vector2(transform.position.x - distanceToCheckLeft,  transform.position.y + distanceToCheckTop);
            pointB = new Vector2(transform.position.x + distanceToCheckRight, transform.position.y + distanceToCheckTop);
            pointC = new Vector2(transform.position.x + distanceToCheckRight, transform.position.y - distanceToCheckBottom);
            pointD = new Vector2(transform.position.x - distanceToCheckLeft,  transform.position.y - distanceToCheckBottom);

            pointE  = new Vector2(transform.position.x  + (distanceToCheckRight + maxHorizontalMovement * LookDirection), transform.position.y + distanceToCheckTop);
            pointF  = new Vector2(transform.position.x +  (distanceToCheckRight + maxHorizontalMovement * LookDirection), transform.position.y - distanceToCheckBottom);

            //Debug.Log("Start: " + (pointB) + "|| Direction: " + (pointC - pointB) + "|| Distance: " + (pointC.y - pointB.y));
            //Debug.Log("Start: " + (pointD) + "|| Direction: " + (pointA - pointD) + "|| Distance: " + (pointA.y - pointD.y));


            RaycastHit2D raycastTop   = Physics2D.Raycast(pointA, pointB - pointA, distanceToCheckLeft + distanceToCheckRight);
            RaycastHit2D raycastRight = Physics2D.Raycast(pointB, pointC - pointB, distanceToCheckTop  + distanceToCheckBottom);
            RaycastHit2D raycastDown  = Physics2D.Raycast(pointC, pointD - pointC, distanceToCheckLeft + distanceToCheckRight);
            RaycastHit2D raycastLeft  = Physics2D.Raycast(pointD, pointA - pointD, distanceToCheckTop  + distanceToCheckBottom);

            //canMoveUp    = raycastTop;
            //canMoveRight = raycastRight;
            //canMoveDown  = raycastDown;
            //canMoveLeft  = raycastLeft;


            //Debug.Log(raycastTop.transform.gameObject.layer == walkLayer ? true : false);
            //Debug.Log("Layer: " + raycastTop.transform.gameObject.layer + " Wanted: " + walkLayer);

            canMoveUp    = raycastTop   ? raycastTop  .transform.gameObject.layer == walkLayer ? true : false : false;
            canMoveRight = raycastRight ? raycastRight.transform.gameObject.layer == walkLayer ? true : false : false;
            canMoveDown  = raycastDown  ? raycastDown .transform.gameObject.layer == walkLayer ? true : false : false;
            canMoveLeft  = raycastLeft  ? raycastLeft .transform.gameObject.layer == walkLayer ? true : false : false;

            //if (!raycastTop || !raycastRight || !raycastDown || !raycastLeft) InBounds = false;
            //else                                                              InBounds = true;


            #endregion


            #region Movement
            if (canMove) {
               
                    if (InputAxis.x != 0 || InputAxis.y != 0)
                    {

                        // Move left.
                        if (InputAxis.x < 0 && canMoveLeft) transform.position  += (new Vector3(InputAxis.x * xWalkSpeed, 0, 0) * Time.deltaTime);

                        // Move Right.
                        if (InputAxis.x > 0 && canMoveRight) transform.position += (new Vector3(InputAxis.x * xWalkSpeed, 0, 0) * Time.deltaTime);

                        // Move Up.
                        if (InputAxis.y > 0 && canMoveUp) transform.position    += (new Vector3(0, InputAxis.y * yWalkSpeed, 0) * Time.deltaTime);

                        // Move Down.
                        if (InputAxis.y < 0 && canMoveDown) transform.position  += (new Vector3(0, InputAxis.y * yWalkSpeed, 0) * Time.deltaTime);
                    }
                
                
            }
            #endregion
        }


        private IEnumerator Jump(float direction)
        {
            // Lock movement.
            canMove             = false;

            float startHeight   = transform.position.y;

            // Set jumpheight according to direction
            RaycastHit2D maxJumpDist = Physics2D.Raycast(pointE, pointF - pointE, distanceToCheckTop + distanceToCheckBottom);

            // if there is a block in the way dont jump sideways
            direction = direction == 0 ?             
                0 : 
                LookDirection = maxJumpDist ? 
                    maxJumpDist.transform.gameObject.layer == walkLayer ? 
                        LookDirection 
                        : 0 
                    : 0;

            float modifiedJumpHeight = direction == 0 ? jumpHeight : movingJumpHeight;



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

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (drawRaycheckGizmos)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawLine(pointA, pointB);
                Gizmos.DrawLine(pointB, pointC);
                Gizmos.DrawLine(pointC, pointD);
                Gizmos.DrawLine(pointD, pointA);

                Gizmos.color = Color.yellow;
                Gizmos.DrawLine(transform.position, new Vector2(pointE.x, transform.position.y));
                Gizmos.DrawLine(pointE, pointF);
            }
        }
#endif
    }
}

