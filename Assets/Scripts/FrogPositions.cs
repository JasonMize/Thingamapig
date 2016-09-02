using UnityEngine;
using System.Collections;

public class FrogPositions : MonoBehaviour {

	Vector3 offset;

	float offsetX;

	PlayerMove thingamapig;

	//GameObject thingamapig;


	void Start () {

		thingamapig = GameObject.FindObjectOfType<PlayerMove> ();

		// find the frog position and its distance from Thingamapig
		offsetX = thingamapig.transform.position.x - transform.position.x;
		offset.y = transform.position.y;
		offset.z = transform.position.z;

	}// end Start


	void Update () {

	}// end Update

	void FixedUpdate () {

		// move the frog with Thingamapig
		offset.x = thingamapig.transform.position.x - offsetX;
		transform.position = offset;

	}// end FixedUpdate



}// end FrogPositions

