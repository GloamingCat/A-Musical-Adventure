using UnityEngine;
using System.Collections;

public class BlockGenerator : MonoBehaviour {

	public GameObject block;

	private GameObject blocks;

	void Start() {
		Block.slice = false;
		blocks = new GameObject();
		blocks.name = "blocks";
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.A)) {
			GenerateBlock();
		}
	}

	void GenerateBlock() {
		GameObject b = Instantiate(block, transform.position, transform.rotation) as GameObject;
		b.transform.SetParent (blocks.transform);
	}
}
