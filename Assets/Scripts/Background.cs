using UnityEngine;
using System.Collections;

public class Background : MonoBehaviour {

	public float colorTime;
	public float changeDuration;
	public Color[] colors;

	float changeValue;
	int index;
	int prox;
	
	void Start () {
		index = 0;
		Camera.main.backgroundColor = colors[index];
		Invoke ("ChangeColor", colorTime);
	}

	void Update() {
		if (changeValue < 1) { 
			Camera.main.backgroundColor = Color.Lerp (colors [index], colors [prox], changeValue);
			changeValue += 1 / (changeDuration * 60);
		}
	}

	void ChangeColor() {
		index = prox;
		if (index == colors.Length - 1)
			prox = 0;
		else
			prox = index + 1;

		changeValue = 0;
		Invoke ("ChangeColor", colorTime + changeDuration);
	}

}
