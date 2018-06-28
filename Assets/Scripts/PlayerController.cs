using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour {
	public float moveSpeed = 10;

	private Rigidbody2D rigidB;
	private Transform thisTransform;

	// Use this for initialization
	void Start ()
	{
		rigidB = GetComponent<Rigidbody2D>();
		thisTransform = transform;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Movement(); //получаем инпут с джостика, приравниваем его к скорости движения и поворачиваем спрайт по направлению движения 
		TEMPPCMOVEMENT();
	}

	public void Movement()
	{
		float moveY = CrossPlatformInputManager.GetAxisRaw("Vertical");
		float moveX = CrossPlatformInputManager.GetAxisRaw("Horizontal");

		Vector3 moveVector = new Vector3(moveX, moveY, 0);
		moveVector = moveVector.normalized;

		rigidB.velocity = moveVector * moveSpeed;

		if (rigidB.velocity != Vector2.zero)
		{
			float angle = Mathf.Atan2(rigidB.velocity.y, rigidB.velocity.x) * Mathf.Rad2Deg;
			thisTransform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
		}
	}

	public void MeleeAttack()
	{

	}

	public void TEMPPCMOVEMENT()
	{
		float moveY = Input.GetAxisRaw("Vertical");
		float moveX = Input.GetAxisRaw("Horizontal");

		Vector3 moveVector = new Vector3(moveX, moveY, 0);
		moveVector = moveVector.normalized;

		rigidB.velocity = moveVector * moveSpeed;

		Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		thisTransform.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - transform.position);
	}
}
