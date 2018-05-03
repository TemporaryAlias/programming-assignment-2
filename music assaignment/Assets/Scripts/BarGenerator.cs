using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarGenerator : MonoBehaviour {

	public float maxScale;

	public GameObject barPrefab;

	private GameObject[] _bars = new GameObject[100];

	void Start () {
		for (int i = 0; i < _bars.Length; i++) {
			GameObject sampleCube = Instantiate (barPrefab, gameObject.transform);

			sampleCube.transform.position = gameObject.transform.position;
			transform.eulerAngles = new Vector3 (0, -3.6f * i, 0);
			sampleCube.transform.position = Vector3.forward * 25;

			_bars [i] = sampleCube;
		}

		gameObject.transform.Rotate(new Vector3(90, 0, 3.6f));
	}

	void Update () {
		for (int i = 0; i < _bars.Length; i++) {
			_bars [i].transform.localScale = new Vector3 (0.75f, (SampleData.samples[i] * maxScale) + 2, 0.75f);
		}
	}

}
