using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteDestroyer2 : MonoBehaviour {

	bool active = false;
	GameObject Notes;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Alpha3) && active) 
		{
			Destroy(Notes);
		}

		if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			GetComponent<Renderer> ().material.color = Color.cyan;

		} 

		if (Input.GetKeyUp(KeyCode.Alpha3))
		{
			GetComponent<Renderer> ().material.color = Color.grey;
		} 
	}

	void OnTriggerEnter(Collider col)
	{
		active = true;
		if(col.gameObject.CompareTag("Notes"))
			Notes = col.gameObject;
	}

	void OnTriggerExit(Collider col)
	{
		active = false;
	}
}
