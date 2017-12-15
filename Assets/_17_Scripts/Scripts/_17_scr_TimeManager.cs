using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.SceneManagement;

public class _17_scr_TimeManager : MonoBehaviour {

	public float timer;
	public float scale;
	public float screenFall;
	public _17_scr_CollisionWithWalls walls;
	public Slider slider;
	public Image intro;
	public GameObject line;
	public GameObject mouseCursor;
	public GameObject text;

	public GameObject failScreen;
	Animator failAnim;
	public GameObject failTextSlow;
	public GameObject failTextOops;
	GameObject failText;

	public GameObject winScreen;
	Animator winAnim;
	public GameObject winText;

	public List<GameObject> tumblers = new List<GameObject>();

	public GameState state;

	public GameObject breakParticles;

	AudioSource[] sources;
	Coroutine timerInst;

	void Start ()
	{
		state = GameState.Startup;
		intro.material.SetFloat ("_Scale", 40);
		sources = GetComponents<AudioSource> ();
	}

	void Update ()
	{
		if (state == GameState.Ticking) 
		{
			slider.value -= Time.deltaTime / timer;

			if (tumblers.Count == 0) 
			{
				StartCoroutine(Win ());
			}
		}

		if (state == GameState.Loss || state == GameState.Win) 
		{
			if (Input.GetKeyDown (KeyCode.Space)) 
			{
				//SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
			}
		}
	}

	public void StartTimer()
	{
		timerInst = StartCoroutine (Timer ());
	}

	IEnumerator Timer ()
	{
		line.SetActive (true);
		mouseCursor.SetActive (true);
		text.SetActive (false);

		while (intro.material.GetFloat ("_Scale") > 0) 
		{
			intro.material.SetFloat ("_Scale", intro.material.GetFloat("_Scale") - Time.deltaTime * scale);
			yield return null;
		}

		intro.material.SetFloat ("_Scale", 0);

		state = GameState.Ticking;
		yield return new WaitForSeconds (timer);
		state = GameState.Loss;
		Lose ();
	}

	public void Lose()
	{
		if (state == GameState.Ticking) 
		{
			StopCoroutine (timerInst);
			breakParticles.gameObject.SetActive (true);
			GetComponent<_17_scr_ScreenShakeGenerator> ().ShakeScreen (0.2f, 0.2f);
			failText = failTextOops;
			mouseCursor.GetComponent<AudioSource> ().Stop ();
		} 

		else
			failText = failTextSlow;	

		StartCoroutine (Loss ());

		mouseCursor.GetComponent<_17_scr_MouseFollow> ().enabled = false;
	}

	IEnumerator Win ()
	{
		state = GameState.Win;
		StopCoroutine (timerInst);
		mouseCursor.GetComponent<AudioSource> ().Stop ();

		mouseCursor.GetComponent<_17_scr_MouseFollow> ().enabled = false;

		foreach (AudioSource source in sources) 
		{
			source.Play ();
		}

		winAnim = winScreen.GetComponent<Animator> ();
		winAnim.SetTrigger ("wins");
		RectTransform winScr = winScreen.GetComponent<RectTransform> ();

		while (winScr.anchoredPosition.x > 0) 
		{
			yield return null;
		}

		winText.SetActive (true);
		//yield return new WaitForSeconds (4);
		//GetComponent<Transition> ().Win ();
	}

	IEnumerator Loss()
	{
		state = GameState.Loss;

		ParticleSystem particles = breakParticles.GetComponent<ParticleSystem> ();

		while (particles.isPlaying) 
		{
			yield return null;
		}

		failAnim = failScreen.GetComponent<Animator> ();
		failAnim.SetTrigger ("loses");
		RectTransform failScr = failScreen.GetComponent<RectTransform> ();

		while (failScr.anchoredPosition.y > 0) 
		{
			yield return null;
		}

		failText.SetActive (true);

		//yield return new WaitForSeconds (4);
		//GetComponent<Transition> ().Lose ();
	}
}
