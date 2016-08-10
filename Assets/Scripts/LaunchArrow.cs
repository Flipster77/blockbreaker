using UnityEngine;
using System.Collections;

public class LaunchArrow : MonoBehaviour {

	private float rotateStep = 1f;
	private float currentAngle = 0f;
	
	private const float MIN_ANGLE = -85f;
	private const float MAX_ANGLE = 85f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		//Debug.Log ("Current angle of launch arrow: " + currentAngle);
	
		if (Input.GetKey(KeyCode.LeftArrow) && currentAngle + rotateStep <= MAX_ANGLE) {
			this.transform.Rotate(0f, 0f, rotateStep);
			currentAngle++;
		}
	
		if (Input.GetKey(KeyCode.RightArrow) && currentAngle - rotateStep >= MIN_ANGLE) {
			this.transform.Rotate(0f, 0f, -rotateStep);
			currentAngle--;
		}
	
	}
	
	public void HideArrow() {
		GetComponent<Renderer>().enabled = false;
	}
	
	public void ShowArrow() {
		this.transform.rotation = Quaternion.identity;
		currentAngle = 0f;
		GetComponent<Renderer>().enabled = true;
	}
}
