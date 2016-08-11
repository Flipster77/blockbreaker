using UnityEngine;
using System.Collections;

public class ExtraLife : MonoBehaviour {

	public GameObject collectEffect;
	public AudioClip collectSound;

    private Rigidbody2D rigidBody2D;

    private Lives lives;
	private Paddle paddle;
	private LoseCollider loseCollider;

    private Vector2 velocityAtPause;
    private float angularVelocityAtPause;

    // Use this for initialization
    void Start () {
		lives = GameObject.FindObjectOfType<Lives>();
		paddle = GameObject.FindObjectOfType<Paddle>();
		loseCollider = GameObject.FindObjectOfType<LoseCollider>();
        rigidBody2D = GetComponent<Rigidbody2D>();
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

    void OnPauseGame() {
        velocityAtPause = rigidBody2D.velocity;
        angularVelocityAtPause = rigidBody2D.angularVelocity;
        rigidBody2D.isKinematic = true;
    }

    void OnResumeGame() {
        rigidBody2D.isKinematic = false;
        rigidBody2D.velocity = velocityAtPause;
        rigidBody2D.angularVelocity = angularVelocityAtPause;
        rigidBody2D.WakeUp();
    }
}
