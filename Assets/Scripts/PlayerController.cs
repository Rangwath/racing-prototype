using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Configuration
    [SerializeField] float maxSpeed = 20f;
    [SerializeField] float accelerationFactor = 10f;
    [SerializeField] float turnFactor = 3f;
    [SerializeField] float driftFactor = 0.5f;

    // Local Variables
    float accelerationInput;
    float steeringInput;
    float rotationAngle;
    float forwardVelocity;

    // Components
    Rigidbody2D myRigidbody;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        rotationAngle = transform.rotation.eulerAngles.z;
    }

    void FixedUpdate() 
    {
        ApplyForwardForce();
        DecreaseSideVelocity();
        ApplySteering();
    }

    void OnMove(InputValue value) 
    {
        // Get the movement input and save it to local variables
        Vector2 moveInput = value.Get<Vector2>();
        accelerationInput = moveInput.y;
        steeringInput = moveInput.x;
    }

    void ApplyForwardForce()
    {
        // Calculate how much forward the object is moving
        forwardVelocity = Vector2.Dot(transform.up, myRigidbody.velocity);

        // Limit so that the object cannot move forward faster than the max speed
        if (forwardVelocity >= maxSpeed && accelerationInput > 0)
        {
            return;
        }

        // Limit so that the object cannot move backwards faster than 50 % of the max speed
        if (forwardVelocity <= -maxSpeed * 0.5f && accelerationFactor < 0)
        {
            return;
        }

        // Limit so that we cannot go faster than max speed in any direction while accelerating
        if (myRigidbody.velocity.sqrMagnitude > maxSpeed * maxSpeed && accelerationFactor > 0)
        {
            return;
        }

        // Apply drag if there is no accelerationInput so that the object stops when player is not accelerating
        if (accelerationInput == 0)
        {
            myRigidbody.drag = Mathf.Lerp(myRigidbody.drag, 3f, Time.fixedDeltaTime * 3f);
        }
        else
        {
            myRigidbody.drag = 0;
        }

        // Create a forward force (up force) based on accelerationInput (up/down keys)
        Vector2 forwardForceVector = transform.up * accelerationInput * accelerationFactor;

        // Apply the force to rigidbody pushing the object forward
        myRigidbody.AddForce(forwardForceVector, ForceMode2D.Force);
    }

    void ApplySteering()
    {
        // Update the rotation angle based on steeringInput (left/right keys)
        rotationAngle -= steeringInput * turnFactor;

        // Apply steering by rotating the rigidbody object
        myRigidbody.MoveRotation(rotationAngle);
    }

    void DecreaseSideVelocity()
    {
        Vector2 forwardVelocity = transform.up * Vector2.Dot(myRigidbody.velocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(myRigidbody.velocity, transform.right);

        myRigidbody.velocity = forwardVelocity + rightVelocity * driftFactor;
    }
}
