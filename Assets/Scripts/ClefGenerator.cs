using UnityEngine;
using System.Collections;

public class ClefGenerator : MonoBehaviour {
	
	public GameObject clef;
	
	private GameObject clefs;
	
	void Start() {
		clefs = new GameObject();
		clefs.name = "clefs";
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.X)) {
			Invoke("GenerateClef", 0.1f);
		}
		if (Input.GetKeyDown(KeyCode.Delete)) {
			GenerateClef();
		}
	}
	
	void GenerateClef() {
		GameObject b = Instantiate(clef, transform.position, transform.rotation) as GameObject;
		b.transform.SetParent (clefs.transform);
	}
}