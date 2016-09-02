using UnityEngine;
using System.Collections;

public class GroundTrigger : MonoBehaviour {

	public bool onGround;

	void Start () {
	}// end Start

	void OnTriggerEnter (Collider other) {
		if (other.CompareTag ("Ground")) {
		//	print ("Houston, we have found the ground.");
			onGround = true;
		}
	}// end onTriggerEnter

	void OnTriggerExit (Collider other) {
		if (other.CompareTag ("Ground")) {
		//	print ("Goodbye ground.");
			onGround = false;
		}
	}// end OnTriggerExit

}// end GroundTrigger
