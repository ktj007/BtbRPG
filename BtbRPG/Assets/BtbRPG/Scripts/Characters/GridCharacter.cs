using System.Collections.Generic;
using UnityEngine;

using btbrpg.grid;
using btbrpg.holders;


namespace btbrpg.characters
{
	public class GridCharacter : MonoBehaviour, ISelectable
	{
        public PlayerHolder owner;
        public Character character;

        public GameObject highlighter;
        private bool isSelected;

        [Header("Action Points")]
        public int actionPoints;

        [Header("Character Speed Settings")]
        public float normalSpeed = 1f;
        public float crouchSpeed = 0.8f;
        public float runSpeed = 2.5f;
        public float rotateSpeed = 5;

        [Header("Character Stances")]
        public bool isProne;
        public bool isCrouched;
        public bool isRunning;

        public bool isStateCurrentlyMoving;
        

        public float Speed
        {
            get
            {
                float r = normalSpeed;
                if (isCrouched)
                {
                    r = crouchSpeed;
                }

                if (isRunning)
                {
                    r = runSpeed;
                }
                return r;
            }

        }

        [HideInInspector] public Node currentNode;
        [HideInInspector] public List<Node> currentPath;

        private Animator animator;

        public void Init()
        {
            owner.RegisterCharacter(this);
            highlighter.SetActive(false);

            animator = GetComponentInChildren<Animator>();
            animator.applyRootMotion = false;
        }

        public void OnStartTurn()
        {
            actionPoints = character.StartingAP;
        }


        public void SetCurrentPath(List<Node> path)
        {
            currentPath = path;
        }

        #region Stance Handling
        public void SetCrouch()
        {
            if (isCrouched)
            {
                // do nothing, already crouched
            }
            else
            {
                ResetStance();
                isCrouched = true;

                if(isStateCurrentlyMoving)
                {
                    PlayMovementAnimation();
                }
                else
                {
                    PlayIdleAnimation();
                }                
            }
        }

        public void SetProne()
        {
            if (isProne)
            {
                // do nothing, already prone
            }
            else
            {
                ResetStance();
                isProne = true;

                if (isStateCurrentlyMoving)
                {
                    PlayMovementAnimation();
                }
                else
                {
                    PlayIdleAnimation();
                }
            }

        }

        public void SetRun()
        {
            if (isRunning)
            {
                // do nothing, already running
            }
            else
            {
                ResetStance();
                isRunning = true;

                if (isStateCurrentlyMoving)
                {
                    PlayMovementAnimation();
                }
                else
                {
                    PlayIdleAnimation();
                }
            }
        }

        public void SetNormal()
        {
            ResetStance();

            if (isStateCurrentlyMoving)
            {
                PlayMovementAnimation();
            }
            else
            {
                PlayIdleAnimation();
            }
        }

        public void ResetStance()
        {
            isRunning = false;
            isProne = false;
            isCrouched = false;
        }
        #endregion

        #region Animations
        public void PlayMovementAnimation()
        {
            if (isCrouched)
            {
                PlayAnimation("Movement Crouch");
            }
            else if (isRunning)
            {

                PlayAnimation("Movement Run");
            }
            else
            {
                PlayAnimation("Movement Walk");

            }
        }

        public void PlayIdleAnimation()
        {
            if (isCrouched)
            {
                PlayAnimation("Idle Crouch");
            }
            else
            {
                PlayAnimation("Idle");

            }
        }

        public void PlayAnimation(string targetAnim)
        {
            animator.CrossFade(targetAnim, 0.25f);
        }
        #endregion

        #region Interfaces
        public void OnSelect(PlayerHolder player)
        {
            highlighter.SetActive(true);
            isSelected = true;
            player.stateManager.CurrentCharacter = this;
        }

        public void OnDeselect(PlayerHolder player)
        {
            isSelected = false;
            highlighter.SetActive(false);

        }

        public void OnHighlight(PlayerHolder player)
        {
            highlighter.SetActive(true);
        }

        public void OnDeHighlight(PlayerHolder player)
        {
            if (!isSelected)
            {
                highlighter.SetActive(false);
            }
        }

        public Node OnDetect()
        {
            return currentNode;
        }
        #endregion
    }


    public enum CharacterStance
    {
        PRONE, CROUCHED, NORMAL
    }


}
