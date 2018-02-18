using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Turn
{
	public Unit Actor;
	public bool HasUnitMoved;
	public bool HasUnitActed;
	public bool LockMove;

	Tile startTile;
	Directions startDir;

	public void Change(Unit current)
	{
		Actor = current;
		HasUnitMoved = false;
		HasUnitActed = false;
		LockMove = false;
		startTile = Actor.Tile;
		startDir = Actor.Dir;
	}

	public void UndoMove()
	{
		HasUnitMoved = false;
		Actor.Place(startTile);
		Actor.Dir = startDir;
		Actor.Match();
	}
}