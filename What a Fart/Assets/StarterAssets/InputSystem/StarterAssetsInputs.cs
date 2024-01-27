using UnityEngine;
using UnityEngine.Events;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look; 
		public bool sprint;
		public bool fartTest;

		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;
		
		[HideInInspector]
		public UnityEvent  openMenu;

		public UnityEvent  fart;

#if ENABLE_INPUT_SYSTEM
		public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			if(cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
			}
		}

		public void OnSprint(InputValue value)
		{
			SprintInput(value.isPressed);
		}
		
		public void OnFart(InputValue value)
		{
			FartInput(value.isPressed);
		}

		public void OnOpenMenu(InputValue value)
		{
			OpenMenuCallback(value.isPressed);
			Debug.Log("Open Menu");
		}

		public void OnFartPressed(InputValue value)
		{
			FartCallback(value.isPressed);
			Debug.Log("Change state");
		}
#endif


		public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		} 

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}

		public void FartInput(bool newFartState)
		{
			fartTest = newFartState;
		}
		public void OpenMenuCallback(bool newInteractState)
		{
			//interactOnRelease
			if (!newInteractState)
			{
				openMenu.Invoke();
			}
		}

		public void FartCallback(bool newFartState)
		{
			if (!newFartState)
			{
				fart.Invoke();
			}
		}

		private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}
	}
	
}