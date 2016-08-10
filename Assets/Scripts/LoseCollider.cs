using UnityEngine;
using System.Collections;

public class LoseCollider : MonoBehaviour {

	private Lives lives;
	private Ball ball;
	private LevelManager levelManager;
	
	void Start() {
		ball = GameObject.FindObjectOfType<Ball>();
		lives = GameObject.FindObjectOfType<Lives>();
		levelManager = GameObject.FindObjectOfType<LevelManager>();
	}

	void OnTriggerEnter2D(Collider2D collider) {
		
		if (collider.gameObject == ball.gameObject) {
			this.LostLife();
		}
	}
	
	private void LostLife() {
		bool stillLivesLeft = lives.LostLife();
		
		if (stillLivesLeft) {
			ball.ResetBall();
		} else {
			levelManager.GameOver();
			Lives.ResetLives();
		}
	}
}
