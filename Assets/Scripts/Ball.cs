using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

	public bool hasLaunched { get; private set; }

    private Rigidbody2D rigidBody2D;
    private AudioSource audioSource;

	private Paddle paddle;
	private LaunchArrow launchArrow;

	private Vector3 paddleToBallVector;
	private Vector3 BallToArrowVector;

    private bool gamePaused = false;
    private Vector2 velocityAtPause;
    private float angularVelocityAtPause;

    private const float minFlickerIntensity = 5.5f;
	private const float maxFlickerIntensity = 7f;
	private const float flickerSpeed = 0.1f;

	// Use this for initialization
	void Start () {
		// Find paddle and launch arrow objects
		paddle = GameObject.FindObjectOfType<Paddle>();
		launchArrow = GameObject.FindObjectOfType<LaunchArrow>();
        rigidBody2D = GetComponent<Rigidbody2D>();


        ResetBall();
		StartCoroutine(FlickerLight());
	}
	
	// Update is called once per frame
	void Update () {
	
		if (!hasLaunched && !gamePaused) {
			// Lock the ball relative to the paddle
			this.transform.position = paddle.transform.position + paddleToBallVector;
			
			launchArrow.transform.position = this.transform.position + BallToArrowVector;
			
			// Launch the ball when the mouse is pressed
			if (Input.GetMouseButtonDown(0)) {
			
				Vector3 arrowDirection = launchArrow.transform.up * 8f;
			
				this.GetComponent<Rigidbody2D>().velocity = arrowDirection;
				
				launchArrow.HideArrow();
				hasLaunched = true;
			}
		}
	}
	
	void OnCollisionEnter2D(Collision2D collision) {
		// Tweak the bounce vector
		Vector2 tweak = new Vector2(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f));
	
		// Play the bounce sound
		if (hasLaunched) {
			GetComponent<AudioSource>().Play();

            rigidBody2D.velocity += tweak;
		}
	}

    void OnPauseGame() {
        velocityAtPause = rigidBody2D.velocity;
        angularVelocityAtPause = rigidBody2D.angularVelocity;
        rigidBody2D.isKinematic = true;
        gamePaused = true;
    }

    void OnResumeGame() {
        rigidBody2D.isKinematic = false;
        rigidBody2D.velocity = velocityAtPause;
        rigidBody2D.angularVelocity = angularVelocityAtPause;
        rigidBody2D.WakeUp();
        gamePaused = false;
    }

    public void ResetBall() {
		// Ball hasn't launched
		hasLaunched = false;
	
		// Set ball position to be just above the paddle
		Vector3 ballPos = new Vector3(paddle.transform.position.x, paddle.transform.position.y + 0.455f, paddle.transform.position.z);
		this.transform.position = ballPos;
		paddleToBallVector = this.transform.position - paddle.transform.position;
		
		// Set the arrow position to be just above the ball
		Vector3 arrowPos = new Vector3(paddle.transform.position.x, paddle.transform.position.y + 0.7f, launchArrow.transform.position.z);
		launchArrow.transform.position = arrowPos;		
		BallToArrowVector = launchArrow.transform.position - this.transform.position;
		
		// Show the launch arrow
		launchArrow.ShowArrow();
	}
	
	private IEnumerator FlickerLight() {
	
		while (true) {
			GetComponent<Light>().intensity = (Random.Range(minFlickerIntensity, maxFlickerIntensity));
			yield return new WaitForSeconds(flickerSpeed);
		}
	}
}
