using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SongButton : MonoBehaviour {

	public AudioSource audioInButton;

	private Button _button;

	private Text _buttonText;

	void Start () {
		_button = GetComponent<Button> ();
		_buttonText = GetComponentInChildren<Text> ();

		_button.onClick.AddListener (OnClicked);
	}
		
	void Update () {
		_buttonText.text = audioInButton.name;
	}

	void OnClicked() {
		SongManager.currentSong.clip = audioInButton.clip;
	}

}
