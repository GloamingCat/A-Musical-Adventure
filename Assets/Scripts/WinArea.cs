using UnityEngine;
using System.Collections;

public class WinArea : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag == "Player")
			other.gameObject.GetComponent<Player>().Win();
	}

}
