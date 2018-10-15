using UnityEngine;

public class CameraFollow : MonoBehaviour {
    [Range (1, 10)]
    public float lerpSpeed = 5;
    private Transform playerTransform;
	// Use this for initialization
	void Start ()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 newPos = new Vector3(Mathf.Lerp(transform.position.x, playerTransform.position.x, 0.1f * lerpSpeed), Mathf.Lerp(transform.position.y, playerTransform.position.y, 0.1f * lerpSpeed), transform.position.z);
        transform.position = newPos;
    }
}
