using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteGenerator : MonoBehaviour {

	List<float> noteList = new List<float>() {1, 2, 3, 4, 1, 3, 3, 3, 3, 2, 3, 2, 2, 2, 3, 4, 4, 1, 4, 1, 2, 3, 2, 3, 4, 1, 4, 4, 1, 4, 1, 2, 3, 2, 3, 4, 1, 4, 1, 3, 2, 4 ,1 , 2, 4, 1, 1, 4, 2, 3, 2, 3, 3, 2, 1, 4, 4, 1, 3, 2, 2, 4, 4, 1, 1, 2, 4, 3, 1, 1, 2, 3, 4, 1, 3, 3, 3, 3, 2, 3, 2, 2, 2, 3, 4, 4, 1, 4, 1, 2, 3, 2, 3, 4, 1, 4, 4, 1, 4, 1, 2, 3, 2, 3, 4, 1, 4, 1, 3, 2, 4 ,1 , 2, 4, 1, 1, 4, 2, 3, 2, 3, 3, 2, 1, 4, 4, 1, 3, 2, 2, 4, 4, 1, 1, 2, 4, 3, 1};

	public int notePos = 0;

	public Transform NotesObj;

	public GameObject songHolder;

	public string timerReset = "y";

	public float xPos;

	private AudioSource _source;

	void Start () {
		_source = songHolder.GetComponent<AudioSource> ();
	}
	// Update is called once per frame
	void Update () {
		if (_source.isPlaying) {
			if (timerReset == "y") {
				StartCoroutine (SpawnNotes ());
				timerReset = "n";
			}
		}
	}

	IEnumerator SpawnNotes()
	{
		yield return new WaitForSeconds (1);

		int notePlace = Random.Range (1, 5);

		if (notePlace == 1) {
			xPos = -6f;
		}

		if (notePlace == 2) {
			xPos = -2f;
		}
		if (notePlace == 3) {
			xPos = 2f;
		}
		if (notePlace == 4) {
			xPos = 6f;
		}

		//notePos += 1;
		timerReset = "y";
		Instantiate (NotesObj, new Vector3 (xPos, -1.5f, -10f), NotesObj.rotation);
	}


}
