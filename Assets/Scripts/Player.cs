using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
	
	public static string levelToLoad;
	public static int charType;
	public float runSpeed;
	public float jumpSpeed;
	public float jumpError;
	public float maxJumpIncrease;
	public GameObject death;
	public RuntimeAnimatorController[] controllers;

	Animator animator;
	Rigidbody2D rb2D;
	BoxCollider2D box;
	AudioSource music;
	Text timeText;
	GameObject pauseHUD;
	Text scoreText;
	Text bestScoreText;

	int score;
	int time;
	float jumpCount;

	void Start() {
		animator = GetComponent<Animator>();
		rb2D = GetComponent<Rigidbody2D>();
		box = GetComponent<BoxCollider2D> ();
		music = GetComponent<AudioSource> ();
		rb2D.velocity = new Vector2(runSpeed, 0);
		animator.runtimeAnimatorController = controllers [charType];

		pauseHUD = GameObject.FindGameObjectWithTag ("pauseHUD");
		timeText = GameObject.FindGameObjectWithTag ("time").GetComponent<Text> ();
		scoreText = GameObject.FindGameObjectWithTag ("player score").GetComponent<Text> ();
		bestScoreText = GameObject.FindGameObjectWithTag ("best score").GetComponent<Text> ();
		bestScoreText.text = PlayerPrefs.GetInt("best score " + levelToLoad).ToString();
		GameObject.FindGameObjectWithTag ("deaths").GetComponent<Text>().text = PlayerPrefs.GetInt("deaths " + levelToLoad).ToString();
		music.time = (transform.position.x - 750) / runSpeed;
		music.Play();
		pauseHUD.SetActive (false);
		score = 0;
		time = 0;
		jumpCount = 0;

		Invoke ("UpdateTime", 1);
	}

	void Update () {
		if (animator.GetBool ("dead") || animator.GetBool ("win")) {
			if (Input.GetKeyDown (KeyCode.C))
				Restart ();
			if (Input.GetKeyDown (KeyCode.V))
				Exit ();
		} else {
			if (!isPaused()) {
				rb2D.velocity = new Vector2 (runSpeed, rb2D.velocity.y);
				UpdateJump ();

				if (Input.GetKeyDown (KeyCode.Z) || Input.GetMouseButtonDown (0))
					Jump ();
				else if (Input.GetKeyDown (KeyCode.X) || Input.GetMouseButtonDown (1))
					Slide();
				if (Input.GetKeyUp (KeyCode.X) || Input.GetMouseButtonUp (1))
					ReleaseSlide ();
			}

			if (Input.GetKeyDown(KeyCode.Escape)) {
				if (isPaused())
					Resume();
				else
					Pause();
			}
		}
	}
	
	void Pause() {
		music.Pause ();
		pauseHUD.SetActive(true);
		Time.timeScale = 0;
	}

	void Resume() {
		music.UnPause ();
		pauseHUD.SetActive(false);
		Time.timeScale = 1;
	}

	bool isPaused() {
		return Time.timeScale == 0;
	}

	void UpdateTime() {
		time++;
		timeText.text = (time / 60).ToString() + ":" + ((time % 60) / 10).ToString() + ((time % 60) % 10).ToString();
		Invoke ("UpdateTime", 1);
	}

	// =====================================================================
	// 	Jump
	// =====================================================================

	void UpdateJump() {
		if (jumpCount > 0) {
			if (Input.GetKey (KeyCode.Z) || Input.GetMouseButton (0)) {
				rb2D.velocity = new Vector2(runSpeed, jumpSpeed);
				jumpCount -= 60 * Time.deltaTime;
			} else 
				jumpCount = 0;
		}
		if (rb2D.velocity.y > jumpError) {
			animator.SetBool ("jumping", true);
			animator.SetBool ("falling", false);
		} else if (rb2D.velocity.y < -jumpError) {
			animator.SetBool ("jumping", true);
			animator.SetBool ("falling", true);
		}
		else {
			animator.SetBool ("jumping", false);
			animator.SetBool ("falling", true);
		}
	}

	void Jump() {
		if (!IsJumping() && !IsSliding()) {
			jumpCount = maxJumpIncrease;
			rb2D.velocity = new Vector2(runSpeed, jumpSpeed);
		}
	}

	bool IsJumping() {
		return Mathf.Abs(rb2D.velocity.y) > jumpError;
	}

	// =====================================================================
	// 	Slide
	// =====================================================================

	void Slide() {
		if (!IsJumping()) {
			animator.SetBool ("sliding", true);
			box.size = new Vector2 (box.size.x, 0.8f);
			box.offset = new Vector2 (box.offset.x, -0.2f);
		}
	}

	void ReleaseSlide() {
		animator.SetBool ("sliding", false);
		box.size = new Vector2 (box.size.x, 1.2f);
		box.offset = new Vector2 (box.offset.x, 0);
	}

	bool IsSliding() {
		return animator.GetBool ("sliding");
	}

	// =====================================================================
	// 	Interaction with game objects
	// =====================================================================

	public void Die() {
		if (animator.GetBool ("dead"))
			return;
		GameObject d = Instantiate (death, transform.position, transform.rotation) as GameObject;
		Destroy (d, 0.5f);
		animator.SetBool ("dead", true);
		music.Pause();
		rb2D.velocity = Vector2.zero;
		pauseHUD.SetActive(true);

		int deaths = PlayerPrefs.GetInt ("deaths " + levelToLoad) + 1;
		PlayerPrefs.SetInt ("deaths " + levelToLoad, deaths);
		GameObject.FindGameObjectWithTag ("deaths").GetComponent<Text>().text = deaths.ToString();
		CancelInvoke ("UpdateTime");
	}

	public void IncrementScore() {
		score++;
		int best = PlayerPrefs.GetInt ("best score " + levelToLoad);
		if (score > best) {
			best = score;
			PlayerPrefs.SetInt ("best score " + levelToLoad, best);
		}
		scoreText.text = score.ToString();
		bestScoreText.text = best.ToString();
	}

	public void Win() {
		if (animator.GetBool ("win"))
			return;
		animator.SetBool ("win", true);
		rb2D.velocity = Vector2.zero;
		CancelInvoke ("UpdateTime");
		pauseHUD.SetActive(true);
	}

	// =====================================================================
	// 	Interaction with other scenes
	// =====================================================================

	static int numberOfStages = 3;

	public void setChar (int i) {
		charType = i;
	}
	
	public void setLevel (string s) {
		levelToLoad = s;
	}

	public void StartLevel() {
		Time.timeScale = 1;
		SceneManager.LoadScene (levelToLoad);
	}
	
	public void Restart() {
		Time.timeScale = 1;
		SceneManager.LoadScene (SceneManager.GetActiveScene().name);
	}
	
	public void Exit() {
		Time.timeScale = 1;
		SceneManager.LoadScene ("chooseStage");
	}

	public void ResetValues() {
		for (int i = 0; i < numberOfStages; i++) {
			PlayerPrefs.SetInt ("best score stage" + i.ToString(), 0);
			PlayerPrefs.SetInt ("deaths stage" + i.ToString(), 0);
		}
	}

}
