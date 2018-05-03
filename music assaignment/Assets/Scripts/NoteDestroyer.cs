using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteDestroyer : MonoBehaviour {

	bool active = false;
	GameObject Notes;

	void start ()
	{
		
	}

	void Update () {
		if (Input.GetKeyDown(KeyCode.Alpha2) && active) 
		{
			Destroy(Notes);

		}

		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			GetComponent<Renderer> ().material.color = Color.cyan;

		} 

		if (Input.GetKeyUp(KeyCode.Alpha2))
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
