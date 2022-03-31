using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JH
{
    public class PlayerLocomotion : MonoBehaviour
    {
        Transform cameraObject;
        InputHandler inputHandler;
        Vector3 moveDirection;

        [HideInInspector]
        public Transform myTransform;
        [HideInInspector]
        public AnimatorHandler animatorHandler;

        public new Rigidbody rigidbody;
        public GameObject normalCamera;

        [Header("Stats")]
        [SerializeField]
        float movementSpeed = 5;
        [SerializeField]
        float sprintSpeed = 8;
        [SerializeField]
        float rotationSpeed = 10;

        public bool isSprinting;


        void Start()
        {
            rigidbody = GetComponent<Rigidbody>();
            inputHandler = GetComponent<InputHandler>();
            animatorHandler = GetComponentInChildren<AnimatorHandler>();
            cameraObject = Camera.main.transform;
            myTransform = transform;
            animatorHandler.Initialize();
        }

        public void Update(){
            float delta = Time.deltaTime;

            isSprinting = inputHandler.b_Input;

            inputHandler.TickInput(delta);

            HandleMovement(delta);

            HandleRollingAndSprinting(delta);
        }

        #region Movement
        Vector3 normalVector;
        Vector3 targetPosition;

        public void HandleMovement(float delta)
        {
            if(inputHandler.rollFlag){
                return;
            }

            moveDirection = cameraObject.forward * inputHandler.vertical;
            moveDirection += cameraObject.right * inputHandler.horizontal;
            moveDirection.Normalize();
            moveDirection.y = 0;

            float speed;

            if((inputHandler.horizontal < 0.55f && inputHandler.moveAmount < 0.6f) 
                || (inputHandler.vertical < 0.55f && inputHandler.moveAmount < 0.6f) ){
                speed = movementSpeed/2;
            }
            else{
                speed = movementSpeed;
            }

            if(inputHandler.sprintFlag){
                speed = sprintSpeed;
                isSprinting = true;
            }

            moveDirection *= speed;

            Vector3 projectedVelocity = Vector3.ProjectOnPlane(moveDirection, normalVector);
            rigidbody.velocity = projectedVelocity;

            animatorHandler.UpdateAnimatorValues(inputHandler.moveAmount, 0, isSprinting);

            if(animatorHandler.canRotate == true){
                HandleRotation(delta);
            }
        }

        private void HandleRotation(float delta){
            Vector3 targetDir = Vector3.zero;
            float moveOverride = inputHandler.moveAmount;

            targetDir = cameraObject.forward * inputHandler.vertical;
            targetDir += cameraObject.right * inputHandler.horizontal;

            targetDir.Normalize();
            targetDir.y = 0;

            if(targetDir == Vector3.zero){
                targetDir = myTransform.forward;
            }

            float rs = rotationSpeed;

            Quaternion tr = Quaternion.LookRotation(targetDir);
            Quaternion targetRotation = Quaternion.Slerp(myTransform.rotation, tr, rs * delta);

            myTransform.rotation = targetRotation;
        }

        public void HandleRollingAndSprinting(float delta)
        {
            if(animatorHandler.anim.GetBool("isInteracting")){
                return;
            }

            if(inputHandler.rollFlag){
                moveDirection = cameraObject.forward *inputHandler.vertical;
                moveDirection += cameraObject.right * inputHandler.horizontal;

                if(inputHandler.moveAmount > 0){
                    animatorHandler.PlayTargetAnimation("Dodge Roll", true);
                    moveDirection.y = 0;
                    Quaternion rollRotation = Quaternion.LookRotation(moveDirection);
                    myTransform.rotation = rollRotation;
                }
                else{
                    animatorHandler.PlayTargetAnimation("Dodge Backstep", true);
                }
            }
        }

        #endregion

        
    }
}
