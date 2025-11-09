using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MoveController : MonoBehaviour
{
    //redisign
    [SerializeField] InputActionAsset InputActions;
    Vector2 moveVector;
    Vector2 lookVector;


    //basics
    Rigidbody _rigidbody;
    [SerializeField] Transform _cameraTransform;
    InputAction moveAction;
    InputAction jumpAction;
    InputAction lookAction;

    //variable
    [SerializeField] float movementSpeed = 5f;
    [SerializeField] float lookSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;

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
        _rigidbody = GetComponent<Rigidbody>();
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
        lookAction = InputSystem.actions.FindAction("Look");
    }

    void Update()
    {
        moveVector = moveAction.ReadValue<Vector2>();
        lookVector = lookAction.ReadValue<Vector2>();

        if (jumpAction.WasPressedThisFrame()) Jump();

        Looking();
    }

    void FixedUpdate()
    {
        Moving();
    }

    void Jump()
    {
        _rigidbody.AddForce(Vector3.up * jumpSpeed, ForceMode.VelocityChange);
    }

    void Moving()
    {
        _rigidbody.MovePosition(
            _rigidbody.position +
            transform.forward * moveVector.y * movementSpeed * Time.fixedDeltaTime + 
            transform.right * moveVector.x * movementSpeed * Time.fixedDeltaTime);
    }
    
    void Looking()
    {
        float rotationPlayerY = lookVector.x * lookSpeed * Time.deltaTime;
        Quaternion deltaRotation = Quaternion.Euler(0, rotationPlayerY, 0);
        //_rigidbody.MoveRotation(_rigidbody.rotation * deltaRotation);
        transform.Rotate(deltaRotation.eulerAngles);

        float rotationCameraX = lookVector.y * lookSpeed * Time.deltaTime;
        Quaternion deltaXRotation = Quaternion.Euler(-rotationCameraX, 0, 0);
        if (_cameraTransform.rotation.x < 90f && _cameraTransform.rotation.x > -90f)
            _cameraTransform.Rotate(deltaXRotation.eulerAngles);
    }
}
