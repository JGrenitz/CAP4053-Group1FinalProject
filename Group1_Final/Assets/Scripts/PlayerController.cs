using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public int hp;
	public bool alive;
	
	private AnimationSelector animSelector;

	// Use this for initialization
	void Start () {
		animSelector = GetComponent<AnimationSelector>();
		hp = 50;
		alive = true;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(alive){
			if(hp <= 0){
				animSelector.die ();
				alive = false;
				print(alive);
			}
			else
				animSelector.animate ();
		}
	}


	void OnCollisionEnter(Collision collision){
		//if(collision.collider.tag == "axe"){
			hp -= 10;
			print("player hp:"+hp);
		//}
	}
}