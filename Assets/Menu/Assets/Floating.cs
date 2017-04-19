using UnityEngine;
using System.Collections;

public class Floating : MonoBehaviour {

	public float maxUpAndDown            = 2;             // amount of meters going up and down
	public float speed                   = 300;            // up and down speed
	protected float angle         = 0;            // angle to determin the height by using the sinus
	protected float toDegrees     = Mathf.PI/180;    // radians to degrees

	void Update() {
		angle += speed * Time.deltaTime;
		if (angle > 360) angle -= 360;
		transform.position =
			new Vector3(
				transform.position.x,
				(maxUpAndDown * Mathf.Sin(angle * toDegrees)),
				transform.position.z);
	}
}
