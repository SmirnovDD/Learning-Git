using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobsController : MonoBehaviour {

    public MobsController() { }
    public float HP { get; set; }
    public float Damage { get; set; }
    public float AttackDst{ get; set; }
    public float AttackSpeed { get; set; }
    public float MoveSpeed { get; set; }
    public float MaxMoveVelocity { get; set; }
    public float MaxAngularVelocity { get; set; }

    private Transform playerTransform;
    private Rigidbody2D rigidB;
    private Vector2 moveVector;
    private float timer;
    private bool startAttacking;
    private float rotationY;

    private readonly VectorPid angleController = new VectorPid(10, 0, 0.2553191f);
    private readonly VectorPid angularVelocityController = new VectorPid(9.244681f, 0, 0.06382979f);

    private void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        rigidB = GetComponent<Rigidbody2D>();
    }
    public void MoveToPlayer()
    {
        if (Vector2.Distance(transform.position, playerTransform.position) > AttackDst)
        {
            moveVector = playerTransform.position - transform.position;
            Vector3 localVelocity = transform.InverseTransformDirection(rigidB.velocity);
            Vector2 velocityChange = transform.InverseTransformDirection(moveVector.x, moveVector.y, 0) - localVelocity;

            velocityChange = Vector2.ClampMagnitude(velocityChange, MaxMoveVelocity);
            velocityChange = transform.TransformDirection(velocityChange);

            Vector2 addForceVector = new Vector2(velocityChange.x + Mathf.Sin(rigidB.velocity.x), velocityChange.y);

            rigidB.AddForce(addForceVector, ForceMode2D.Impulse);

            startAttacking = false;

            float targetAngle = (Mathf.Atan2(moveVector.y, moveVector.x) * Mathf.Rad2Deg - 90);

            if (targetAngle < 0)
                targetAngle += 360;

            if (Vector2.Distance(transform.position, playerTransform.position) > AttackDst + 3)
            {
                if (targetAngle > transform.rotation.eulerAngles.z)
                {
                    rigidB.AddTorque((targetAngle - transform.rotation.eulerAngles.z) / 5);
                }
                else if (targetAngle < transform.rotation.eulerAngles.z)
                {
                    rigidB.AddTorque(-(transform.rotation.eulerAngles.z - targetAngle) / 5);
                }
            }
        }
        else
        {
            startAttacking = true;           
        }
    }
    public void AttackPlayer()
    {
        if (Time.time > timer && startAttacking)
        {
            float targetAngle = (Mathf.Atan2(moveVector.y, moveVector.x) * Mathf.Rad2Deg - 90);

            float angleError = Mathf.DeltaAngle(transform.eulerAngles.z, targetAngle);
            float torqueCorrectionForAngle = angleController.Update(angleError, Time.fixedDeltaTime);

            rigidB.angularVelocity = Mathf.Clamp(rigidB.angularVelocity, -MaxAngularVelocity, MaxAngularVelocity);

            float torque = torqueCorrectionForAngle;

            int changeDir = 1;

            if (Mathf.Abs(rigidB.angularVelocity) < 5 && torque > 0)
                changeDir = -4;
            else if (Mathf.Abs(rigidB.angularVelocity) < 5 && torque < 0)
                changeDir = 4;

            if (rigidB.angularVelocity < 0 && torque > 0)
                changeDir = -1;
            else if (rigidB.angularVelocity > 0 && torque < 0)
                changeDir = -1;

            torque *= changeDir;

            rigidB.AddTorque(torque);

            timer = Time.time + AttackSpeed;
        }
        Quaternion rotation = Quaternion.Euler(0, rotationY+16, 0);
        rigidB.AddForce((playerTransform.position - (rotation * moveVector)) * Time.fixedDeltaTime);
    }
    public virtual void TakeDamage(float damage)
    {

    }
        
}
