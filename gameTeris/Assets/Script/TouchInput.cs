using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(UnityEngine.EventSystems.EventTrigger))]
public class TouchInput : MonoBehaviour {
	public static bool left=false;
	public static bool right=false;
	public static bool down=false;
	public static bool RotateL=false;
	public static bool RotateR=false;
	// Use this for initialization
	void Start () {
		Button btn = this.GetComponent<Button> ();
		EventTrigger trigger = btn.gameObject.GetComponent<EventTrigger> ();
		EventTrigger.Entry entry = new EventTrigger.Entry ();
		entry.eventID = EventTriggerType.PointerDown;
		entry.callback = new EventTrigger.TriggerEvent ();
		entry.callback.AddListener (OnClick);

		EventTrigger.Entry entry2 = new EventTrigger.Entry ();
		entry2.eventID = EventTriggerType.PointerUp;
		entry2.callback = new EventTrigger.TriggerEvent ();
		entry2.callback.AddListener (OnExit);

		trigger.triggers.Add (entry);
		trigger.triggers.Add (entry2);
	}
	private  void OnClick(BaseEventData pointData){
		if(this.name=="left"){
			left = true;
		}
		if(this.name=="right"){
			right = true;
		}
		if(this.name=="down"){
			down = true;
		}
		if(this.name=="RotateL"){
			RotateL = true;
		}
		if(this.name=="RotateR"){
			RotateR = true;
		}
	}

	private  void OnExit(BaseEventData pointData){
		if(this.name=="left"){
			left = false;
		}
		if(this.name=="right"){
			right = false;
		}
		if(this.name=="down"){
			down = false;
		}
		if(this.name=="RotateL"){
			RotateL = false;
		}
		if(this.name=="RotateR"){
			RotateR = false;
		}
	}
	// Update is called once per frame
	void Update () {
		
	}
}
