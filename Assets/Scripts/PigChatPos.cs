using UnityEngine;
using System.Collections;

public class PigChatPos : MonoBehaviour {

	Vector3 offset;

	float offsetX;
	float offsetY;

	PlayerMove thingamapig;

	//GameObject thingamapig;


	void Start () {

		thingamapig = GameObject.FindObjectOfType<PlayerMove> ();

		// find the chat position and its distance from Thingamapig
		offsetX = thingamapig.transform.position.x - transform.position.x;
		offsetY = thingamapig.transform.position.y - transform.position.y;
		offset.z = transform.position.z;

	}// end Start


	void Update () {

	}// end Update

	void FixedUpdate () {

		// move the chat with Thingamapig
		offset.x = thingamapig.transform.position.x - offsetX;
		offset.y = thingamapig.transform.position.y - offsetY; 
		transform.position = offset;

	}// end FixedUpdate


}// end PigChatPos

