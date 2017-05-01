using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Tetris : MonoBehaviour {
	public static bool isTouched=true;//if the block has touch other block or the bottomPlane
	public GameObject[] BlockPfb=new GameObject[7];
	public GameObject end;
	public Text scoreText;
	private int scoreNum=0;
	int frame=0;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		frame++;
		//Debug.Log (1.0f/Time.deltaTime);
		if (BLockControl.lineTag) {
			for (float i = -7.5f; i <= 11.5f; i++) {
				bool tag = true;
				for (float j = -4.5f; j <= 4.5f; j++) {
					if (!Physics.Raycast (new Vector3 (j, i, 5.0f), new Vector3 (0.0f, 0.0f, -1.0f), 20.0f, 1 << LayerMask.NameToLayer ("Block"))) {
						tag = false;
					}
				}
				if (tag) {
					RaycastHit rayHit;
					for (float j = -4.5f; j <= 4.5f; j++) {
						if (Physics.Raycast (new Vector3 (j, i, 5.0f), new Vector3 (0.0f, 0.0f, -1.0f), out rayHit, 20.0f, 1 << LayerMask.NameToLayer ("Block"))) {
							foreach (Transform child in rayHit.transform) {
								child.gameObject.AddComponent<Rigidbody> ();
								child.GetComponent<Rigidbody> ().collisionDetectionMode = CollisionDetectionMode.Continuous;
							}
							if (rayHit.transform.parent) {//需要判断父物体是否存在，不判断可能导致重复操作。
								Transform parent = rayHit.transform.parent;
								parent.DetachChildren ();
								Destroy (parent.gameObject);
							}
							Destroy (rayHit.transform.gameObject);
						}
					}
					scoreNum++;
					scoreText.text = scoreNum.ToString ("000");
				}
			}
		}
		RaycastHit hit;
		if (!BLockControl.endTag&&Physics.Raycast (new Vector3 (-6.0f, 14.0f, 1.0f), new Vector3 (1.0f, 0.0f, 0.0f),out hit, 30.0f, 1 << LayerMask.NameToLayer ("Block"))&&!hit.transform.GetComponent<BLockControl>()) {
			Tetris.isTouched = false;
			BLockControl.endTag = true;
			Debug.Log ("Game Over");
		}
		if(isTouched&&!BLockControl.endTag){
			int ran = (int) Random.Range (0.0f, 7.0f);
			Vector3 pos = new Vector3 (-1.0f, 14f, 1.0f);
			Instantiate (BlockPfb[ran],pos,BlockPfb[ran].transform.rotation);
			isTouched = false;
		}
		if(BLockControl.endTag){
			end.SetActive (true);
		}

	}
}
