using UnityEngine;
using System.Collections;

public class ExtraLife : MonoBehaviour {

	public GameObject collectEffect;
	public AudioClip collectSound;

	private Lives lives;
	private Paddle paddle;
	private LoseCollider loseCollider;

	// Use this for initialization
	void Start () {
		lives = GameObject.FindObjectOfType<Lives>();
		paddle = GameObject.FindObjectOfType<Paddle>();
		loseCollider = GameObject.FindObjectOfType<LoseCollider>();
	}
	
	void OnTriggerEnter2D(Collider2D collider) {
		// Hit the paddle, gain a life
		if (collider.gameObject == paddle.gameObject) {
			lives.GainedLife();
			CollectEffect();
			Destroy(gameObject);
		}
		
		// Hit the boundary, destroy the extra life
		else if (collider.gameObject == loseCollider.gameObject) {
			Destroy(gameObject);
		}
	}
	
	void CollectEffect() {
		// Play the collect sound
		AudioSource.PlayClipAtPoint(collectSound, Camera.main.transform.position, 0.3f);
	
		GameObject starEffect = Instantiate(collectEffect, transform.position, Quaternion.identity) as GameObject;
		
		float smokeLifetime = starEffect.GetComponent<ParticleSystem>().duration + starEffect.GetComponent<ParticleSystem>().startLifetime;
		
		Destroy(starEffect, smokeLifetime);
	}
}
