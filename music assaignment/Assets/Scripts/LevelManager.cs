using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

	public static int score;

	public float minSpawn;
	public float spawnMultiplier;

	public GameObject bulletPrefab;

	public Text scoreText;

	public List<GameObject> spawnPoints = new List<GameObject>();

	private bool _reset;

	private float _timer;

	void Start () {
		score = 0;
	}
		
	void Update () {
		float spawnRate = minSpawn;
		float allSamples = 0;

		if (SampleData.songPlaying) {
			if (_reset) {
				score = 0;
				_reset = false;
			}

			for (int i = 0; i < SampleData.samples.Length; i++) {
				allSamples += SampleData.samples [i];
			}

			spawnMultiplier = allSamples;

			spawnRate = minSpawn - spawnMultiplier;

			scoreText.text = "" + score;

			if (_timer < spawnRate) {
				_timer += Time.deltaTime;
			} else {
				SpawnBullet ();
				_timer = 0;
			}
		} else {
			Debug.Log ("Song over");
			scoreText.text = "Final Score: " + score;
		}

		if (Input.GetKeyDown(KeyCode.Escape)) {
			SceneChanger sceneChanger = GetComponent<SceneChanger> ();

			sceneChanger.LoadSceneByInt (0);
		}
	}

	void SpawnBullet() {
		int spawnPointNum = spawnPoints.Count;
		int spawning = Random.Range (0, spawnPointNum);

		int isEnemy = Random.Range (1, 3);

		GameObject newObject = Instantiate (bulletPrefab, spawnPoints[spawning].transform);
		Bullet newBullet = newObject.GetComponent<Bullet> ();

		if (isEnemy == 1) {
			newBullet.enemy = true;
		} else if (isEnemy == 2) {
			newBullet.enemy = false;
		} else {
			Debug.Log ("Error on isEnemy, defaulting to true");
			newBullet.enemy = true;
		}
	}

}
