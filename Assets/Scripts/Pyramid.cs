using UnityEngine;
using System.Collections;

public class Pyramid : Brick {

	public GameObject extraLife;

	protected override void BrickDestroyed() {
		base.BrickDestroyed();
		
		Vector3 newLifePos = new Vector3(this.transform.position.x, this.transform.position.y, -1);
		GameObject newLife = Instantiate(extraLife, newLifePos, Quaternion.identity) as GameObject;
	}
	
}
