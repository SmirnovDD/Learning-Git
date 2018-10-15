using UnityEngine;

public class PreventRotation : MonoBehaviour {
    public float height = 2;
    public Transform mobTransform;
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = mobTransform.position + Vector3.up * height;
	}
}
