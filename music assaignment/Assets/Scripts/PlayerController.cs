using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public Text scoreText;

	public GameObject popEffect;

	public float rotateSpeed = 1;
	public float speedMultiplier;

	private Color alphaRed, alphaBlue;

	void Start () {
		alphaRed = Color.red;
		alphaRed.a = 0.2f;

		alphaBlue = Color.blue;
		alphaBlue.a = 0.6f;
	}

	void Update () {
		if (SampleData.songPlaying) {
			if (Input.GetMouseButtonDown (0)) {
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				RaycastHit hit;

				Physics.Raycast (ray, out hit);
				if (hit.collider != null) {
					if (hit.collider.gameObject.CompareTag ("Bullet")) {
						Bullet bullet = hit.collider.gameObject.GetComponent<Bullet> ();

						if (bullet.enemy) {
							LevelManager.score += 1;
							scoreText.color = alphaBlue;
						} else {
							LevelManager.score -= 1;
							scoreText.color = alphaRed;
						}

						Instantiate (popEffect, hit.collider.gameObject.transform.position, hit.collider.gameObject.transform.rotation);
						Destroy (hit.collider.gameObject);
					}
				}
			}

			float allSamples = 0;

			for (int i = 0; i < SampleData.samples.Length; i++) {
				allSamples += SampleData.samples [i];
			}

			speedMultiplier = allSamples;

			transform.RotateAround (transform.position, Vector3.forward, rotateSpeed * speedMultiplier);
		}
	}

	void OnTriggerEnter(Collider col) {
		if (col.gameObject.CompareTag("Bullet")) {
			Bullet bullet = col.gameObject.GetComponent<Bullet> ();

			if (bullet.enemy) {
				LevelManager.score -= 1;
				scoreText.color = alphaRed;
			} else {
				LevelManager.score += 1;
				scoreText.color = alphaBlue;
			}

			Destroy (col.gameObject);
		}
	}

}
