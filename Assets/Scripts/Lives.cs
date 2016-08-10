using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Lives : MonoBehaviour {

	public const int START_NUM_OF_LIVES = 3;

	public static int numOfLives = START_NUM_OF_LIVES;
	
	private Text displayNumber;

	// Use this for initialization
	void Start () {
		displayNumber = GetComponent<Text>();
		displayNumber.text = numOfLives.ToString();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void GainedLife() {
		numOfLives++;
		displayNumber.text = numOfLives.ToString();
	}
	
	// Returns true if there are still lives left
	public bool LostLife() {
		numOfLives--;
		displayNumber.text = numOfLives.ToString();
		
		if (numOfLives > 0) {
			return true;
		} else {
			return false;
		}
	}
	
	public static void ResetLives() {
		numOfLives = START_NUM_OF_LIVES;
	}
}
