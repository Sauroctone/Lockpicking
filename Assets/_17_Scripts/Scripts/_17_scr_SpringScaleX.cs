using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _17_scr_SpringScaleX : MonoBehaviour {

	Transform tumbler;
	Vector3 originPos;

	void Start()
	{
		tumbler = transform.parent.Find ("Tumbler");
		originPos = tumbler.localPosition;
	}

	void Update()
	{
		transform.localScale = new Vector3 (1 - Mathf.Abs(originPos.x - tumbler.localPosition.x), 1, 1);
	}
}
