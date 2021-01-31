using UnityEngine;

namespace Invector.vCharacterController
{
    public class vThirdPersonController : vThirdPersonAnimator
    {

        private bool _IsPlayerWithinRange = false;
        private GameObject ActiveNPC;

       
        public FMOD.Studio.EventInstance Radio1Event;
        public FMOD.Studio.EventInstance Radio2Event;
        public FMOD.Studio.EventInstance Radio3Event;

        void Start()
        {
            Radio1Event = FMODUnity.RuntimeManager.CreateInstance(SoundManager.mainAudio.Radio1);    
        }

        public virtual void ControlAnimatorRootMotion()
        {
            if (!this.enabled) return;

            if (inputSmooth == Vector3.zero)
            {
                transform.position = animator.rootPosition;
                transform.rotation = animator.rootRotation;
            }

            if (useRootMotion)
                MoveCharacter(moveDirection);
        }

        public virtual void ControlLocomotionType()
        {
            if (lockMovement) return;

            if (locomotionType.Equals(LocomotionType.FreeWithStrafe) && !isStrafing || locomotionType.Equals(LocomotionType.OnlyFree))
            {
                SetControllerMoveSpeed(freeSpeed);
                SetAnimatorMoveSpeed(freeSpeed);
            }
            else if (locomotionType.Equals(LocomotionType.OnlyStrafe) || locomotionType.Equals(LocomotionType.FreeWithStrafe) && isStrafing)
            {
                isStrafing = true;
                SetControllerMoveSpeed(strafeSpeed);
                SetAnimatorMoveSpeed(strafeSpeed);
            }

            if (!useRootMotion)
                MoveCharacter(moveDirection);
        }

        public virtual void ControlRotationType()
        {
            if (lockRotation) return;

            bool validInput = input != Vector3.zero || (isStrafing ? strafeSpeed.rotateWithCamera : freeSpeed.rotateWithCamera);

            if (validInput)
            {
                // calculate input smooth
                inputSmooth = Vector3.Lerp(inputSmooth, input, (isStrafing ? strafeSpeed.movementSmooth : freeSpeed.movementSmooth) * Time.deltaTime);

                Vector3 dir = (isStrafing && (!isSprinting || sprintOnlyFree == false) || (freeSpeed.rotateWithCamera && input == Vector3.zero)) && rotateTarget ? rotateTarget.forward : moveDirection;
                RotateToDirection(dir);
            }
        }

        public virtual void UpdateMoveDirection(Transform referenceTransform = null)
        {
            if (input.magnitude <= 0.01)
            {
                moveDirection = Vector3.Lerp(moveDirection, Vector3.zero, (isStrafing ? strafeSpeed.movementSmooth : freeSpeed.movementSmooth) * Time.deltaTime);
                return;
            }

            if (referenceTransform && !rotateByWorld)
            {
                //get the right-facing direction of the referenceTransform
                var right = referenceTransform.right;
                right.y = 0;
                //get the forward direction relative to referenceTransform Right
                var forward = Quaternion.AngleAxis(-90, Vector3.up) * right;
                // determine the direction the player will face based on input and the referenceTransform's right and forward directions
                moveDirection = (inputSmooth.x * right) + (inputSmooth.z * forward);
            }
            else
            {
                moveDirection = new Vector3(inputSmooth.x, 0, inputSmooth.z);
            }
        }

        public virtual void Sprint(bool value)
        {
            var sprintConditions = (input.sqrMagnitude > 0.1f && isGrounded &&
                !(isStrafing && !strafeSpeed.walkByDefault && (horizontalSpeed >= 0.5 || horizontalSpeed <= -0.5 || verticalSpeed <= 0.1f)));

            if (value && sprintConditions)
            {
                if (input.sqrMagnitude > 0.1f)
                {
                    if (isGrounded && useContinuousSprint)
                    {
                        isSprinting = !isSprinting;
                    }
                    else if (!isSprinting)
                    {
                        isSprinting = true;
                    }
                }
                else if (!useContinuousSprint && isSprinting)
                {
                    isSprinting = false;
                }
            }
            else if (isSprinting)
            {
                isSprinting = false;
            }
        }

        public virtual void Strafe()
        {
            isStrafing = !isStrafing;
        }

        public virtual void Jump()
        {
            // trigger jump behaviour
            jumpCounter = jumpTimer;
            isJumping = true;

            // trigger jump animations
            if (input.sqrMagnitude < 0.1f)
                animator.CrossFadeInFixedTime("Jump", 0.1f);
            else
                animator.CrossFadeInFixedTime("JumpMove", .2f);
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.tag == "NPC")
            {
                // TODO add sanity check so if this.ActiveNPC is set, nothing happens.
                _IsPlayerWithinRange = true;
                Debug.Log("Collision Detected");
                /* Component[] components = other.gameObject.GetComponents(typeof(Component));
                FollowPlayer FP1 = other.gameObject.GetComponent<FollowPlayer>(); */

                Debug.Log("FollowPlayer Retrieved");
                // Set the follow behavior
                other.gameObject.GetComponent<FollowPlayer>().target = this.transform;

                this.ActiveNPC = other.gameObject;
            }

            if (other.tag == "Fireplace")
            {
                // Clear out our companion's target
                this.ActiveNPC.GetComponent<FollowPlayer>().target = null;
                Debug.Log("NPC is safe.");

                // Lerp the target to the Fireplace
                this.ActiveNPC.transform.position = Vector3.MoveTowards(this.ActiveNPC.transform.position, other.transform.position, 2f * Time.deltaTime);

                // Turn off their collide so nothing else can happen to them.
                this.ActiveNPC.GetComponent<BoxCollider>().enabled = false;
                this.ActiveNPC = null;
            }
        }

        // Fired off when something exits the trigger space of the attached GameObject
        void OnTriggerExit(Collider other)
        {
            if (other.tag == "NPC")
            {
                _IsPlayerWithinRange = false;

            }
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                FMODUnity.RuntimeManager.PlayOneShot("event:/Dialog/DockGirlDia");
                
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                FMODUnity.RuntimeManager.PlayOneShot("event:/Dialog/DogDia");

            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                FMODUnity.RuntimeManager.PlayOneShot("event:/Dialog/WoodsGuyDia");
            }


            if (Input.GetKey("escape"))
            {
                Application.Quit();
            }
        }

        
    }
}

