using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFB_DemoFlyingEye : MonoBehaviour {

	public Animator animator;
	public GameObject[] beamObjects;
	public GameObject airSuck;
	public GameObject projectile;
	public Transform spawnPos;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
	}
	
	public void Locomotion(float newValue){
		animator.SetFloat ("locomotion", newValue);
	}

	public void AlertLevel(float newValue){
		animator.SetFloat ("alertLevel", newValue);
	}

	public void BeamStart(){
		for (int i = 0; i < beamObjects.Length; i++){
			if (beamObjects [i].GetComponent<Light> ()) {
				beamObjects [i].GetComponent<Light> ().enabled = true;
			} else if (beamObjects[i].GetComponent<ParticleSystem>()){
				beamObjects[i].GetComponent<ParticleSystem>().Play();
			}
		}
	}

	public void BeamStop(){
		for (int i = 0; i < beamObjects.Length; i++){
			if (beamObjects [i].GetComponent<Light> ()) {
				beamObjects [i].GetComponent<Light> ().enabled = true;
			} else if (beamObjects[i].GetComponent<ParticleSystem>()){
				beamObjects[i].GetComponent<ParticleSystem>().Stop();
			}
		}
	}

	public void AirSuck(){
		GameObject newObject = Instantiate(airSuck, spawnPos.position, Quaternion.identity);
		newObject.transform.parent = spawnPos.transform;
		Destroy (newObject, 5.0f);
	}

	public void Projectile(){
		GameObject newObject = Instantiate(projectile, spawnPos.position, Quaternion.identity);
		newObject.transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
		Destroy (newObject, 7.0f);
	}
}
