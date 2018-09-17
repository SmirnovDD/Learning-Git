using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour {
	public float moveSpeed = 10; //скорость, на которую умножается вектор движения с джостика
    public float maxSpeed = 10; //максимальная скорость движения rigidB
    public float rotateSpeed = 2; //скорость, на которую умножается вектор с джостика поворота
    public float rotationOffcet;
    public float torqueForce;

	private Rigidbody2D rigidB;
	private Transform thisTransform;

    private readonly VectorPid angularVelocityController = new VectorPid(33, 0, 0.1f);
    private readonly VectorPid headingController = new VectorPid(26, 0, 0.1f);

    // Use this for initialization
    void Start ()
	{
		rigidB = GetComponent<Rigidbody2D>();
		thisTransform = transform;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
        Movement();
        Rotation();
	}

	public void Movement()
	{
        Vector2 moveVector = new Vector2(CrossPlatformInputManager.GetAxisRaw("Horizontal"), CrossPlatformInputManager.GetAxisRaw("Vertical")) * moveSpeed;
        Vector3 localVelocity = thisTransform.InverseTransformDirection(rigidB.velocity);
        Vector2 velocityChange = thisTransform.InverseTransformDirection(moveVector.x, moveVector.y, 0) - localVelocity;

        velocityChange = Vector2.ClampMagnitude(velocityChange, maxSpeed);
        velocityChange = thisTransform.TransformDirection(velocityChange);

        rigidB.AddForce(velocityChange, ForceMode2D.Impulse);
	}

    public void Rotation()
    {
        Vector3 rotateVector = new Vector3(CrossPlatformInputManager.GetAxisRaw("RotateHorizontal"), CrossPlatformInputManager.GetAxisRaw("RotateVertical"), 0);

        //if(rigidB.rotation < (Mathf.Atan2(rotateVector.y, rotateVector.x) * Mathf.Rad2Deg - 90) - rotationOffcet)
        //{
        //    rigidB.AddTorque(torqueForce);
        //}

        var angularVelocityError = rigidB.angularVelocity * -1;

        var angularVelocityCorrection = angularVelocityController.Update(angularVelocityError, Time.deltaTime);

        rigidB.AddTorque(angularVelocityCorrection);

        var desiredHeading = rotateVector;

        Debug.DrawRay(transform.position, desiredHeading, Color.magenta);

        var currentHeading = transform.up;
        Debug.DrawRay(transform.position, currentHeading * 15, Color.blue);

        var headingError = Vector3.Cross(currentHeading, desiredHeading);
        var headingCorrection = headingController.Update(headingError.z, Time.deltaTime);

        rigidB.AddTorque(headingCorrection);

    }
}
