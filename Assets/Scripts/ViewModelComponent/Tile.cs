using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour
{
	public const float StepHeight = 0.25f;

	public GameObject Content;
	public Point Pos;
	public int Height;
	public Vector3 Center { get { return new Vector3(Pos.x, Height * StepHeight, Pos.y); } }

	[HideInInspector]
	public Tile Prev;
	[HideInInspector]
	public int Distance;

	void Refresh()
	{
		transform.localPosition = new Vector3(Pos.x, Height * StepHeight / 2f, Pos.y);
		transform.localScale = new Vector3(1, Height * StepHeight, 1);
	}

	public void Grow()
	{
		Height++;
		Refresh();
	}

	public void Shrink()
	{
		Height--;
		Refresh();
	}

	public void Load(Point p, int h)
	{
		Pos = p;
		Height = h;
		Refresh();
	}

	public void Load(Vector3 v)
	{
		Load(new Point((int)v.x, (int)v.z), (int)v.y);
	}
}
