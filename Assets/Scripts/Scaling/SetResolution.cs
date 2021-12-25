using UnityEngine;
using System.Collections;

public class SetResolution : MonoBehaviour {


	void Start () {
		GetComponent<Camera>().aspect = 2960/1440f;
    }
	void Update()
	{
		GetComponent<Camera>().aspect = 2960/1440f;
	}
	
}
