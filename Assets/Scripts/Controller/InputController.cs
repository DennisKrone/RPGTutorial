using UnityEngine;
using System.Collections;
using System;

public class InputController : MonoBehaviour
{
	public static event EventHandler<InfoEventArgs<Point>> MoveEvent;
	public static event EventHandler<InfoEventArgs<int>> FireEvent;

	Repeater hor = new Repeater("Horizontal");
	Repeater ver = new Repeater("Vertical");

	string[] buttons = new string[] { "Fire1", "Fire2", "Fire3" };

	private void Update()
	{
		int x = hor.Update();
		int y = ver.Update();
		if(x != 0 || y != 0)
		{
			if(MoveEvent != null)
			{
				MoveEvent(this, new InfoEventArgs<Point>(new Point(x, y)));
			}
		}

		for(int i = 0; i < 3; i++)
		{
			if (Input.GetButtonUp(buttons[i]))
			{
				if(FireEvent != null)
				{
					FireEvent(this, new InfoEventArgs<int>(i));
				}
			}
		}
	}

	class Repeater
	{
		const float threshold = 0.5f;
		const float rate = 0.25f;
		float _next;
		bool _hold;
		string _axis;
		public Repeater(string axisName)
		{
			_axis = axisName;
		}
		public int Update()
		{
			int retValue = 0;
			int value = Mathf.RoundToInt(Input.GetAxisRaw(_axis));
			if (value != 0)
			{
				if (Time.time > _next)
				{
					retValue = value;
					_next = Time.time + (_hold ? rate : threshold);
					_hold = true;
				}
			}
			else
			{
				_hold = false;
				_next = 0;
			}
			return retValue;
		}
	}
}
