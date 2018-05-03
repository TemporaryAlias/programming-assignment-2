using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongManager : MonoBehaviour {

	public static AudioSource currentSong;

	void Start () {
		currentSong = GetComponent<AudioSource> ();

		SongSelect songSelect = FindObjectOfType <SongSelect> ();
		currentSong.clip = songSelect.songs [0].clip;

		DontDestroyOnLoad (this.gameObject);
	}

}
