using UnityEngine;
using System.Collections;
using System;

public class Demo : MonoBehaviour
{
	private void OnEnable()
	{
		InputController.MoveEvent += OnMoveEvent;
		InputController.FireEvent += OnFireEvent;
	}

	private void OnDisable()
	{
		InputController.MoveEvent -= OnMoveEvent;
		InputController.FireEvent -= OnFireEvent;
	}

	private void OnFireEvent(object sender, InfoEventArgs<int> e)
	{
		Debug.Log("Fire " + e.info);
	}

	private void OnMoveEvent(object sender, InfoEventArgs<Point> e)
	{
		Debug.Log("Move " + e.info.ToString());
	}

}
