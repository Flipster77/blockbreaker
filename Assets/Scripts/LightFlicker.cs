using UnityEngine;
using System.Collections;

public class LightFlicker : MonoBehaviour {

	public float minFlickerIntensity = 3.5f;
	public float maxFlickerIntensity = 5f;
	public float flickerSpeed = 0.1f;
	
	public bool moveWithMouse = false;

	void Start() {
		StartCoroutine(FlickerLight());
	}
	
	void Update() {
		if (moveWithMouse) {
			MoveWithMouse();
		}
	}
	
	void MoveWithMouse() {
		float mouseXPosInBlocks = Input.mousePosition.x / Screen.width * 16;
		float mouseYPosInBlocks = Input.mousePosition.y / Screen.height * 12;
		
		// Set the light position to the current mouse position
		Vector3 lightPos = new Vector3(mouseXPosInBlocks, mouseYPosInBlocks, this.transform.position.z);
		
		this.transform.position = lightPos;
	}
	
	private IEnumerator FlickerLight() {
		
		while (true) {
			GetComponent<Light>().intensity = (Random.Range(minFlickerIntensity, maxFlickerIntensity));
			yield return new WaitForSeconds(flickerSpeed);
		}
	}
}
