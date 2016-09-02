using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {


	public void LoadLevel (string name) {
		Debug.Log ("Loading: " + name);
		SceneManager.LoadScene (name);
	}// end LoadLevel















}// end LevelManager
