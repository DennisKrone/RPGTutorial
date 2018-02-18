using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class WalkMovement : Movement
{
	public override IEnumerator Traverse(Tile tile)
	{
		unit.Place(tile);

		List<Tile> targets = new List<Tile>();
		while(tile != null)
		{
			targets.Insert(0, tile);
			tile = tile.Prev;
		}

		for(int i = 1; i < targets.Count; i++)
		{
			Tile from = targets[i - 1];
			Tile to = targets[i];

			Directions dir = from.GetDirection(to);
			if(unit.Dir != dir)
			{
				yield return StartCoroutine(Turn(dir));
			}

			if(from.Height == to.Height)
			{
				yield return StartCoroutine(Walk(to));
			}
			else
			{
				yield return StartCoroutine(Jump(to));
			}
		}

		yield return null;
	}

	protected override bool ExpandSearch(Tile from, Tile to)
	{
		if ((Mathf.Abs(from.Height - to.Height) > JumpHeight))
			return false;

		if (to.Content != null)
			return false;

		return base.ExpandSearch(from, to);
	}

	IEnumerator Walk(Tile target)
	{
		Tweener tweener = transform.MoveTo(target.Center, 0.5f, EasingEquations.Linear);
		while (tweener != null)
			yield return null;
	}
	IEnumerator Jump(Tile to)
	{
		Tweener tweener = transform.MoveTo(to.Center, 0.5f, EasingEquations.Linear);
		Tweener t2 = jumper.MoveToLocal(new Vector3(0, Tile.StepHeight * 2f, 0), tweener.easingControl.duration / 2f, EasingEquations.EaseOutQuad);
		t2.easingControl.loopCount = 1;
		t2.easingControl.loopType = EasingControl.LoopType.PingPong;
		while (tweener != null)
			yield return null;
	}
}
