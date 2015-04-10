﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CatBullet : MonoBehaviour {
	public float speed = 50.0f;
	public float secondsUntilDestroyed = 10.0f;
	public List<GameObject> explosions;
	
	private float startTime;
	private GameObject newExplosion;
	private int explosionNumber;

	void FixedUpdate(){
		transform.position += speed * transform.forward * Time.deltaTime;

		if(Time.time - startTime >= secondsUntilDestroyed){
			Destroy(this.gameObject);
		}

		if(Time.time - startTime + 2 >= secondsUntilDestroyed){
			Destroy(newExplosion);
		}
	}
	
	void OnCollisionEnter(Collision collision){
		Destroy(this.gameObject);
		newExplosion = Instantiate(explosions[explosionNumber], transform.position, transform.rotation) as GameObject;
		Destroy(newExplosion, 4.0f); // Destroy explosion object after a few seconds
	}

	// Use this for initialization
	void Start () {
		startTime = Time.time;
		explosionNumber = Random.Range(0, explosions.Count); // Pick a random explosion for this cat
	}
}
