using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LizardController : MonoBehaviour {
	public bool alive;
	public float fitness;
	public int damage;

	private int hp;
	private LizardAnimationSelector[] animSelector;
	private GameObject player;
	private float moveSpeed;
	private float rotateSpeed;
	private NeuralNet net;
	private Sensors rays;
	private Radar radar;
	private List<double> inputs;
	private List<double> outputs;

	// Use this for initialization
	void Start () {
		fitness = 0;
		hp = 50;
		damage = 5;
		moveSpeed = 2.0f;
		rotateSpeed = 100.0f;
		alive = true;
		animSelector = GetComponentsInChildren<LizardAnimationSelector>();
		player = GameObject.Find ("Warrior");
		inputs = new List<double>();
		outputs = new List<double>();
		net = GetComponent<NeuralNet>();
		rays = GetComponent<Sensors>();
		radar = GetComponent<Radar>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(alive){
			inputs.Clear ();
			outputs.Clear ();

			inputs.Add (net.sigmoid (radar.getRadius () - radar.checkRadar (), net.activationResponse));
			inputs.Add (net.sigmoid(radar.angleToPlayer (), net.activationResponse));
			inputs.Add (net.sigmoid (rays.playerRange - rays.checkPlayer (), net.activationResponse));
			inputs.Add (net.sigmoid (rays.wallRange - rays.checkCenter (), net.activationResponse));
			inputs.Add (net.sigmoid (rays.wallRange - rays.checkLeft(), net.activationResponse));
			inputs.Add (net.sigmoid (rays.wallRange - rays.checkRight (), net.activationResponse));

			outputs = net.updateNet (inputs);

			if(!animSelector[0].dying){
				if(hp <= 0){
					animSelector[0].die ();
					rigidbody.isKinematic = true;
					GetComponent<Collider>().isTrigger = true;
					alive = false;
				}
				else if(!player.GetComponent<PlayerController>().alive){
					animSelector[0].idle ();
				}
				else if(outputs[0] > 0.2f){
					animSelector[0].attack ();
				}
				else{
					animSelector[0].walk ();
					transform.Translate (-transform.forward * Time.deltaTime * moveSpeed *(float)outputs[1]);
					transform.Rotate(transform.up * Time.deltaTime * rotateSpeed * radar.checkDir () * (float)outputs[2]);
				}
	
				transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
			}
		}
	}

	void OnCollisionEnter(Collision collision){
		if(alive){
			if((collision.collider.tag == "playerweapon" && collision.collider.GetComponentInParent<AnimationSelector>().attacking)){
				hp -= player.GetComponent<PlayerController>().melee;
				animSelector[0].hit();
				
				player.GetComponent<PlayerController> ().numMelee++;
			}
			if(collision.collider.tag == "catbullet"){
				hp -= player.GetComponent<PlayerController>().ranged;
				player.GetComponent<PlayerController> ().numRanged++;
			}
		}
	}
}
