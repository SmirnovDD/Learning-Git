using UnityEngine;

public class DealDamageToMob : MonoBehaviour {
    private Rigidbody2D rigidB;
    private float oldVelocity;
    private void Start()
    {
        rigidB = gameObject.GetComponent<Rigidbody2D>();
    }

    private void LateUpdate()
    {
        Debug.Log("velocity is " + (int)rigidB.velocity.magnitude + " old velocity is " + (int)oldVelocity + " force is " + (int)(rigidB.velocity.magnitude - oldVelocity / Time.fixedDeltaTime) * rigidB.mass);
        oldVelocity = rigidB.velocity.magnitude;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Mob"))
        {
            collision.gameObject.GetComponent<BasicMob>().TakeDamage((rigidB.velocity.magnitude - oldVelocity / Time.fixedDeltaTime) * rigidB.mass);
        }
    }
}
