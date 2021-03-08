using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercamera : MonoBehaviour{
    public float smoothTimeX, smoothTimeY;
	public float zoom,speed,fstzoom;
	public Vector2 velocity;
	public GameObject player;
	public GameObject hihi;
	public Vector3 target;
	public Vector2 minPos, maxPos;
	public bool bound;
    void Start(){
		fstzoom = GetComponent<Camera>().orthographicSize;
    }
	
	void FixedUpdate () {
		if (hihi!=null){
			target = (player.transform.position+hihi.transform.position)/2;
		}
		else target=player.transform.position;
		float posX = Mathf.SmoothDamp (transform.position.x, target.x, ref velocity.x, smoothTimeX);
		float posY = Mathf.SmoothDamp (transform.position.y, target.y+1f, ref velocity.y, smoothTimeY);
		transform.position = new Vector3 (posX, posY, transform.position.z);
		if(bound) {
			transform.position = new Vector3 (Mathf.Clamp (transform.position.x, minPos.x, maxPos.x),
				Mathf.Clamp (transform.position.y, minPos.y, maxPos.y),
				Mathf.Clamp (transform.position.z, transform.position.z, transform.position.z)
			);
		}
	}
	public void LateUpdate(){
		if (zoom!=0){
			GetComponent<Camera>().orthographicSize = Mathf.Lerp(GetComponent<Camera>().orthographicSize,zoom,speed);
		}
		else GetComponent<Camera>().orthographicSize=fstzoom;
	}
}
