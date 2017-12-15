using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class _17_scr_CollisionWithWalls : MonoBehaviour {

	_17_scr_TimeManager manager;

	void Start()
	{
		manager = Camera.main.GetComponent<_17_scr_TimeManager> ();
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.GetComponent<_17_tag_Wall> () != null && manager.state == GameState.Ticking) 
		{
			manager.Lose ();
		}
	}
}
