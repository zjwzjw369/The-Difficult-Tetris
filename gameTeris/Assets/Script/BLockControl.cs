using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BLockControl : MonoBehaviour {
	bool isDPressed=false;
	public bool delayTag=false;
	static public bool lineTag = true;
	static public bool endTag = false;
	// Use this for initialization
	void Start () {
		lineTag = false;
	}
	
	// Update is called once per frame
	void Update () {
	}
	void FixedUpdate(){
		
		if (!isDPressed) {
			Rigidbody r = GetComponent<Rigidbody> ();
			transform.Translate (Vector3.down * Time.deltaTime*3.0f, Space.World);
			if (Input.GetKey (KeyCode.A)) {
				r.AddForce (Vector3.left * 10);
			}
			if (Input.GetKey (KeyCode.D)) {
				r.AddForce (Vector3.right * 10);
			}
			if (Input.GetKeyDown (KeyCode.S)) {
				r.constraints = RigidbodyConstraints.None;
				r.velocity = new Vector3 (0.0f, -40.0f, 0.0f);
				r.useGravity = true;
				isDPressed = true;
			}
			if (Input.GetKey (KeyCode.J)) {
				transform.Rotate (new Vector3 (0.0f, 0.0f, 1.0f), Space.World);
			}
			if (Input.GetKey (KeyCode.K)) {
				transform.Rotate (new Vector3 (0.0f, 0.0f, -1.0f), Space.World);
			}

			if (TouchInput.left) {
				r.AddForce (Vector3.left * 10);
			}
			if (TouchInput.right) {
				r.AddForce (Vector3.right * 10);
			}
			if (TouchInput.down) {
				r.constraints = RigidbodyConstraints.None;
				r.velocity = new Vector3 (0.0f, -40.0f, 0.0f);
				r.useGravity = true;
				isDPressed = true;
			}
			if (TouchInput.RotateL) {
				transform.Rotate (new Vector3 (0.0f, 0.0f, 5.0f), Space.World);
			}
			if (TouchInput.RotateR) {
				transform.Rotate (new Vector3 (0.0f, 0.0f, -5.0f), Space.World);
			}
		}
	}
	void OnCollisionEnter(Collision collision) {
		if (!delayTag) {
			if (collision.collider.tag == "stop") {
				delayTag = true;
				lineTag = false;
				StartCoroutine (delay ());
			}
		}
	}
	IEnumerator delay(){
		GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.None;//协程还是有问题
		yield return new WaitForSeconds (0.2f);
		Tetris.isTouched = true;
		/*
		if (Physics.Raycast (new Vector3 (-6.0f, 15.6f, 1.0f), new Vector3 (1.0f, 0.0f, 0.0f), 30.0f, 1 << LayerMask.NameToLayer ("Block"))) {
			Tetris.isTouched = false;
			endTag = true;
			Debug.Log ("Game Over");
		}*/
		GetComponent<Rigidbody> ().useGravity = true;
		lineTag = true;
		Destroy (GetComponent<BLockControl> ());
	}
}
