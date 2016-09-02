using UnityEngine;
using System.Collections;

public class PigChat : MonoBehaviour {

	GameObject leftChatPos;
	GameObject rightChatPos;

	GameObject chatText;
	GameObject chatBubble;

	float pigFaceDirection;

	float speed = 50f;


	void Start () {
		leftChatPos = GameObject.Find ("Pig Chat Position Left");
		rightChatPos = GameObject.Find ("Pig Chat Position Right");
		chatText = GameObject.Find ("Pig Chat Text");
		chatBubble = GameObject.Find ("PigChat");


	}// end Start
	
	void Update () {
		pigFaceDirection = PlayerMove.faceDirection; // PlayerMove bool 

	}// end Update


	void FixedUpdate () {

		// turn chat bubble to face other direction when pig turns
		if (pigFaceDirection == 1) { // if player faces right, turn right and move to left pos
			if (chatBubble.transform.rotation.y != 0) { // turn chat bubble right
				chatBubble.transform.rotation = Quaternion.Euler (0, 0, 0);
			}	
			chatText.GetComponent<RectTransform>().rotation = Quaternion.Euler (0, 0, 0);  // cancels the parents rotation, leaving it facing the right direction
			chatBubble.transform.position = Vector3.MoveTowards (chatBubble.transform.position, leftChatPos.transform.position, speed * Time.deltaTime);
		}
		if (pigFaceDirection == -1) { // if player faces left, turn left and move to right pos
			if (chatBubble.transform.rotation.y != 180) { // turn chat bubble left
				chatBubble.transform.rotation = Quaternion.Euler (0, 180, 0);
			}
			chatText.GetComponent<RectTransform>().rotation = Quaternion.Euler (0, 0, 0);	// cancels the parents rotation, leaving it facing the right direction		
			chatBubble.transform.position = Vector3.MoveTowards (chatBubble.transform.position, rightChatPos.transform.position, speed * Time.deltaTime);
		}



	}// end FixedUpdate






}// end PigChat
