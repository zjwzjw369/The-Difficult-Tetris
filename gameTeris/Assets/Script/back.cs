using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class back : MonoBehaviour {
    public Text text;
    int frame = 0;
	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
	}
    public void onClick() {
        StartCoroutine(loading());
    }

    IEnumerator loading() {
        frame++;
        while(frame % 16 != 0) {
            text.color = new Color(text.color.r, text.color.g, text.color.b,  1.0f * Mathf.Sin(frame/10f));
            yield return 0;
            frame++;
        }
        while (frame % 16 != 0){
            yield return 0;
            frame++;
		}
		frame = 0;
        SceneManager.LoadScene(0);
		Tetris.isTouched = true;
		BLockControl.endTag = false;
    }
}
