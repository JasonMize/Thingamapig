using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PigChatText : MonoBehaviour {

	float bubbleTimeMin = 3; // min countdown between thought bubbles
	float bubbleTimeMax = 8; // max countdown between thought bubbles

	float readTimeLength;  // time for the thought bubble to be read before disappearing

	Vector3 bubbleGone; // scale factor for chat bubbles
	Vector3 bubbleBig;  // scale factor for chat bubbles

	GameObject pigChat; // the chat bubble

	string pithyText; // text for the random pig sayings

	void Start () {
		pigChat = GameObject.Find ("PigChat");  // chat bubble
		bubbleGone = new Vector3 (0, 0, 0);	// scale for chat bubble gone
		bubbleBig = new Vector3 (3, 3, 3);  // scale for chat bubble full size
		readTimeLength = 2; // how long to wait before chat bubble shrinks
		pigChat.transform.localScale = bubbleGone; // start game with no chat bubble
		StartCoroutine (BubbleTimer() ); // start with random time before first chat bubbles
	}// end Start
	

	void Update () {
	}// end Update

	void PopBubble () { // scale chat bubble to 0 and reset all timers
		StartCoroutine (BubbleScaleDown() ); // slow scale chat bubble smaller
		StartCoroutine (BubbleTimer() ); // resets time between bubbles
	}// end PopBubble

	void BlowBubble () { // scale chat bubble to full size and pick a new random phrase
		StartCoroutine (BubbleScaleUp() );  // scale the chat bubble slowly larger
		StartCoroutine (ReadTimer() ); // timer for how long chat bubbles exist
	}// end BlowBubble

	IEnumerator BubbleScaleUp () {  // slowly increase size of chat bubble - called in BlowBubble
		while (pigChat.transform.localScale.x < bubbleBig.x) {  
			pigChat.transform.localScale = new Vector3 (pigChat.transform.localScale.x + bubbleBig.x * Time.deltaTime, 
			pigChat.transform.localScale.y + bubbleBig.y * Time.deltaTime, 
			pigChat.transform.localScale.z + bubbleBig.z * Time.deltaTime);
			yield return null;
		}
	}// end BubbleScaleUp

	IEnumerator BubbleScaleDown () { // slowly decrease size of chat bubble - called in PopBubble
		while (pigChat.transform.localScale.x > bubbleGone.x) {
			pigChat.transform.localScale = new Vector3 (pigChat.transform.localScale.x - .9f * Time.deltaTime,
			pigChat.transform.localScale.y - .9f * Time.deltaTime,	
			pigChat.transform.localScale.z - .9f * Time.deltaTime);
			yield return null;
		}
	}// end BubbleScaleDown

	IEnumerator BubbleTimer () { // timer between random chat bubbles
		yield return new WaitForSeconds (Random.Range (bubbleTimeMin, bubbleTimeMax));
		SaySomethingPithy ();
		BlowBubble ();
	}// end BubbleTimer

	IEnumerator ReadTimer () { // timer for how long chat bubble exists
		yield return new WaitForSeconds (readTimeLength);
		PopBubble ();
	}// end ReadTimer

	void SaySomethingPithy () { // picks a random phrase for chat bubble - int given in BlowBubble
		int i = Random.Range (1, 14);
		if (i == 1) {
			pithyText = "I AM a pig!";
		} else if (i == 2) {
			pithyText = "Oink, I say.";
		} else if (i == 3) {
			pithyText = "I am \n\n not a pig!";
		} else if (i == 4) {
			pithyText = "I'd like \n\n a carrot.";
		} else if (i == 5) {
			pithyText = "Where's \n\n the mud?";
		} else if (i == 6) {
			pithyText = "Trotters, \n\n don't fail \n\n me now!";
		} else if (i == 7) {
			pithyText = "Slaughter \n\n THIS!";
		} else if (i == 8) {
			pithyText = "Pig Power!";
		} else if (i == 9) {
			pithyText = "I'd really \n\n like an \n\n apple.";
		} else if (i == 10) {
			pithyText = "Bazinga!";
		} else if (i == 11) {
			pithyText = "I need \n\n coffee.";
		} else if (i == 12) {
			pithyText = "Banzai!";
		} else if (i == 13) {
			pithyText = "I am more \n\n than just \n\n bacon.";
		} else if (i == 14) {
			pithyText = "";
		} else if (i == 15) {
			pithyText = "";
		}
		this.GetComponent<TextMesh> ().text = pithyText;
	}// end SaySomethingPithy

	public void TriggeredStatements (int i) { // statements triggered by events
		if (i == 1) { // triggered by death
			readTimeLength = 5f;
			this.GetComponent<TextMesh> ().text = "I die!";
		}
		BlowBubble ();
	}// end TriggeredStatements


}// end PigChatText
