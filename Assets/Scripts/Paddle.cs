using UnityEngine;
using System.Collections;

public class Paddle : MonoBehaviour {

	public bool autoPlay = false;
	public const float MINIMUM_X_POS = 1f;
	public const float MAXIMUM_X_POS = 15f;
	
	private Ball ball;
	
	void Start() {
		ball = GameObject.FindObjectOfType<Ball>();
	}
	
	// Update is called once per frame
	void Update() {
		if (!autoPlay || !ball.hasLaunched) {
			MoveWithMouse();
		} else {
			AutoMove();
		}
	}
	
	void MoveWithMouse() {
		float mousePosInBlocks = Input.mousePosition.x / Screen.width * 16;
		
		// Set the paddle position to the current mouse position in the x axis
		Vector3 paddlePos = new Vector3(Mathf.Clamp(mousePosInBlocks, MINIMUM_X_POS, MAXIMUM_X_POS), this.transform.position.y, this.transform.position.z);
		
		this.transform.position = paddlePos;
	}
	
	void AutoMove() {
		float ballPosInBlocks = ball.transform.position.x;
		
		// Set the paddle position to the current mouse position in the x axis
		Vector3 paddlePos = new Vector3(Mathf.Clamp(ballPosInBlocks, MINIMUM_X_POS, MAXIMUM_X_POS), this.transform.position.y, this.transform.position.z);
		
		this.transform.position = paddlePos;
	}
}
