using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {

	GameObject rightMarker;
	GameObject leftMarker;

	float speed = 50f;


	void Start () {
		rightMarker = GameObject.Find ("Right Camera Position");
		leftMarker = GameObject.Find ("Left Camera Position");
	}// end Start



	void Update () {
	}// end Update


	void FixedUpdate () {
		if (PlayerMove.faceDirection == 1) { // lead player to the right  - move camera to right marker position
			transform.position = Vector3.MoveTowards(transform.position, rightMarker.transform.position, speed * Time.deltaTime);
		}
		if (PlayerMove.faceDirection == -1) { // lead player to the left   - move camera to left marker position
			transform.position = Vector3.MoveTowards (transform.position, leftMarker.transform.position, speed * Time.deltaTime);
		}
	}// end FixedUpdate





}// end CameraMove






