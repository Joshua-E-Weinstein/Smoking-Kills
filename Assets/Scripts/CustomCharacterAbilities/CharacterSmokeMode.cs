using UnityEngine;
using System.Collections;
using MoreMountains.Tools;
using TemporaryGameCompany;

namespace MoreMountains.TopDownEngine // you might want to use your own namespace here
{
    /// <summary>
    /// TODO_DESCRIPTION
    /// </summary>
    [AddComponentMenu("TopDown Engine/Character/Abilities/Character Smoke Mode")]
    public class CharacterSmokeMode : CharacterAbility
    {
        /// This method is only used to display a helpbox text at the beginning of the ability's inspector
		public override string HelpBoxText() { return "This component allows your character to enter smoke mode when pressing the smoke mode button."; }

		[Header("Speed")]

		/// the speed of the character when it's moving as smoke
		[Tooltip("the speed of the character when it's moving as smoke")]
		public float SmokeModeSpeed = 16f;

		protected const string _smokeModeAnimationParameterName = "Smoke Mode";
		protected int _smokeModeAnimationParameter;
		protected bool _smokeModeStarted = false;

        [Header("GameEvents")]

        [SerializeField] private GameEvent SmokeActivate;
        [SerializeField] private GameEvent SmokeDeactivate;

		/// <summary>
		/// At the beginning of each cycle, we check if we've pressed or released the smoke mode button
		/// </summary>
		protected override void HandleInput()
		{
			if ((_inputManager.SmokeModeButton.State.CurrentState == MMInput.ButtonStates.ButtonDown || _inputManager.SmokeModeButton.State.CurrentState == MMInput.ButtonStates.ButtonPressed) && PlayerSmoke.smoke > 0)
			{
				SmokeModeStart();
			}				
			if (_inputManager.SmokeModeButton.State.CurrentState == MMInput.ButtonStates.ButtonUp || PlayerSmoke.smoke <= 0)
			{
				SmokeModeStop();
			}
		}

		/// <summary>
		/// Every frame we make sure we shouldn't be exiting our smoke mode state
		/// </summary>
		public override void ProcessAbility()
		{
			base.ProcessAbility();
			HandleSmokeModeExit();
		}

		/// <summary>
		/// Checks if we should exit our smoke mode state
		/// </summary>
		protected virtual void HandleSmokeModeExit()
		{
			if (_condition.CurrentState != CharacterStates.CharacterConditions.Normal)
			{
				StopAbilityUsedSfx();
			}
			if (_condition.CurrentState == CharacterStates.CharacterConditions.SmokeMode && AbilityInProgressSfx != null && _abilityInProgressSfx == null)
			{
				PlayAbilityUsedSfx();
			}
			// if we're in smoke mode and not grounded, we change our state to Falling
			if (!_controller.Grounded
			    && (_condition.CurrentState == CharacterStates.CharacterConditions.Normal)
			    && (_condition.CurrentState == CharacterStates.CharacterConditions.SmokeMode))
			{
				_movement.ChangeState(CharacterStates.MovementStates.Falling);
				StopFeedbacks();
				StopSfx ();
			}
			// if we're not moving fast enough, we go back to idle
			/*if ((Mathf.Abs(_controller.CurrentMovement.magnitude) < SmokeModeSpeed / 10) && (_movement.CurrentState == CharacterStates.MovementStates.SmokeMode))
			{
				_movement.ChangeState (CharacterStates.MovementStates.Idle);
				StopFeedbacks();
				StopSfx ();
			}*/
			if (!_controller.Grounded && _abilityInProgressSfx != null)
			{
				StopFeedbacks();
				StopSfx ();
			}
		}

		/// <summary>
		/// Causes the character to enter smoke mode.
		/// </summary>
		public virtual void SmokeModeStart()
		{		
			if ( !AbilityAuthorized // if the ability is not permitted
			     || (_condition.CurrentState != CharacterStates.CharacterConditions.Normal)) // or if we're not in normal conditions
			{
				// we do nothing and exit
				return;
			}

			// if the player presses the smoke mode button and if we're on the ground and not crouching and we can move freely, 
			// then we change the movement speed in the controller's parameters.
			if (_characterMovement != null)
			{
				_characterMovement.MovementSpeed = SmokeModeSpeed;
			}

			// if we're not already in smoke mode, we trigger our sounds
			if (_condition.CurrentState != CharacterStates.CharacterConditions.SmokeMode)
			{
				PlayAbilityStartSfx();
				PlayAbilityUsedSfx();
				PlayAbilityStartFeedbacks();
				SmokeActivate.Raise();
				_smokeModeStarted = true;
			}

			_condition.ChangeState(CharacterStates.CharacterConditions.SmokeMode);
		}

		/// <summary>
		/// Causes the character to exit smoke mode.
		/// </summary>
		public virtual void SmokeModeStop()
		{   
			if (_smokeModeStarted)
			{
				// if the smoke mode button is released, we revert back to the walking speed.
				if ((_characterMovement != null))
				{
					_characterMovement.ResetSpeed();
					_condition.ChangeState(CharacterStates.CharacterConditions.Normal);
				}
				StopFeedbacks();
				StopSfx();
				SmokeDeactivate.Raise();
				_smokeModeStarted = false;
			}            
		}

		/// <summary>
		/// Stops all smoke mode feedbacks
		/// </summary>
		protected virtual void StopFeedbacks()
		{
			if (_startFeedbackIsPlaying)
			{
				StopStartFeedbacks();
				PlayAbilityStopFeedbacks();
			}
		}

		/// <summary>
		/// Stops all smoke mode sounds
		/// </summary>
		protected virtual void StopSfx()
		{
			StopAbilityUsedSfx();
			PlayAbilityStopSfx();
		}

		/// <summary>
		/// Adds required animator parameters to the animator parameters list if they exist
		/// </summary>
		protected override void InitializeAnimatorParameters()
		{
			RegisterAnimatorParameter (_smokeModeAnimationParameterName, AnimatorControllerParameterType.Bool, out _smokeModeAnimationParameter);
		}

		/// <summary>
		/// At the end of each cycle, we send our SmokeMode status to the character's animator
		/// </summary>
		public override void UpdateAnimator()
		{
			MMAnimatorExtensions.UpdateAnimatorBool(_animator, _smokeModeAnimationParameter, (_condition.CurrentState == CharacterStates.CharacterConditions.SmokeMode),_character._animatorParameters, _character.RunAnimatorSanityChecks);
		}
    }
}