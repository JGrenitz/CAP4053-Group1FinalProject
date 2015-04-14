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


	// Use this for initialization
	void Start () {
		currLevel = 0;
		numRooms = 0;
		toLevel = 3;
		levelText.text = "" + (currLevel + 1);
		xpSlider.value = numRooms;
		xpSlider.maxValue = toLevel;
	}
	
	// Update is called once per frame
	void Update () {
	}

	private void levelUp(){
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
		int currMelee = 5;/*Read in character damage to int*/
		int currRanged = 5;/*Read in character damage to int*/

		int numMeleeHits = 100;//Read in number of hits the character had.
		int numRangedHits = 100;

		int currHealth = 100; //Get max hp from player.

		int changeMelee = 15 * (numMeleeHits/(numMeleeHits + numRangedHits));
		currMelee += 5 + changeMelee;
		currRanged += 5 + (15 - changeMelee);
		currHealth += 20;

		healthSlider.maxValue = currHealth;
		healthSlider.value = currHealth;
		meleeDamage.text = "" + currMelee;
		rangedDamage.text = "" + currRanged;
	}
}
