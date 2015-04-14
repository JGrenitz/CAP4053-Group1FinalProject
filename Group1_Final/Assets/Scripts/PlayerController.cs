using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public int hp;
	public bool alive;
	
	private AnimationSelector animSelector;

	// Use this for initialization
	void Awake () {
		animSelector = GetComponent<AnimationSelector>();
		hp = 300;
		alive = true;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(alive){
			if(hp <= 0){
				animSelector.die ();
				alive = false;
			}
			else
				animSelector.animate ();
		}
	}


	void OnCollisionEnter(Collision collision){
		if(collision.collider.tag == "enemyweapon" || collision.collider.tag == "catarrow"){
			hp -= 10;
		}
	}
}