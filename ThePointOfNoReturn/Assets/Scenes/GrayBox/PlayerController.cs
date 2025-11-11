using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //redisign
    [SerializeField] InputActionAsset InputActions;

    //basics
    CharacterController _characterController;
    [SerializeField] Transform _cameraTransform;
    InputAction moveAction;
    InputAction jumpAction;
    InputAction lookAction;

    //variables
    [Header("Look settings")]
    [SerializeField] float lookSpeed = 5f;
    [SerializeField] bool enableMouseSmoothing = true;
    [SerializeField] float lookSmoothing = 0.1f;
    [Header("Movement settings")]
    [SerializeField] float movementSpeed = 5f;
    [SerializeField] float movementSmooth = 0.1f;
    [SerializeField] float airControl = 0.2f;
    [SerializeField] float jumpHeight = 2f;
    [SerializeField] float gravityValue = -10f;

    void OnEnable()
    {
        InputActions.FindActionMap("Player").Enable();
    }
    void OnDisable()
    {
        InputActions.FindActionMap("Player").Disable();
    }


    void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
        lookAction = InputSystem.actions.FindAction("Look");

        //Application.targetFrameRate = 60;
    }

    void Update()
    {

       

        //if (jumpAction.WasPressedThisFrame()) Jump();

        Looking();
        Moving();
    }

    float _yAxisVelocity;
    Vector2 moveVector2Input;
    Vector2 originMovement4Jump = Vector2.up;
    Vector2 smoothVelocity4Movement;
    void Moving()
    {
        //reading input and applying airControl
        if (_characterController.isGrounded)
        {
            //moveVector2Input = moveAction.ReadValue<Vector2>();
            moveVector2Input = Vector2.SmoothDamp(
                moveVector2Input, moveAction.ReadValue<Vector2>(),
                ref smoothVelocity4Movement, movementSmooth);
        }
        else
        {
            if (moveAction.IsInProgress())
                moveVector2Input = Vector2.Lerp(
                    originMovement4Jump, moveAction.ReadValue<Vector2>(), airControl);
        }

        //horizontal moving calculated
        Vector3 movementVector =
            transform.forward * moveVector2Input.y * movementSpeed * Time.deltaTime +
            transform.right * moveVector2Input.x * movementSpeed * Time.deltaTime;


        if (_characterController.isGrounded) _yAxisVelocity = -0.5f;
        //jump applied
        //h = V^2 / 2g for falling ogject
        //V^2 = 2hg
        if (jumpAction.WasPressedThisFrame() && _characterController.isGrounded)
        {
            _yAxisVelocity = Mathf.Sqrt(jumpHeight * -2f * gravityValue);
            originMovement4Jump = moveVector2Input;
        }
            
        //gravity applied 
        //V += g*dt
        //dS = V*dt 
        if (!_characterController.isGrounded)
            _yAxisVelocity += gravityValue * Time.deltaTime;
        movementVector.y = _yAxisVelocity * Time.deltaTime;
        //apply all movement
        _characterController.Move(movementVector);
    }


    float _cameraCurrentRotationX = 0f;
    Vector2 lookVector2Input;
    Vector2 smoothVelocity4Look;
    Vector2 currentLookVector2;
    void Looking()
    {
        //getting input
        lookVector2Input = lookAction.ReadValue<Vector2>();

        //smoothing input vector if needed
        if (enableMouseSmoothing)
            currentLookVector2 =
                Vector2.SmoothDamp(
                    currentLookVector2, lookVector2Input, ref smoothVelocity4Look, lookSmoothing);
        else
            currentLookVector2 = lookVector2Input;

        //player Y rotating
        float deltaRotationPlayerY = currentLookVector2.x * lookSpeed;
        transform.Rotate(deltaRotationPlayerY * Vector3.up);

        //camera X rotating. get the delta
        float deltaRotationCameraX = currentLookVector2.y * lookSpeed;
        //camera vertical clamping
        _cameraCurrentRotationX -= deltaRotationCameraX;
        _cameraCurrentRotationX = Mathf.Clamp(_cameraCurrentRotationX, -90f, 90f);
        //apply rotation
        _cameraTransform.localRotation = Quaternion.Euler(_cameraCurrentRotationX, 0f, 0f);
    }



    [SerializeField] Text screenText;
}
