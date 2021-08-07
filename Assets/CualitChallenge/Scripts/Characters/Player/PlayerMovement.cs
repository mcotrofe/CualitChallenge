using System;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

namespace CualitChallenge.Player
{
    [RequireComponent(typeof(CharacterController), typeof(Animator))]
    public class PlayerMovement : MonoBehaviour
    {
        static readonly string ForwardParameter = "Forward";
        static readonly string RotateParameter = "Rotate";
        static readonly string StrafeParameter = "Strafe";
        static readonly string AimParameter = "Aim";
        static readonly string RunParameter = "Run";

        static readonly string HorizontalAxisInput = "Horizontal";
        static readonly string VerticalAxisInput = "Vertical";
        static readonly string RunInput = "Run";
        static readonly string AimInput = "Aim";

        static readonly Vector3 HorizontalPlane = new Vector3(1, 0, 1);


        [SerializeField] bool canAim = true;
        [SerializeField] float animationParamSmoothing = 10;
        [SerializeField] float inputSmoothing = 10;
        [SerializeField] float rotationSmoothing = 10;

        private CharacterController controller;
        private Animator animator;
        private Transform cameraTransform;

        private bool run { get; set; }
        private bool aim { get; set; }
        private bool inCombat { get; set; }
        private Vector2 rawMoveInput { get; set; }
        private Vector2 smoothedMoveInput { get; set; }
        private Vector3 relativeToCameraMoveVector => cameraForward * smoothedMoveInput.y + cameraRight * smoothedMoveInput.x;
        private Vector3 relativeToPlayerMoveVector => transform.InverseTransformDirection(relativeToCameraMoveVector);
        private Vector3 cameraForward => Vector3.Scale(cameraTransform.forward, HorizontalPlane).normalized;
        private Vector3 cameraRight => Vector3.Scale(cameraTransform.right, HorizontalPlane).normalized;


        public void SetInCombat(bool inCombat) => this.inCombat = inCombat;

        void Awake()
        {
            controller = GetComponent<CharacterController>();
            animator = GetComponent<Animator>();
            cameraTransform = Camera.main.transform;
        }

        void Update()
        {
            UpdateInputs();
            UpdateMovement();
            ApplyGravity();
        }


        void UpdateInputs()
        {
            rawMoveInput = new Vector2(Input.GetAxis(HorizontalAxisInput), Input.GetAxis(VerticalAxisInput));
            smoothedMoveInput = Vector2.Lerp(smoothedMoveInput, rawMoveInput, Time.deltaTime * inputSmoothing);
            run = Input.GetButton(RunInput);
            aim = canAim && Input.GetButton(AimInput);
        }

        void UpdateMovement()
        {
            if (IsFixedMovement()) UpdateFixedMovement();
            else UpdateFreeMovement();
        }

        bool IsFixedMovement() => aim || inCombat && !run;

        void UpdateFixedMovement()
        {
            SetForwardToCameraForward();
            Vector3 inputVector = relativeToPlayerMoveVector;
            SetMoveAnimationParameters(
                forward: IsRunning()? Mathf.Clamp01(inputVector.z):inputVector.z, 
                rotate: 0, 
                strafe: inputVector.x,
                run: IsRunning(),
                aim : !IsRunning());
        }

        void SetForwardToCameraForward() => transform.forward = Vector3.Lerp(transform.forward, cameraForward, Time.deltaTime * rotationSmoothing);

        void UpdateFreeMovement()
        {
            if (IsRunning()) UpdateFreeRun();
            else UpdateFreeWalk();
        }

        bool IsRunning() => run;

        void UpdateFreeRun()
        {
            Vector3 inputVector = relativeToPlayerMoveVector;
            SetMoveAnimationParameters(
                forward: inputVector.z, 
                rotate: inputVector.x, 
                strafe: 0,
                IsRunning(),
                IsFixedMovement());
        }

        void UpdateFreeWalk()
        {
            Vector3 inputVector = relativeToPlayerMoveVector;
            SetMoveAnimationParameters(
                forward: Mathf.Max(Mathf.Clamp01(inputVector.z), Mathf.Abs(inputVector.x * .5f)),
                rotate: inputVector.z >= -.1f ? inputVector.x * 2 : Mathf.Sign(inputVector.x),
                strafe: 0,
                IsRunning(),
                IsFixedMovement());
        }

        void ApplyGravity() => controller.SimpleMove(Vector3.zero);

        void SetMoveAnimationParameters(float forward, float rotate, float strafe, bool run, bool aim)
        {
            SetAnimationParameter(ForwardParameter, forward);
            SetAnimationParameter(RotateParameter, rotate);
            SetAnimationParameter(StrafeParameter, strafe);
            SetAnimationParameter(RunParameter, run);
            SetAnimationParameter(AimParameter, aim);
        }

        void SetAnimationParameter(string name, float value) =>
            animator.SetFloat(name, Mathf.Lerp(animator.GetFloat(name), value, Time.deltaTime * animationParamSmoothing));

        void SetAnimationParameter(string name, bool value) =>
            animator.SetBool(name, value);


    }
}
