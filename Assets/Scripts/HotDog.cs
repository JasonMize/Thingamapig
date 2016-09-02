using UnityEngine;
using System.Collections;

public class HotDog : MonoBehaviour {

	GameObject hotDogGun;
	GameObject thingamapig;

	void Start () {
		hotDogGun = GameObject.Find ("Hot Dog Gun");
		thingamapig = GameObject.Find ("Thingamapig");
	}// end Start
	
	void FixedUpdate () {
	}// end FixedUpdate

	void OnTriggerEnter (Collider other) {
		if (other.gameObject == thingamapig) {		// if bullet hits player move it back to gun and set velocity to 0	
			//print ("You ate my hot dog");
			this.transform.position = hotDogGun.transform.position;
			this.GetComponent<Rigidbody> ().velocity = new Vector3 (0,0,0);
			other.SendMessage ("PigDamage", 1f);
		}
		else {	// if bullet hits non player object, move it back to gun and set velocity to 0
			//print ("SPLAT!");	
			this.transform.position = hotDogGun.transform.position;
			this.GetComponent<Rigidbody> ().velocity = new Vector3 (0,0,0);
		}
	}// end OnTriggerEnter



}// end HotDog
