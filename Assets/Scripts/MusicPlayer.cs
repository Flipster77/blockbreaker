using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour {

	private static MusicPlayer instance = null;

	void Awake () {
		
		if (instance != null) {
			GameObject.Destroy(gameObject);
		} else {
			instance = this;
			GameObject.DontDestroyOnLoad(gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
