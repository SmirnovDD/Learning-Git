using UnityEngine;

public class DealDamage : MonoBehaviour {
    private Rigidbody2D rigidB;
    private PlayerController playerControllerScript;

    private void Start()
    {
        rigidB = gameObject.GetComponent<Rigidbody2D>();
        playerControllerScript = FindObjectOfType(typeof(PlayerController)) as PlayerController;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            playerControllerScript.TakeDamage(rigidB.angularVelocity * rigidB.mass);
        }
    }
}
