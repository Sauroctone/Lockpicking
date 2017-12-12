using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class _17_scr_TimeManager : MonoBehaviour {

	public float timer;
	public _17_scr_CollisionWithWalls walls;
	public Slider slider;
	public Image intro;
	public GameObject line;
	public GameObject mouseCursor;
	public GameObject text;

	public List<GameObject> tumblers = new List<GameObject>();

	public GameState state;

	void Start ()
	{
		state = GameState.Startup;
		intro.material.SetFloat ("_Value", -0.49f);
	}

	void Update ()
	{
		if (state == GameState.Ticking) 
		{
			slider.value -= Time.deltaTime / timer;

			if (tumblers.Count == 0) 
			{
				Win ();
			}
		}
	}

	public void StartTimer()
	{
		StartCoroutine (Timer ());
	}

	IEnumerator Timer ()
	{
		line.SetActive (true);
		mouseCursor.SetActive (true);
		text.SetActive (false);

		while (intro.material.GetFloat ("_Value") < 1) 
		{
			intro.material.SetFloat ("_Value", intro.material.GetFloat("_Value") + Time.deltaTime);
			yield return null;
		}

		state = GameState.Ticking;
		yield return new WaitForSeconds (timer);
		state = GameState.Loss;
		print ("Time's up!");
		Lose ();
	}

	void Win ()
	{
		state = GameState.Win;
		StopCoroutine (Timer ());
		print ("You won !");
		mouseCursor.GetComponent<_17_scr_MouseFollow> ().enabled = false;
	}

	public void Lose()
	{
		if (state == GameState.Ticking) 
		{
			StopCoroutine (Timer ());
			state = GameState.Loss;
			print ("Oops !");
		}

		mouseCursor.GetComponent<_17_scr_MouseFollow> ().enabled = false;
	}
}
