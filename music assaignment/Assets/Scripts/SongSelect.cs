using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongSelect : MonoBehaviour {

	public GameObject buttonPrefab;
	public GameObject gridList;
	public GameObject managerPrefab;

	public List<AudioSource> songs = new List<AudioSource> ();

	void Start () {
		GameObject manager = GameObject.Find ("SongManager(Clone)");

		if (manager == null) {
			Instantiate (managerPrefab);
		}

		foreach (AudioSource song in songs) {
			GameObject selectButton = Instantiate (buttonPrefab, gridList.transform);
			SongButton selectScript = selectButton.GetComponent<SongButton> ();

			selectScript.audioInButton = song;
		}
	}

}
