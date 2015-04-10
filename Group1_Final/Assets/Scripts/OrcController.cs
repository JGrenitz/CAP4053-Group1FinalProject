using UnityEngine;
using System.Collections;

public class OrcController : MonoBehaviour {
	public Animation animate;
	public int hp;
	public bool alive = true;
	private bool attack = false;

	private GameObject player;

	// Use this for initialization
	void Start () {
		animate = GetComponent<Animation>();
		player = GameObject.Find ("Warrior");
		hp = 50;
	}
	
	// Update is called once per frame
	void Update () {
		if(alive){
			if(hp <= 0){
				animate.CrossFade ("die02");
				alive = false;
			}
			else if(attack){
				animate.CrossFade ("hit");
			}
			else if(Vector3.Distance (player.transform.position, transform.position) < 3.0f){
				animate.CrossFade ("attack01");
			}
			else
				animate.CrossFade ("idle");
		}
	}

	void OnCollisionEnter(Collision collision){
		if(player.GetComponent<AnimationSelector>().attacking || collision.collider.tag == "catbullet"){
			StartCoroutine(underAttack());
			hp -= 10;
		}
	}
	
	IEnumerator underAttack(){
		attack = true;
		yield return new WaitForSeconds(animate["hit"].length);
		attack = false;
		yield break;
	}
}
