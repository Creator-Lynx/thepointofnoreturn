using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MoveController : MonoBehaviour
{

    //basics
    Rigidbody _rigidbody;
    InputAction moveAction, jumpAction;

    //variable
    [SerializeField]
    float movementSpeed = 5f;

    //temporary testing
    [SerializeField]
    Text testText;


    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        moveAction = InputSystem.actions.FindAction("Player/Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
    }

    void Update()
    {
        Vector2 moveVector = moveAction.ReadValue<Vector2>();
        testText.text = moveVector.ToString() + "  " + Keyboard.current.wKey.wasPressedThisFrame.ToString();

        _rigidbody.AddForce(moveVector * movementSpeed * Time.deltaTime);
    }
}
