using UnityEngine;
using System.Collections;

public class CatGun : MonoBehaviour {
	public GameObject bullet;

	void Fire(){
		GameObject newBullet = Instantiate(bullet, transform.position, Quaternion.LookRotation (transform.forward)) as GameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown (KeyCode.Mouse1)){
			Fire();
		}
	}
}
