using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Experience : MonoBehaviour {
	public int currLevel;
	public int numRooms;
	public int toLevel;

	public Slider xpSlider;
	public Text levelText;
	public Slider healthSlider;
	public Text meleeDamage;
	public Text rangedDamage;

	private PlayerController player;


	// Use this for initialization
	void Start () {
		currLevel = 0;
		numRooms = 2;
		toLevel = 3;
		levelText.text = "" + (currLevel + 1);
		xpSlider.value = numRooms;
		xpSlider.maxValue = toLevel;
		player = GameObject.Find ("Warrior").GetComponent<PlayerController> ();
	}
	
	// Update is called once per frame
	void Update () {
		meleeDamage.text = "" + player.GetComponent<PlayerController> ().melee;
		rangedDamage.text = "" + player.GetComponent<PlayerController> ().ranged;
	}

	public void levelUp(){
		numRooms = 0;
		currLevel++;
		toLevel = (currLevel * 2) + 3;

		xpSlider.value = numRooms;
		xpSlider.maxValue = toLevel;

		levelText.text = "" + (currLevel + 1);

		changeStats ();
	}

	//Function called when cleared a room.
	public void roomClear(){
		numRooms++;
		xpSlider.value = numRooms;
		if (numRooms >= toLevel) {
			levelUp();
		}
	}

	void changeStats(){
		int currMelee = player.GetComponent<PlayerController> ().melee;/*Read in character damage to int*/
		int currRanged = player.GetComponent<PlayerController> ().ranged;/*Read in character damage to int*/

		float numMeleeHits = player.GetComponent<PlayerController> ().numMelee++;
		float numRangedHits = player.GetComponent<PlayerController> ().numRanged++;

		int currHealth = 100+(20*currLevel);
		player.hp = currHealth;//Get max hp from player.

		int changeMelee = (int)Mathf.Floor(15 * (numMeleeHits/(numMeleeHits + numRangedHits)));
		currMelee += 5 + changeMelee;
		currRanged += 5 + (15 - changeMelee);

		healthSlider.maxValue = currHealth;
		healthSlider.value = currHealth;
		meleeDamage.text = "" + currMelee;
		player.GetComponent<PlayerController> ().melee = currMelee;
		rangedDamage.text = "" + currRanged;
		player.GetComponent<PlayerController> ().ranged = currRanged;
		player.GetComponent<PlayerController> ().numMelee = 0;
		player.GetComponent<PlayerController> ().numRanged = 0;
	}
}
