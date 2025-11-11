using UnityEngine;
using UnityEngine.InputSystem;

public class MoveControllerCharacter : MonoBehaviour
{
    //redisign
    [SerializeField] InputActionAsset InputActions;
    Vector2 moveVector2Input;
    Vector2 lookVector2Input;


    //basics
    CharacterController _characterController;
    [SerializeField] Transform _cameraTransform;
    InputAction moveAction;
    InputAction jumpAction;
    InputAction lookAction;

    //variable
    [SerializeField] float movementSpeed = 5f;
    [SerializeField] float lookSpeed = 5f;
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
        moveVector2Input = moveAction.ReadValue<Vector2>();
        lookVector2Input = lookAction.ReadValue<Vector2>();

        //if (jumpAction.WasPressedThisFrame()) Jump();

        Looking();
        Moving();
    }

    float _yAxisVelocity;
    void Moving()
    {
        //horizontal moving calculated
        Vector3 movementVector =
            transform.forward * moveVector2Input.y * movementSpeed * Time.deltaTime +
            transform.right * moveVector2Input.x * movementSpeed * Time.deltaTime;



        //jump applied
        if (jumpAction.WasPressedThisFrame() && _characterController.isGrounded)
            _yAxisVelocity = Mathf.Sqrt(jumpHeight * -2f * gravityValue);
        //gravity applied
        if (!_characterController.isGrounded)
            _yAxisVelocity += gravityValue * Time.deltaTime;
        movementVector.y = _yAxisVelocity * Time.deltaTime;
        //apply all movement
        _characterController.Move(movementVector);
    }


    float _cameraCurrentRotationX = 0f;
    void Looking()
    {
        //player Y rotating
        float deltaRotationPlayerY = lookVector2Input.x * lookSpeed * Time.smoothDeltaTime;
        transform.Rotate(deltaRotationPlayerY * Vector3.up);

        //camera X rotating. get the delta
        float deltaRotationCameraX = lookVector2Input.y * lookSpeed * Time.smoothDeltaTime;
        //camera vertical clamping
        _cameraCurrentRotationX -= deltaRotationCameraX;
        _cameraCurrentRotationX = Mathf.Clamp(_cameraCurrentRotationX, -90f, 90f);
        //apply rotation
        _cameraTransform.localRotation = Quaternion.Euler(_cameraCurrentRotationX, 0f, 0f);
        
    }
}
