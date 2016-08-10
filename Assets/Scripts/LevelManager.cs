using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public void LoadLevel(string name) {
		Debug.Log("Level load requested for: " + name);
		Brick.breakableCount = 0;
		SceneManager.LoadScene(name);
	}
	
	public void LoadNextLevel() {
		Brick.breakableCount = 0;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        Debug.Log("Loading level " + (currentSceneIndex+1).ToString());
        SceneManager.LoadScene(currentSceneIndex+1);
	}
	
	public void QuitRequest() {
		Debug.Log("Quit requested");
		Application.Quit();
	}
	
	public void BrickDestroyed() {
		if (Brick.breakableCount <= 0) {
			StartCoroutine(WaitThenLoadNextLevel());
		}
	}
	
	public void NewGame() {
		Lives.ResetLives();
		LoadLevel("Level_01");
	}
	
	public void GameOver() {
		LoadLevel("Lose_Screen");
	}
	
	private IEnumerator WaitThenLoadNextLevel() {
		yield return new WaitForSeconds(1);
		LoadNextLevel();
	}
}
