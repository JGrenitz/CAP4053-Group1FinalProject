using UnityEngine;
using System.Collections;

public class CheckCollision : MonoBehaviour {
	void OnTriggerEnter(Collider other){
		if(other.tag == "Player"){
			GetComponentInParent<LizardController>().fitness += 10.0f;
		}
	}

	void OnCollisionEnter(Collision collision){
		if(collision.collider.tag == "Player"){
			GetComponentInParent<LizardController>().fitness += 10.0f;
		}
	}
}
