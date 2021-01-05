using UnityEngine;

public class medusaDemo : MonoBehaviour {

	public Animator animator;								// Animator controller
	public float desiredHairWeight = 1.0f;					// Weight of hair animations
	public float hairWeightChangeSpeed = 5f;				// Change per second

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (animator.GetLayerWeight (1) != desiredHairWeight) {
			animator.SetLayerWeight(1, Mathf.MoveTowards(animator.GetLayerWeight(1), desiredHairWeight, hairWeightChangeSpeed * Time.deltaTime));
		}
	}

	public void UpdateLocomotion(float value){
		if (!animator) {
			animator = GetComponent<Animator> ();
		}
		if (animator) {
			animator.SetFloat ("locomotion", value);
		}
	}

	public void SetDesiredHairWeight(float newValue){
		desiredHairWeight = newValue;
	}
}
