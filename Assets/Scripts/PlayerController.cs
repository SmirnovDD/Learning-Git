using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour {
	public float moveSpeed = 10; //скорость, на которую умножается вектор движения с джостика
    public float maxSpeed = 10; //максимальная скорость движения rigidB
    public float rotateSpeed = 2; //скорость, на которую умножается вектор с джостика поворота
    public float maxAngularVelocity = 20;
    public float rotationOffcet;
    public float torqueForce;

	private Rigidbody2D rigidB;
	private Transform thisTransform;
    private float torque;
    private float targetAngle; //угол, на который будет поворачиваться персонаж

    private readonly VectorPid angleController = new VectorPid(33.7766f, 0, 0.2553191f);
    private readonly VectorPid angularVelocityController = new VectorPid(9.244681f, 0, 0.06382979f);

    // Use this for initialization
    void Start ()
	{
		rigidB = GetComponent<Rigidbody2D>();
		thisTransform = transform;
        targetAngle = transform.eulerAngles.z;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
        Movement();
        Rotation();
	}

	public void Movement()
	{
        //Vector2 moveVector = new Vector2(CrossPlatformInputManager.GetAxisRaw("Horizontal"), CrossPlatformInputManager.GetAxisRaw("Vertical")) * moveSpeed;
        Vector2 moveVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * moveSpeed;
        Vector3 localVelocity = thisTransform.InverseTransformDirection(rigidB.velocity);
        Vector2 velocityChange = thisTransform.InverseTransformDirection(moveVector.x, moveVector.y, 0) - localVelocity;

        velocityChange = Vector2.ClampMagnitude(velocityChange, maxSpeed);
        velocityChange = thisTransform.TransformDirection(velocityChange);

        rigidB.AddForce(velocityChange, ForceMode2D.Impulse);
	}

    public void Rotation()
    {
        Vector3 rotateVector = new Vector3(CrossPlatformInputManager.GetAxisRaw("RotateHorizontal"), CrossPlatformInputManager.GetAxisRaw("RotateVertical"), 0);

        targetAngle = (Mathf.Atan2(rotateVector.y, rotateVector.x) * Mathf.Rad2Deg - 90) * rotateSpeed * Time.fixedDeltaTime;

        float angleError = Mathf.DeltaAngle(transform.eulerAngles.z, targetAngle);
        float torqueCorrectionForAngle = angleController.Update(angleError, Time.fixedDeltaTime);

        rigidB.angularVelocity = Mathf.Clamp(rigidB.angularVelocity, -maxAngularVelocity, maxAngularVelocity);

        float angularVelocityError = -rigidB.angularVelocity;
        float torqueCorrectionForAngularVelocity = angularVelocityController.Update(angularVelocityError, Time.fixedDeltaTime);

        torque = torqueCorrectionForAngle + torqueCorrectionForAngularVelocity;
        rigidB.AddTorque(torque);
    }
}
