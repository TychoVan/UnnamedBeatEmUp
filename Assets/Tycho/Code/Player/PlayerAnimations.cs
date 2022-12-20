using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Player{

    public enum AnimationState { walking, jumping, falling }
    [RequireComponent(typeof(PlayerMovement))]
    [RequireComponent(typeof(SpriteRenderer))]

    public class PlayerAnimations : MonoBehaviour
    {
        public AnimationState PlayerState;

        private PlayerMovement movementScript;
        private SpriteRenderer playerSprite;

        private void Awake()
        {
            playerSprite   = this.gameObject.GetComponent<SpriteRenderer>();
            movementScript = this.gameObject.GetComponent<PlayerMovement>();
        }

        public bool Mirrored() {
            if (movementScript.InputAxis.x < 0) return true;
            else                                return false;
        }


        private void Update()
        {
            if (movementScript.InputAxis.x != 0) {
                transform.localScale = new Vector3((Mirrored() ? -1 : 1),
                                                   transform.localScale.y,
                                                   transform.localScale.z);
            }
            
        }
    }
}
