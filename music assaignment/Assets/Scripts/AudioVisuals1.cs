using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioVisuals1 : MonoBehaviour {

	private const int SAMPLE_SIZE = 1024;

	public float _dBvalue;
	//public float _pitchValue;
	public float _rmsValue;

	public float _maxVisualScale = 25.0f;
	public float _visualMod = 50.0f;
	public float _smoothSpeed = 10.0f;
	public float _keepPercentage = 0.5f;

	private AudioSource source;
	private float[] _mySamples;
	private float[] _spectrum;
	private float _sampRate;

	public Transform[] _vList;
	private float[] _vScale;
	private int _VisualAmount = 64;


	// Use this for initialization
	void Start () {
		source = GetComponent<AudioSource> ();
		source.clip = SongManager.currentSong.clip;
		source.Play ();

		_mySamples = new float[SAMPLE_SIZE];
		_spectrum = new float[SAMPLE_SIZE];
		_sampRate = AudioSettings.outputSampleRate;
		SpawnLine ();
	}

	private void SpawnLine()
	{
		_vScale = new float[_VisualAmount]; 
		_vList = new Transform[_VisualAmount];

		for (int i = 0; i < _VisualAmount; i++) {
			GameObject go = GameObject.CreatePrimitive (PrimitiveType.Cube) as GameObject;
			_vList [i] = go.transform;
			_vList [i].position = Vector3.left * i;
		}
	}
	
	// Update is called once per frame
		private void Update () {
		AnalyzeSound ();
		UpdateVisual ();
	}

	private void UpdateVisual()
	{
		int visualIndex = 0;
		int spectrumIndex = 0;
		int averageSize = (int)((SAMPLE_SIZE * _keepPercentage) / _VisualAmount);

		while (visualIndex < _VisualAmount) {
			int j = 0;
			float sum = 0;
			while (j < averageSize) {
				sum += _spectrum [spectrumIndex];
				spectrumIndex++;
				j++;
			}
		

			float scaleY = sum / averageSize * _visualMod;
			_vScale[visualIndex] -= Time.deltaTime * _smoothSpeed;
			if (_vScale[visualIndex] < scaleY)
				_vScale[visualIndex] = scaleY;

			if (_vScale [visualIndex] > _maxVisualScale)
				_vScale [visualIndex] = _maxVisualScale;

				_vList[visualIndex].localScale = Vector3.one + Vector3.up * _vScale[visualIndex];
				visualIndex++;
		}
	
	}
	private void AnalyzeSound()	{
		source.GetOutputData (_mySamples, 0);

		int i = 0;
		float sum = 0;
		for (; i < SAMPLE_SIZE; i++) {
			sum += _mySamples [i] * _mySamples [i];
		}
		_rmsValue = Mathf.Sqrt (sum / SAMPLE_SIZE);

		_dBvalue = 20 * Mathf.Log10 (_rmsValue / 0.1f);

		source.GetSpectrumData (_spectrum, 0, FFTWindow.BlackmanHarris);
	}
}
