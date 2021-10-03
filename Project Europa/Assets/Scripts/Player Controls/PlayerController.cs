using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [FoldoutGroup("Components"), SerializeField] private Rigidbody rigidbody;
    [FoldoutGroup("Attributes"), SerializeField, Range(0f, 100f)] private float acceleration = 5f;
    [FoldoutGroup("Attributes"), SerializeField, Range(0f, 100f)] private float decelearation = 3f;
    [FoldoutGroup("Attributes"), SerializeField, Range(0f, 100f)] private float brakeForce = 2f;
    [FoldoutGroup("Attributes"), SerializeField, Range(0f, 100f)] private float maxForwardSpeed = 10f;
    [FoldoutGroup("Attributes"), SerializeField, Range(0f, 100f)] private float maxBackwardSpeed = 6f;
    [FoldoutGroup("Attributes"), SerializeField, Range(0f, 100f)] private float rotationSpeed = 5f;

    private PlayerInput playerInput;
    private Vector2 moveInput;

    private void Awake()
    {
        playerInput = InputManager.P_Input;
        rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        playerInput.PlayerActions.Enable();
    }

    void Update()
    {
        moveInput = GetMoveInput();
        Debug.Log(rigidbody.velocity);
    }

    private void FixedUpdate()
    {
        transform.Rotate(transform.up, moveInput.x * rotationSpeed * Time.fixedDeltaTime);
        
        if(moveInput.y > 0)
        {
            Vector3 maxforwardVelocity = transform.forward * maxForwardSpeed;
            if (rigidbody.velocity.magnitude < maxForwardSpeed)
            {
                rigidbody.AddForce(transform.forward * moveInput.y * acceleration, ForceMode.Acceleration);
                rigidbody.velocity = ClampVelocity(rigidbody.velocity, transform.forward, maxForwardSpeed);
            }
                
        }
        else if (moveInput.y < 0)
        {
            if (rigidbody.velocity.magnitude < maxBackwardSpeed)
            {
                rigidbody.AddForce(transform.forward * moveInput.y * decelearation, ForceMode.Acceleration);
                rigidbody.velocity = ClampVelocity(rigidbody.velocity, -transform.forward, maxBackwardSpeed);
            }   
        }
        else
        {
            if(rigidbody.velocity.magnitude > Mathf.Epsilon)
                rigidbody.AddForce(-rigidbody.velocity.normalized * brakeForce, ForceMode.Acceleration);
        }
    }

    private void OnDisable()
    {
        playerInput.PlayerActions.Disable();
    }

    private Vector2 GetMoveInput()
    {
        return playerInput.PlayerActions.Movement.ReadValue<Vector2>();
    }

    private Vector3 ClampVelocity(Vector3 _currentVelocity, Vector3 _moveDirection, float _maxSpeed)
    {
        Vector3 maxVelocity = _moveDirection * _maxSpeed;
        float xClamp = Mathf.Clamp(_currentVelocity.x, Mathf.Min(_currentVelocity.x, maxVelocity.x), Mathf.Max(_currentVelocity.x, maxVelocity.x));
        float yClamp = Mathf.Clamp(_currentVelocity.y, Mathf.Min(_currentVelocity.y, maxVelocity.y), Mathf.Max(_currentVelocity.y, maxVelocity.y));
        float zClamp = Mathf.Clamp(_currentVelocity.z, Mathf.Min(_currentVelocity.z, maxVelocity.z), Mathf.Max(_currentVelocity.z, maxVelocity.z));
        return new Vector3(xClamp, yClamp, zClamp);
    }
}
