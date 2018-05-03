using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notes : MonoBehaviour {

	Rigidbody _myRB;
	public float _speed;


	void Awake()
	{
		_myRB = GetComponent<Rigidbody> ();
	}

	void Start () 
	{
		_myRB.velocity = new Vector3(0, 0, - _speed);
	}
	
	// Update is called once per frame
	void Update () {

	}
}
