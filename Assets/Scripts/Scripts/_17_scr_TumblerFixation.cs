using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _17_scr_TumblerFixation : MonoBehaviour {

	Rigidbody rb;
	AudioSource source;
	_17_scr_TimeManager manager;
	_17_scr_MouseFollow player;

	void Start()
	{
		manager = Camera.main.GetComponent<_17_scr_TimeManager> ();
		manager.tumblers.Add (gameObject);

		rb = GetComponent<Rigidbody> ();
		source = GetComponent<AudioSource> ();
	}

	void OnCollisionEnter(Collision col)
	{
		if (player == null && col.transform.GetComponent<_17_scr_MouseFollow> () != null) 
		{
			player = col.transform.GetComponent<_17_scr_MouseFollow> ();
		}
	}

	void OnTriggerEnter()
	{
		StartCoroutine (Fixate ());
		player.StartCoroutine (player.Fixate ());
	}

	IEnumerator Fixate ()
	{
		//source.pitch = Random.Range (0.95f, 1.05f);
		source.Play ();
		Camera.main.GetComponent<_17_scr_ScreenShakeGenerator> ().ShakeScreen (0.1f, 0.05f);

		SpringJoint joint = GetComponent<SpringJoint> ();
		joint.breakForce = 0;
		yield return null;
		rb.isKinematic = true;

		manager.tumblers.Remove (gameObject);
	}
}
