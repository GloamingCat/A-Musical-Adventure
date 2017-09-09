using UnityEngine;
using System.Collections;

public class Clef : MonoBehaviour {

	public GameObject catchEffect;

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.name == "Player") {
			other.gameObject.GetComponent<Player>().IncrementScore();
			GetComponent<SpriteRenderer>().sprite = null;
			GameObject e = Instantiate(catchEffect, transform.position, transform.rotation) as GameObject;
			Destroy (e, 0.2f);
			Destroy (gameObject, 0.25f);
		}
	}
}
