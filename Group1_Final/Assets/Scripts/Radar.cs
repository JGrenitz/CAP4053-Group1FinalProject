using UnityEngine;
using System.Collections;

public class Radar : MonoBehaviour {

	public bool showDebug;
	
	private int radius;
	private int playerLayerMask;
	private Collider[] hit;
	private bool a;
	
	// Use this for initialization
	void Start () {
		playerLayerMask = 1 << 8;
		radius = 10;
		hit = Physics.OverlapSphere (transform.position, radius, playerLayerMask);
	}
	
	void FixedUpdate(){
		hit = Physics.OverlapSphere (transform.position, radius, playerLayerMask);
	}
	
	void OnDrawGizmos() {
		if (showDebug == true) {
			Gizmos.color = Color.green;
			Gizmos.DrawWireSphere (transform.position, radius);
		}
	}
	
	public double checkRadar(){
		if(hit != null){
			if(hit.Length > 0){
				return (double)Vector3.Distance (transform.position, hit[0].transform.position);
			}
		}
		
		return (double)radius;
	}

	public double angleToPlayer(){
		if(hit != null){
			if(hit.Length > 0){
				Vector3 playerDir = hit[0].transform.position - transform.position;
				float angle = Vector3.Angle (transform.forward, playerDir);
				return (double)angle;
			}
		}

		return 0;
	}

	public float checkDir(){
		if(hit != null){
			if(hit.Length > 0){
				Vector3 playerDir = hit[0].transform.position - transform.position;
				float angleDir = AngleDir(transform.forward, playerDir, Vector3.up);
				return angleDir;
			}
		}

		return 0;
	}
	
	public double getRadius(){
		return (double)radius;
	}

	// Returns -1 if left of player heading, 1 if right	
	float AngleDir(Vector3 fwd, Vector3 targetDir, Vector3 up) {
		Vector3 perp = Vector3.Cross(fwd, targetDir);
		float dir = Vector3.Dot(perp, up);
		
		if (dir > 0f) {
			return 1f;
		} else if (dir < 0f) {
			return -1f;
		} else {
			return 0f;
		}
	}
}
