using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
	public int hp;
	public bool alive;
	public Slider healthSlider;
	public int melee;
	public int ranged;
	public float numMelee;
	public float numRanged;
	
	private AnimationSelector animSelector;
	private GameObject rEnemy;
	private GameObject mEnemy;

	// Use this for initialization
	void Start () {
		animSelector = GetComponent<AnimationSelector>();
		hp = 100;
		alive = true;
		melee = 10;
		ranged = 10;
		numMelee = 0.0F;
		numRanged = 0.0F;
		rEnemy = GameObject.FindGameObjectWithTag ("rsnged");
		mEnemy = GameObject.FindGameObjectWithTag ("melee");
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
		healthSlider.value = hp;

	}


	void OnCollisionEnter(Collision collision){
		if(collision.collider.tag == "catarrow"){
			hp -= rEnemy.GetComponent<OrcRangedController>().damage;
		}
	}

	void OnTriggerEnter(Collider other){
		if(other.tag == "enemyweapon" && other.GetComponentInParent<LizardAnimationSelector>().attacking){
			hp -= mEnemy.GetComponent<LizardController>().damage;
		}
	}
}