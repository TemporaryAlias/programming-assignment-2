using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public GameObject target;

	public float speed;
	public float speedMultiplier;

	public bool enemy = true;

	private Renderer _rend;

	void Start () {
		_rend = GetComponent<Renderer> ();

		if (enemy) {
			_rend.material.SetColor ("_Color", Color.red);
		} else {
			_rend.material.SetColor ("_Color", Color.blue);
		}

		if (target == null) {
			target = GameObject.Find ("Player");
		}
	}

	void Update () {
		float allSamples = 0;

		for (int i = 0; i < SampleData.samples.Length; i++) {
			allSamples += SampleData.samples [i];
		}

		speedMultiplier = allSamples;

		float step = speed * speedMultiplier * Time.deltaTime;
		transform.position = Vector3.MoveTowards (transform.position, target.transform.position, step);
	}

}
