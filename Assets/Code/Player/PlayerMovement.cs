using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private Vector2 inputAxis;
        [SerializeField] private bool    canJump;
        [SerializeField] private bool    canMove;

        [SerializeField] private float xMovementSpeed;
        [SerializeField] private float yMovementSpeed;

        [SerializeField] private float jumpHeight;
        [SerializeField] private AnimationCurve jumpspeed;




        private void Start()
        {
            canJump = true;
        }


        private void Update()
        {
            #region input
            inputAxis = new Vector2(Input.GetAxis("Horizontal"),
                                    Input.GetAxis("Vertical"));

            if (canJump)
            {
                if (Input.GetButtonDown("Jump")) Jump(inputAxis.x);
            }
            #endregion
        }



        private void FixedUpdate()
        {
            if (canMove) {
                if (inputAxis.x != 0 || inputAxis.y != 0) {
                    transform.position += new Vector3(inputAxis.x * xMovementSpeed,
                                                      inputAxis.y * yMovementSpeed,
                                                      0) * Time.deltaTime;
                }
            }
        }


        private void Jump(float direction)
        {
            canMove = false;
            canJump = false;

            float startHeight = transform.position.y;
            if (direction < 0) direction = -1;
            if (direction > 0) direction =  1;

            canJump = false;
            int limit = 1000;
            int i = 0;

            while (transform.position.y < startHeight + jumpHeight && i < limit) {
                transform.position += new Vector3(direction,
                                                  //jumpspeed.Evaluate(transform.position.y - startHeight / jumpHeight * 50),
                                                  1f,
                                                  0) * Time.deltaTime;

                Debug.Log(transform.position.y - startHeight + "/" + jumpHeight);
                i++;
            }

            i = 0;

            while (transform.position.y > startHeight && i < limit)
            {
                transform.position -= new Vector3(direction,
                                                  //jumpspeed.Evaluate(transform.position.y - startHeight / jumpHeight * 50),
                                                  1f,
                                                  0) * Time.deltaTime;

                Debug.Log(transform.position.y - startHeight + "/" + jumpHeight);
                i++;
            }

            canJump = true;
            canMove = true;
        }
    }
}

