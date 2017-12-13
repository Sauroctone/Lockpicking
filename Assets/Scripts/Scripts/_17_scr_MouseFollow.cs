using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _17_scr_MouseFollow : MonoBehaviour {

	Rigidbody rb;
	Vector3 mousePos;
	Vector3 prevPos;
	Vector3 movement;

	public float rotSpeed;
	bool isFixating;
	public float fixateTime;

	void Start()
	{
		rb = GetComponent<Rigidbody> ();

		mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		prevPos = transform.position;
	}
		
	void FixedUpdate()
	{
		mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);

		if (!isFixating)
			rb.MovePosition (new Vector3 (mousePos.x, mousePos.y, transform.position.z));

		movement = transform.position - prevPos;

		//if (movement != Vector3.zero) 

//			transform.rotation = Quaternion.LookRotation (movement.normalized, Vector3.back);
		//	transform.LookAt (mousePos);

		//	transform.rotation = Quaternion.Euler (0, 0, transform.rotation.z);
		

		prevPos = transform.position;
	}

	public IEnumerator Fixate()
	{
		isFixating = true;
		yield return new WaitForSeconds (fixateTime);
		isFixating = false;
	}
}
