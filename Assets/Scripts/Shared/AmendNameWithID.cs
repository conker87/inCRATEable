using UnityEngine;
using System.Collections;

public class AmendNameWithID : MonoBehaviour {

	void Start () {
	
		gameObject.name = gameObject.name + gameObject.GetInstanceID();

	}

}