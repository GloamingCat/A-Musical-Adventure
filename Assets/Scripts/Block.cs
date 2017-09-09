using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour {

	public static bool slice = true;

	public GameObject leftTop;
	public GameObject middleTop;
	public GameObject rightTop;

	public GameObject leftCenter;
	public GameObject middleCenter;
	public GameObject rightCenter;

	public GameObject leftBottom;
	public GameObject middleBottom;
	public GameObject rightBottom;

	void Awake() {
		if (slice)
			Slice ();
	}

	void Slice() {
		CreateBorders();
		SetScale();
	}

	void CreateBorders() {
		Vector3 offset = new Vector3 (0, 0, -1);

		Bounds bounds = GetComponent<SpriteRenderer> ().bounds;
		Vector3 ltPos = new Vector2(bounds.min.x, bounds.max.y);
		Vector3 mtPos = new Vector2(bounds.center.x, bounds.max.y);
		Vector3 rtPos = bounds.max;

		Vector3 lcPos = new Vector2(bounds.min.x, bounds.center.y);
		Vector3 mcPos = new Vector2(bounds.center.x, bounds.center.y);
		Vector3 rcPos = new Vector2(bounds.max.x, bounds.center.y);

		Vector3 lbPos = bounds.min;
		Vector3 mbPos = new Vector2(bounds.center.x, bounds.min.y);
		Vector3 rbPos = new Vector2(bounds.max.x, bounds.min.y);

		leftTop = Instantiate(leftTop);
		leftTop.transform.position = ltPos + offset;

		middleTop = Instantiate(middleTop);
		middleTop.transform.position = mtPos + offset;

		rightTop = Instantiate(rightTop);
		rightTop.transform.position = rtPos + offset;

		leftCenter = Instantiate(leftCenter);
		leftCenter.transform.position = lcPos + offset;

		middleCenter = Instantiate(middleCenter);
		middleCenter.transform.position = mcPos + offset;

		rightCenter = Instantiate(rightCenter);
		rightCenter.transform.position = rcPos + offset;

		leftBottom = Instantiate(leftBottom);
		leftBottom.transform.position = lbPos + offset;

		middleBottom = Instantiate(middleBottom);
		middleBottom.transform.position = mbPos + offset;

		rightBottom = Instantiate(rightBottom);
		rightBottom.transform.position = rbPos + offset;
	}

	void SetScale() {
		Vector2 totalSize = GetComponent<SpriteRenderer> ().bounds.size;
		Vector2 border = leftTop.GetComponent<SpriteRenderer> ().bounds.size;
		Vector2 border1 = leftTop.GetComponent<SpriteRenderer> ().sprite.bounds.size;
		Vector2 size = totalSize - border * 2;

		float borderX = leftTop.transform.localScale.x;
		float borderY = leftTop.transform.localScale.y;

		float scaleX = size.x / border1.x;
		float scaleY = size.y / border1.y;

		middleTop.transform.localScale = new Vector2(scaleX, borderY);
		middleBottom.transform.localScale = new Vector2(scaleX, borderY);
		leftCenter.transform.localScale = new Vector2(borderX, scaleY);
		rightCenter.transform.localScale = new Vector2(borderX, scaleY);
		
		middleCenter.transform.localScale = new Vector2 (scaleX, scaleY);

		Color c = GetComponent<SpriteRenderer> ().color;
		leftTop.GetComponent<SpriteRenderer> ().color = c;
		middleTop.GetComponent<SpriteRenderer> ().color = c;
		rightTop.GetComponent<SpriteRenderer> ().color = c;
		leftCenter.GetComponent<SpriteRenderer> ().color = c;
		middleCenter.GetComponent<SpriteRenderer> ().color = c;
		rightCenter.GetComponent<SpriteRenderer> ().color = c;
		leftBottom.GetComponent<SpriteRenderer> ().color = c;
		middleBottom.GetComponent<SpriteRenderer> ().color = c;
		rightBottom.GetComponent<SpriteRenderer> ().color = c;

		GetComponent<SpriteRenderer> ().sprite = null;
	}

}
