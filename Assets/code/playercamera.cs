using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercamera : MonoBehaviour{
    public float smoothTimeX, smoothTimeY;
	public Vector2 velocity;
	public GameObject player;
	public Vector2 minPos, maxPos;
	public bool bound;
    void Start(){
        
    }
	void FixedUpdate () {
		float posX = Mathf.SmoothDamp (transform.position.x, player.transform.position.x, ref velocity.x, smoothTimeX);
		float posY = Mathf.SmoothDamp (transform.position.y, player.transform.position.y+1f, ref velocity.y, smoothTimeY);
		transform.position = new Vector3 (posX, posY, transform.position.z);
		if(bound) {
			transform.position = new Vector3 (Mathf.Clamp (transform.position.x, minPos.x, maxPos.x),
				Mathf.Clamp (transform.position.y, minPos.y, maxPos.y),
				Mathf.Clamp (transform.position.z, transform.position.z, transform.position.z)
			);
		}
	}
}
