using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class _17_scr_TimerColor : MonoBehaviour {

	public Color color1;
	public Color color2;
	Image fill;
	public Slider slider;

	void Start()
	{
		fill = GetComponent<Image> ();
		fill.color = color1;
	}

	void Update ()
	{
		fill.color = Color.Lerp(color2, color1, slider.value);
	}
}
