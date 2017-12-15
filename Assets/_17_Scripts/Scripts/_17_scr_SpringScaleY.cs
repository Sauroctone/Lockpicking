using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _17_scr_SpringScaleY : MonoBehaviour {

	Transform tumbler;
	Vector3 originPos;

	void Start()
	{
		tumbler = transform.parent.Find ("Tumbler");
		originPos = tumbler.localPosition;
	}

	void Update()
	{
		transform.localScale = new Vector3 (1, 1 - Mathf.Abs(originPos.y - tumbler.localPosition.y), 1);
	}
}
