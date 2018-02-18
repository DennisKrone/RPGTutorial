using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public abstract class Movement : MonoBehaviour
{
	public int Range;
	public int JumpHeight;

	protected Unit unit;
	protected Transform jumper;

	protected virtual void Awake()
	{
		unit = GetComponent<Unit>();
		jumper = transform.Find("Jumper");
	}

	public abstract IEnumerator Traverse(Tile tile);

	public virtual List<Tile> GetTilesInRange (Board board)
	{
		List<Tile> retValue = board.Search(unit.Tile, ExpandSearch);
		Filter(retValue);

		return retValue;
	}

	protected virtual void Filter(List<Tile> tiles)
	{
		for(int i = tiles.Count -1; i >= 0; --i)
		{
			if(tiles[i].Content != null)
			{
				tiles.RemoveAt(i);
			}
		}
	}

	protected virtual bool ExpandSearch(Tile from, Tile to)
	{
		return (from.Distance + 1) <= Range;
	}

	protected virtual IEnumerator Turn(Directions dir)
	{
		TransformLocalEulerTweener t = (TransformLocalEulerTweener)transform.RotateToLocal(dir.ToEuler(), 0.25f, EasingEquations.EaseInOutQuad);

		if (Mathf.Approximately(t.startValue.y, 0f) && Mathf.Approximately(t.endValue.y, 270f))
			t.startValue = new Vector3(t.startValue.x, 360f, t.startValue.z);
		else if (Mathf.Approximately(t.startValue.y, 270) && Mathf.Approximately(t.endValue.y, 0))
			t.endValue = new Vector3(t.startValue.x, 360f, t.startValue.z);
		unit.Dir = dir;

		while (t != null)
			yield return null;
	}
}
