using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFB_DragonHeight : MonoBehaviour {

	public bool isFlying = false;
	Animator animator;
	public int animatorHash1;
	public int animatorHash2;
	public int animatorHash3;
	public bool isDying = false;
	public int currentHash;
	public float normalizedTime;
	public float deathGroundTime = 0.8f;

	public GameObject airAnimations;
	public GameObject groundAnimations;

	public float[] landingTime;  // How many seconds to land.  0 = idle, 1 = running
	public float[] landingDistance; // How much down does the land normall take us.
	public float landingDelta; // Difference we have to make up to end at zero
	public float landingTimer; // keeps track of our landing time
	public float groundHeight = 0; // Height of ground after we land.  For demo it's just zero.

	public float tiltMax = 30.0f;		// Maximum tilt
	public float turnMax = 80.0f;		// Maximum turning tilt
	public float turnMod = 4.0f;		// Modification for actual turning
	public float turningValue = 0.0f;	// Actual turning in game.

	void Start () {
		animator = GetComponent<Animator> ();
		animatorHash1	= Animator.StringToHash("Base Layer.Air.FlyDeath01");
		animatorHash2	= Animator.StringToHash("Base Layer.Air.FlyDeath02");
		animatorHash3	= Animator.StringToHash("Base Layer.Air.FlyDeath03");
	}
	
	void Update () {
		//Debug.Log ("update position: " + transform.position);
		AnimatorStateInfo animatorStateInfo = animator.GetCurrentAnimatorStateInfo (0);
		normalizedTime = animatorStateInfo.normalizedTime;
		currentHash = animatorStateInfo.fullPathHash;

		if (currentHash != animatorHash1 && currentHash != animatorHash2 && currentHash != animatorHash3) {
			isDying = false;
		} else {
			isDying = true;
			if (currentHash == animatorHash1 || currentHash == animatorHash2)
			{
				if (transform.position.y < 10.0f)
					animator.SetTrigger("flyDeathEnd");
			}
		}

		if (isDying && currentHash == animatorHash3 && normalizedTime >= deathGroundTime) {
			Vector3 newHeight = new Vector3 (transform.position.x, 0, transform.position.z);
			transform.position = newHeight;
			isDying = false;
		}

		if (landingTimer > 0) {
			float newY = transform.position.y - (landingDelta * Time.deltaTime);
			if (newY < 0) {
				newY = 0;
			}
			Vector3 newHeight = new Vector3 (transform.position.x, newY, transform.position.z);
			transform.position = newHeight;
			landingTimer -= Time.deltaTime;
		}

		float turnY = transform.eulerAngles.y + (turningValue * turnMax * Time.deltaTime * turnMod);
		Vector3 newRotation = new Vector3 (transform.eulerAngles.x, turnY, transform.eulerAngles.z);
		transform.eulerAngles = newRotation;
	}

	public void SetGroundNumbers(int landType){
		// Landing Delta:  Subtract the landingDistance for the land type from the distance to the ground.
		// We will do a portion of that each frame until the timer is up.
		landingDelta = ((transform.position.y - groundHeight) - landingDistance[landType]) / landingTime[landType];
		landingTimer = landingTime [landType];
	}

	public void SetGroundHeight(){
		Vector3 newHeight = new Vector3 (transform.position.x, groundHeight, transform.position.z);
		transform.position = newHeight;
		Debug.Log ("SetGroundHeight: " + newHeight);
	}

	public void StartFlying(){
		//Debug.Log ("StartFlying()");
		isFlying				= true;
		groundAnimations.SetActive (false);
		airAnimations.SetActive (true);
	}

	public void StartGround(){
		isFlying				= false;
		groundAnimations.SetActive (true);
		airAnimations.SetActive (false);
	}

	public void SetTilt(float value){
		Vector3 newRotation = new Vector3 (value * tiltMax, transform.eulerAngles.y, transform.eulerAngles.z);
		transform.eulerAngles = newRotation;
	}

	public void SetTurn(float value){
		turningValue = value;
		Vector3 newRotation = new Vector3 (transform.eulerAngles.x, transform.eulerAngles.y, -value * turnMax);
		transform.eulerAngles = newRotation;
	}
}
