using UnityEngine;
using System.Collections;

public class CameraPositions : MonoBehaviour {

	Vector3 offset;  // camera position distance from thingamapig

	float X;  // distance between camera position and Pig on X axis
	float pigZoomSpeed = 10f;
	
	void Start () {
		X = PlayerMove.thingamapig.transform.position.x - transform.position.x;
		offset.y = transform.position.y;
		offset.z = transform.position.z;	
	}// end Start
	
	void Update () {

	}// end update

	void FixedUpdate () {
		
		// matches camera positions to pig X position
		offset.x = PlayerMove.thingamapig.transform.position.x - X;
		transform.position = Vector3.MoveTowards (transform.position, offset, PlayerMove.pigVelocity + pigZoomSpeed * Time.deltaTime);

		CameraZoom ();
	}// end FixedUpdate

	void CameraZoom () {
		if (PlayerMove.pigVelocity > .1f) { // if Thingamapig is moving, set new zoom
			offset.z = -55;
			offset.y = 23;
		} 

		if (PlayerMove.pigVelocity <= .1f) { // if thingamapig isn't moving, set new zoom 
			StartCoroutine (PigStillnessTimer() ); // how many seconds pig must be still before camera starts zooming in
		}
	}// end CameraZooom


	IEnumerator PigStillnessTimer () {
		if (PlayerMove.pigVelocity <= .1f) {
			print ("start");
			yield return new WaitForSeconds (4);
			print ("end");
//			offset.z = -35;
//			offset.y = 12;
		}
	}// end PigStillnessTimer



}// end CameraPositions
