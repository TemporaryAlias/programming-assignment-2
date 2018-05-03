using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class orbit : MonoBehaviour
{
    private float angle;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {  angle += 1 * Time.deltaTime;
        var hi = new Vector3 (Mathf.Cos(angle), 0, Mathf.Sin(angle))*50;
        gameObject.transform.position += hi * Time.deltaTime;
            
    }
}