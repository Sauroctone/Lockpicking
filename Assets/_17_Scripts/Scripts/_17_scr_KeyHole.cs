using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _17_scr_KeyHole : MonoBehaviour {

	void OnMouseDown()
	{
		Camera.main.GetComponent<_17_scr_TimeManager> ().StartTimer ();
		gameObject.SetActive (false);
	}
}
