using UnityEngine;
using System.Collections;

public class Brick : MonoBehaviour {

	public static int breakableCount = 0;
	public AudioClip breakSound;
	public AudioClip hitSound;
	public Sprite[] hitSprites;
	public GameObject smoke;
	
	private int timesHit;
	private LevelManager levelManager;
	private bool isBreakable;

	// Use this for initialization
	void Start () {
		isBreakable = (this.tag == "Breakable");
		
		if (isBreakable) {
			breakableCount++;
		}
	
		timesHit = 0;
		levelManager = GameObject.FindObjectOfType<LevelManager>();
	}
	
	void OnCollisionEnter2D(Collision2D collision) {
		
		if (isBreakable) {
			HandleHits();
		}
	}
	
	void HandleHits() {
		timesHit++;
		
		if (timesHit >= hitSprites.Length+1) {
			BrickDestroyed();
		} else {
			// Play the hit sound
			AudioSource.PlayClipAtPoint(hitSound, Camera.main.transform.position, 1.0f);
			LoadSprites();
		}
	}
	
	protected virtual void BrickDestroyed() {
		// Play the breaking sound
		AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position, 1.0f);
	
		breakableCount--;
		
		levelManager.BrickDestroyed();
		PuffSmoke();
		
		Destroy(gameObject);
	}
	
	protected void PuffSmoke() {
		GameObject smokePuff = Instantiate(smoke, transform.position, Quaternion.identity) as GameObject;
		smokePuff.GetComponent<ParticleSystem>().startColor = this.GetComponent<SpriteRenderer>().color;
		
		float smokeLifetime = smokePuff.GetComponent<ParticleSystem>().duration + smokePuff.GetComponent<ParticleSystem>().startLifetime;
		
		Destroy(smokePuff, smokeLifetime);
	}
	
	private void LoadSprites() {
		int spriteIndex = timesHit - 1;
		
		if (hitSprites[spriteIndex] != null) {
			this.GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
		} else {
			Debug.LogError("No brick sprite found at index " + spriteIndex);
		}
	}
}
