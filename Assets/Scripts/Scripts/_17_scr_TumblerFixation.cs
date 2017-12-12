using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _17_scr_TumblerFixation : MonoBehaviour {

	Rigidbody rb;
	AudioSource source;
	_17_scr_TimeManager manager;

	void Start()
	{
		manager = Camera.main.GetComponent<_17_scr_TimeManager> ();
		manager.tumblers.Add (gameObject);

		rb = GetComponent<Rigidbody> ();
		source = GetComponent<AudioSource> ();
	}

	void OnTriggerEnter()
	{
		StartCoroutine (Fixate ());
	}

	IEnumerator Fixate ()
	{
		//source.pitch = Random.Range (0.95f, 1.05f);
		source.Play ();

		SpringJoint joint = GetComponent<SpringJoint> ();
		joint.breakForce = 0;
		yield return null;
		rb.isKinematic = true;

		manager.tumblers.Remove (gameObject);
	}
}
