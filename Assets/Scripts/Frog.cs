using UnityEngine;
using System.Collections;

public class Frog : MonoBehaviour {



	GameObject rightMarker;
	GameObject leftMarker;


	float pigFaceDirection;

	bool grounded; // we are on the ground

	float rightTimer;
	float leftTimer;
	float countdownFar = 0f;
	float countdownMiddle = .5f;
	float coundownNear = 1.5f;
	float far = 25;
	float near = 10;


	void Start () {

		rightMarker = GameObject.Find ("Right Whatchamafrog Position");
		leftMarker = GameObject.Find ("Left Whatchamafrog Position");

	}// end Start


	void Update () {

		pigFaceDirection = PlayerMove.faceDirection; // PlayerMove bool 
		grounded = this.GetComponentInChildren<GroundTrigger> ().onGround; // are we on the ground
	}// end Update


	void FixedUpdate () {
		float frogDistanceToRight = rightMarker.transform.position.x - transform.position.x;  // distance from frog to right marker, the further from marker, the faster the frog hops
		float frogDistanceToLeft = transform.position.x - leftMarker.transform.position.x;  // distance from frog to left marker

		float jumpVerticalMomentum = 300;  
		float jumpForwardMomentum = 600;



		if (pigFaceDirection == 1) { // lead player to the right  - move frog to right marker position
			if (transform.rotation.y != 0) { // turn frog to right
				transform.rotation = Quaternion.Euler (0, 0, 0);
			}	
			if (rightTimer >= 0) { // count down timer to jump again
				rightTimer -= Time.deltaTime;
			}
			if (rightTimer <= 0) {
				if (transform.position.x < rightMarker.transform.position.x && grounded == true) {  // jump to position
					transform.GetComponent<Rigidbody> ().AddForce (jumpForwardMomentum + (frogDistanceToRight * 8), jumpVerticalMomentum, 0f);
				}
				if (frogDistanceToRight >= far) {  // reset timer
					rightTimer = countdownFar;
				}
				if (frogDistanceToRight < far && frogDistanceToRight >= near) {  // reset timer
					rightTimer = countdownMiddle;
				}
				if (frogDistanceToRight < near) {  // reset timer
					rightTimer = coundownNear;
				}
			}
			if (frogDistanceToRight > 80) {  // if frog is stuck and offscreen, jump him forward
				transform.position = new Vector3 (transform.position.x + 10, rightMarker.transform.position.y, transform.position.z);
			}
		}

		if (pigFaceDirection == -1) { // lead player to the left   - move frog to left marker position
			if (transform.rotation.y != 180) { // turn frog to left
				transform.rotation = Quaternion.Euler (0, 180, 0);
			}
			if (leftTimer >= 0) {  // count down timer to jump again
				leftTimer -= Time.deltaTime;
			}
			if (leftTimer <= 0) {   
				if (transform.position.x > leftMarker.transform.position.x && grounded == true) {   // jump to position
					transform.GetComponent<Rigidbody> ().AddForce (-jumpForwardMomentum - (frogDistanceToLeft * 8), jumpVerticalMomentum, 0f);
				}
				if (frogDistanceToLeft >= far) {  // reset timer
					leftTimer = countdownFar;
				}
				if (frogDistanceToLeft < far && frogDistanceToLeft >= near) {  // reset timer
					leftTimer = countdownMiddle;
				}
				if (frogDistanceToLeft < near) {  // reset timer
					leftTimer = coundownNear;
				}
			}
			if (frogDistanceToLeft > 90) {  // if frog is stuck and offscreen, jump him forward
				transform.position = new Vector3 (transform.position.x - 15, leftMarker.transform.position.y, transform.position.z);
			}
		}

	}// end FixedUpdate




}// End Frog
