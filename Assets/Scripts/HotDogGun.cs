using UnityEngine;
using System.Collections;


public class HotDogGun : MonoBehaviour {

	GameObject hotDog;
	float hotdogspeed = 1800f;
	bool loaded;


	void Start () {
		hotDog = GameObject.Find ("Hot Dog");
	}// end Start
		
	void Update () {
	}// end Update


	void FixedUpdate () {
		if (loaded == true) { // fire the first hotdog
			StartCoroutine (HotDogTimer ());
		}
	}// end FixedUpdate
		
	IEnumerator HotDogTimer () { // count down to fire
		yield return new WaitForSeconds (2); // seconds between gun being loaded and gun firing
		Fire ();
	}// end TimeCountDown

	void OnTriggerEnter (Collider other) {   // set gun to loaded if hotdog present
		if (other.gameObject == hotDog) {
			//print ("Gun Loaded");
			loaded = true;
		}
	}// end OnCollisionEnter		

	void Fire () {  // if timer is 0 and hot dog is at gun, fire that bad boy then reset everything to false
		if (loaded == true) {
			if (PlayerMove.thingamapig.transform.position.x < hotDog.transform.position.x) { // fire hot dog left
				hotDog.GetComponent<Rigidbody> ().AddForce (-hotdogspeed, 0f, 0f);
			}
			if (PlayerMove.thingamapig.transform.position.x > hotDog.transform.position.x) { // fire hot dog right
				hotDog.GetComponent<Rigidbody> ().AddForce (hotdogspeed, 0f, 0f);
			}
			loaded = false;
		}
	}// end Fire

}// end HotDogGun
