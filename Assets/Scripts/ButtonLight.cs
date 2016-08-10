using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;

public class ButtonLight : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	public AudioClip lightSound;

	private const float minFlickerIntensity = 3.5f;
	private const float maxFlickerIntensity = 5f;
	private const float flickerSpeed = 0.1f;

	void Start() {
		this.GetComponent<Light>().enabled = false;
		StartCoroutine(FlickerLight());
	}

	public void OnPointerEnter(PointerEventData eventData) {
		this.GetComponent<Light>().enabled = true;
		AudioSource.PlayClipAtPoint(lightSound, Camera.main.transform.position, 1.0f);
	}
	
	public void OnPointerExit(PointerEventData eventData) {
		this.GetComponent<Light>().enabled = false;
	}
	
	private IEnumerator FlickerLight() {
		
		while (true) {
			GetComponent<Light>().intensity = (Random.Range(minFlickerIntensity, maxFlickerIntensity));
			yield return new WaitForSeconds(flickerSpeed);
		}
	}
}
