using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JH
{
    public class PlayerManager : MonoBehaviour
    {
        InputHandler inputHandler;
        Animator anim;
        CameraHandler cameraHandler;
        PlayerLocomotion playerLocomotion;

        [Header("Player Flags")]
        public bool isInteracting;
        public bool isSprinting;

        void Start()
        {
            cameraHandler = CameraHandler.singleton;
            inputHandler = GetComponent<InputHandler>();
            anim = GetComponentInChildren<Animator>();
            playerLocomotion = GetComponent<PlayerLocomotion>();
        }

        void Update()
        {
            float delta = Time.deltaTime;

            isInteracting = anim.GetBool("isInteracting");

            inputHandler.TickInput(delta);

            playerLocomotion.HandleMovement(delta);

            playerLocomotion.HandleRollingAndSprinting(delta);
        }

        private void FixedUpdate()
        {
            float delta = Time.fixedDeltaTime;

            isSprinting = inputHandler.b_Input;

            if(cameraHandler != null){
                cameraHandler.FollowTarget(delta);
                cameraHandler.HandleCameraRotation(delta, inputHandler.mouseX, inputHandler.mouseY);
            }
        }

        private void LateUpdate() 
        {
            inputHandler.rollFlag = false;
            inputHandler.sprintFlag = false;

        }
    }
}
