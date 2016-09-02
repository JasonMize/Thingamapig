using UnityEngine;
using System.Collections;

public class PigGizmoPos : MonoBehaviour {

	public string imageName;


	void Start () {
	}// end Start
	
	void Update () {
	}// end Update


	void OnDrawGizmos () { // finds image in the Assets/Gizmos folder
		Gizmos.DrawIcon(transform.position, imageName, true);
	}// end OnDrawGizmos



}// end PigGizmosPos
