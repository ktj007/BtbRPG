using System.Collections.Generic;
using UnityEngine;

using btbrpg.grid;
using btbrpg.holders;


namespace btbrpg.characters
{
	public class GridCharacter : MonoBehaviour, ISelectable
	{
        public PlayerHolder owner;

        public GameObject highlighter;
        private bool isSelected;

        public float walkSpeed = 1f;
        public float crouchSpeed = 0.8f;
        public float runSpeed = 2.5f;
        public float rotateSpeed = 5;

        public bool isCrouched;
        public bool isRunning;
        public bool isProne;

        public float Speed
        {
            get
            {
                float r = walkSpeed;
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

        public void SetCurrentPath(List<Node> path)
        {
            currentPath = path;
        }

        #region Stance Handling
        public void SetCrouch()
        {
            ResetStance();
            isCrouched = true;
        }

        public void SetProne()
        {
            ResetStance();
            isProne = true;
        }

        public void SetRun()
        {
            ResetStance();
            isRunning = true;
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
            animator.CrossFade(targetAnim, 0.05f);
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
}
