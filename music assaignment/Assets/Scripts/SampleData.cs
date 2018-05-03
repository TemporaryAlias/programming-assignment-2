using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (AudioSource))]
public class SampleData : MonoBehaviour {

	public static float[] samples = new float[512];

	public static bool songPlaying = true;

	private AudioSource _source;

	void Start () {
		_source = GetComponent<AudioSource> ();

		_source.clip = SongManager.currentSong.clip;
		_source.Play ();
	}

	void Update () {
		_source.GetSpectrumData (samples, 0, FFTWindow.Blackman);

		if (_source.isPlaying) {
			songPlaying = true;
		} else if (!_source.isPlaying) {
			songPlaying = false;
		}
	}

}
