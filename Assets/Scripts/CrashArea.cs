using UnityEngine;
using System.Collections;

public class CrashArea : MonoBehaviour {

	void OnTriggernEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			other.gameObject.GetComponent<Player> ().Die ();
		}
	}

	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag == "Player") {
			other.gameObject.GetComponent<Player> ().Die ();
			GetComponent<BoxCollider2D>().isTrigger = true;
		}
	}
}
