using UnityEngine;
using System.Collections;

public class OrcAttachWeapon : MonoBehaviour {
	public GameObject hand;

	// Use this for initialization
	void Start () {
		hand = GameObject.Find ("MaskedOrcRigWeapon");
	}
	
	// Update is called once per frame
	void Update () {
		transform.parent = hand.transform;
	}
}
