using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _17_scr_LineController : MonoBehaviour {

	public LineRenderer line;

	List<Transform> points = new List<Transform>();

	public Transform startingPoint;

	void Start()
	{
		points.Add (transform);
		points.Add (startingPoint);
	}

	void Update ()
	{
		if (line.gameObject.activeSelf)	
			DrawLine ();
	}

	void DrawLine()
	{
		line.positionCount = points.Count;

		for (int i = 0; i < points.Count; i++) 
		{
			line.SetPosition(i, points[i].position);
		}
	}

	void OnTriggerEnter(Collider col)
	{
		if (line.gameObject.activeSelf	&& col.GetComponent<_17_tag_AngleTrigger> () != null) 
		{
			if (points.Contains(col.transform))
			{
				points.Remove (col.transform);
			}

			else 
			{
				points.Insert (1, col.transform);
			}
		}
	}
}
