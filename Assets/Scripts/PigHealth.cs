using UnityEngine;
using System.Collections;

public class PigHealth : MonoBehaviour {

	float health;    	// 1 = basic pig, 0 = dead
	Animator anim;

	GameObject pigChatText;  // the chat bubble for Thingamapig

	// Use this for initialization
	void Start () {
		pigChatText = GameObject.Find ("Pig Chat Text");
		anim = GetComponent <Animator> ();
		health = 1;
	}// end Start
	
	// Update is called once per frame
	void Update () {
	}// end Update

	public void PigDamage (float damage) {
		health = health - damage;

		if (health == 0) {
			anim.SetBool ("Walking", false);
			anim.SetBool ("Jumping", false);
			anim.SetBool ("Slamming", false);
			anim.SetBool ("Dying", true);
			this.SendMessage ("PigDeath", true);
			pigChatText.SendMessage ("TriggeredStatements", 1);
		}
	}// end PigDamage


}// end PigHealth
