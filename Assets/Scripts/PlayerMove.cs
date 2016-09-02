using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {

	public static GameObject thingamapig; 
	Animator anim;  

	LevelManager levelManager;

	int idlingAnimHash;  // id numbers for animation states
	int walkingAnimHash;
	int jumpingAnimHash;
	int slammingAnimHash;
	int dyingAnimHash;

	public static float pigVelocity;  // how fast thingamapig is moving

	public static bool jump;		// jump up
	public static bool slam;		// attack slam 
	public static bool walk; 		// walk
	public static bool living;  	// is the pig alive? 
	public float moveDirection; // move left, right, or stop (-1, 1, 0)

	public static float faceDirection; // face left, right, or camera (-1, 1, 0)

	float touchDown;	// where the mouse or touch lands
	float touchUp;		// where the mouse or touch 

	bool grounded; 		// are we on the ground


	void Awake () {
		thingamapig = this.gameObject;
		anim = GetComponent <Animator> ();

		idlingAnimHash = Animator.StringToHash ("Idle");
		walkingAnimHash = Animator.StringToHash ("Walk");
		jumpingAnimHash = Animator.StringToHash ("Jump");
		slammingAnimHash = Animator.StringToHash ("Slam");
		dyingAnimHash = Animator.StringToHash ("Die");

		levelManager = GameObject.FindObjectOfType<LevelManager> ();
	}// end Awake

	void Start () {
		faceDirection = 1;
		living = true;
	}// end Start


	void Update () {
		if (living) { // if pig is alive
			grounded = this.GetComponentInChildren<GroundTrigger> ().onGround; // are we on the ground

			FaceLeftRight ();
			ControlPC ();
			PigAnimations ();
		} 
		if (!living) { // wait and then go to start scene
			StartCoroutine (DeathTimer() );
		}
	}// end Update

	void FixedUpdate () {
		if (living) { // if pig is alive
			PigVelocity ();
			Jumping ();
			Slamming ();
			Walking ();
		}
	}// end FixedUpdated

	IEnumerator DeathTimer () {
		yield return new WaitForSeconds (5); // how many seconds to wait before loading next level
		levelManager.LoadLevel ("Scene 0 - Start");
	}// end DeathTimer

	void PigAnimations ()	{  // turn off and on animation clips for Thingamapig
		float idleSpeed = .5f;
		//float walkSpeed = 1f;
		//float jumpSpeed = 1f;
		float slamSpeed = 1f;
		float dieSpeed = .5f;

		if (!grounded) {  // if pig is in the air
			anim.SetBool ("Walking", false);
			anim.SetBool ("Jumping", true);
			if (slam == true) {  // if pig is slamming
				anim.SetBool ("Jumping", false);
				anim.SetBool ("Slamming", true);
			}
		}
		if (grounded) {  // if pig is on the ground
			anim.SetBool ("Jumping", false);
			anim.SetBool ("Slamming", false);
			if (!walk) {
				anim.SetBool ("Walking", false);
			} else if (walk) {
				anim.SetBool ("Walking", true);
			}
		}

		if (anim.GetCurrentAnimatorStateInfo (0).shortNameHash == idlingAnimHash) {
			anim.speed = idleSpeed;
		}
		if (anim.GetCurrentAnimatorStateInfo (0).shortNameHash == walkingAnimHash) {
			PigAnimationSpeeds ();
		}
		if (anim.GetCurrentAnimatorStateInfo (0).shortNameHash == jumpingAnimHash) {
			PigAnimationSpeeds ();
		}
		if (anim.GetCurrentAnimatorStateInfo (0).shortNameHash == slammingAnimHash) {
			anim.speed = slamSpeed;
		}
		if (anim.GetCurrentAnimatorStateInfo (0).shortNameHash == dyingAnimHash) {
			anim.speed = dieSpeed;
		}
	}// end PigAnimations

	void PigAnimationSpeeds () {
		// make animations play faster as the pigs velocity is increased
		if (pigVelocity <= 0.1) {      // pig is not moving
			anim.speed = .5f;
		}
		if (pigVelocity >= 0.1f && pigVelocity < 1) {   // pig is moving slowly
				anim.speed = 1;
		}
		if (pigVelocity >= 1) {    // pig is moving fast
				anim.speed = pigVelocity / 10;
		}
	}// end PigAnimationSpeeds

	void PigDeath (bool dead) { // trigger sent by PigHealth script
		if (dead) {
			living = false;
		}
	}// end PigDeath

	void Walking () { // Walk
		float moveSpeed = 70;  
		float slowMoveSpeed = 25;
		float nearlyMaxSpeed = 40;  
		float maxSpeed = 50;

		if (grounded) { // only walk if on ground
			if (moveDirection == 1) { // move right 
				if (pigVelocity <= nearlyMaxSpeed) { // move right at full acceleration
					thingamapig.GetComponent<Rigidbody> ().AddForce (moveSpeed, 0f, 0f);
				}
				if (pigVelocity > nearlyMaxSpeed && pigVelocity <= maxSpeed) { // move right at partial acceleration till max speed is reached
					thingamapig.GetComponent<Rigidbody>().AddForce (slowMoveSpeed, 0f, 0f);
				}
			}// end move right

			if (moveDirection == -1) { // move left
				if (pigVelocity <= nearlyMaxSpeed) { // move left at full acceleration 
					thingamapig.GetComponent<Rigidbody>().AddForce (-moveSpeed, 0f, 0f);
				}
				if (pigVelocity > nearlyMaxSpeed && pigVelocity <= maxSpeed) { // move left at partial acceleration till max speed is reached
					thingamapig.GetComponent<Rigidbody>().AddForce (-slowMoveSpeed, 0f, 0f);
				}
			}// end move left
		}// end ground check
	}// end Walking

	void Slamming () { // Slam
		float slamSpeed = -5000;

		if (slam && !grounded) { // slam down
			thingamapig.GetComponent<Rigidbody>().AddForce (0f, slamSpeed, 0f);
			slam = false;
			thingamapig.GetComponentInChildren <ParticleSystem> ().Play ();   // trigger pig cloud particle system
		}// end slam down
	}// end Slamming

	void Jumping () { // jump
		float jumpSpeed = 950;  
		float jumpForwardMomentum = 200 + pigVelocity;

		if (jump && grounded) { // jump up
			if (faceDirection == 1) { // facing right 
				thingamapig.GetComponent<Rigidbody>().AddForce (jumpForwardMomentum, jumpSpeed, 0f);
				jump = false;
			}
			if (faceDirection == -1) { // facing left
				thingamapig.GetComponent<Rigidbody>().AddForce (-jumpForwardMomentum, jumpSpeed, 0f);
				jump = false;
			}
		}
	}// End Jumping

	void FaceLeftRight () { // turns the player 
		if (faceDirection == 1) { // move right
			if (transform.rotation.y != 0) { // turn player to right
				transform.rotation = Quaternion.Euler (0, 0, 0);
			}	
		}
		if (faceDirection == -1) { // move left
			if (transform.rotation.y != 180) { // turn player to left
				transform.rotation = Quaternion.Euler (0, 180, 0);
			}
		}
	}// end FaceLeftRight

	void ControlPC () { // keyboard controls for computer
		if (Input.GetKeyDown (KeyCode.W) || Input.GetKeyDown (KeyCode.UpArrow)) { // jump
			if (grounded) {
				jump = true;
			}
		} 
		if (Input.GetKeyDown (KeyCode.S) || Input.GetKeyDown (KeyCode.DownArrow)) { // slam 
			if (grounded == false) {
				slam = true;	
			}
		}
		if (Input.GetKeyDown (KeyCode.D) || Input.GetKeyDown (KeyCode.RightArrow)) { // move right
			moveDirection = 1;
			faceDirection = 1;
		}
		if (Input.GetKeyUp (KeyCode.D) || Input.GetKeyUp (KeyCode.RightArrow)) { // stop move rig
			moveDirection = 0;
		}
		if (Input.GetKeyDown (KeyCode.A) || Input.GetKeyDown (KeyCode.LeftArrow)) { // move left
			moveDirection = -1;
			faceDirection = -1;
		}
		if (Input.GetKeyUp (KeyCode.A) || Input.GetKeyUp (KeyCode.LeftArrow)) { // stop move left
			moveDirection = 0;
		}
	}// end ControlPC

	public void ControlPhone (float i) { // controls for phone
		if (i == 1) { // button on phone - move right
			moveDirection = 1;
			faceDirection = 1;
		}
		if (i == -1) { // button on phone - move left
			moveDirection = -1;
			faceDirection = -1;
		}
		if (i == 0) { // release right or left button on phone - stop
			moveDirection = 0;
		}

		// touch & mouse controls for jumping
		if (i == 2) { // where the touch lands
			touchDown = Input.mousePosition.y / Screen.height;
			//print ("touchdown: " + touchDown);
		}
		if (i == 3) { // where the touch is when it releases
			touchUp = Input.mousePosition.y / Screen.height;
			//print ("touchUp: " + touchUp);
			if (touchUp > touchDown + 0.1f && grounded) { // if mouse has moved up on screen, jump
				//print ("touchdown +: " + touchDown + .25);
				jump = true;
			}
			if (touchUp < touchDown + 0.1f && !grounded) { // if mouse has moved down, slam
				slam = true;
			}
		}
	}// end ControlPhone
		
	public void PigVelocity () { // how fast is Thingamapig moving?
		pigVelocity = thingamapig.GetComponent<Rigidbody> ().velocity.magnitude;	
		if (grounded && pigVelocity > .05f) {  // if we're on the ground and moving, we must be walking
			walk = true;
		} else {
			walk = false;
		}
	}// end PigVelocity


}// end PlayerMove
