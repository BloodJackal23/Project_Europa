using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [FoldoutGroup("Components"), SerializeField] private Rigidbody rigidbody;
    [FoldoutGroup("Components"), SerializeField] private ObjectMarker playerMarker;
    [FoldoutGroup("Attributes"), SerializeField, Range(0f, 100f)] private float acceleration = 5f;
    [FoldoutGroup("Attributes"), SerializeField, Range(0f, 100f)] private float decelearation = 3f;
    [FoldoutGroup("Attributes"), SerializeField, Range(0f, 100f)] private float brakeForce = 2f;
    [FoldoutGroup("Attributes"), SerializeField, Range(0f, 100f)] private float maxForwardSpeed = 10f;
    [FoldoutGroup("Attributes"), SerializeField, Range(0f, 100f)] private float maxBackwardSpeed = 6f;
    [FoldoutGroup("Attributes"), SerializeField, Range(0f, 100f)] private float rotationSpeed = 5f;

    private Vector2 moveInput;

    private void Awake()
    {
        if(rigidbody == null)
            rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        playerMarker.InitializeMarker(transform);
    }

    void Update()
    {
        moveInput = GetMoveInput();
    }

    private void FixedUpdate()
    {
        transform.Rotate(transform.up, moveInput.x * rotationSpeed * Time.fixedDeltaTime);
        if (moveInput.y > 0)
        {
            if (rigidbody.velocity.magnitude < maxForwardSpeed)
                rigidbody.AddForce(transform.forward * moveInput.y * acceleration, ForceMode.Acceleration);
            else
                rigidbody.velocity = transform.forward * rigidbody.velocity.magnitude;

        }
        else if (moveInput.y < 0)
        {
            if (rigidbody.velocity.magnitude < maxBackwardSpeed)
                rigidbody.AddForce(transform.forward * moveInput.y * decelearation, ForceMode.Acceleration);
            else
                rigidbody.AddForce(-rigidbody.velocity.normalized * decelearation, ForceMode.Acceleration);
        }
        else
        {
            if (rigidbody.velocity.magnitude > Mathf.Epsilon)
                rigidbody.AddForce(-rigidbody.velocity.normalized * brakeForce, ForceMode.Acceleration);
        }
    }

    private Vector2 GetMoveInput()
    {
        return InputManager.P_Input.PlayerActions.Movement.ReadValue<Vector2>();
    }
}
