using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MoveTargetState : BattleState
{
	List<Tile> tiles;

	public override void Enter()
	{
		base.Enter();
		Movement mover = owner.Turn.Actor.GetComponent<Movement>();
		tiles = mover.GetTilesInRange(Board);
		Board.SelectTiles(tiles);
	}

	public override void Exit()
	{
		base.Exit();
		Board.DeselectTiles(tiles);
		tiles = null;
	}

	protected override void OnMove(object sender, InfoEventArgs<Point> e)
	{
		SelectTile(e.info + Pos);
	}

	protected override void OnFire(object sender, InfoEventArgs<int> e)
	{
		if (e.info == 0)
		{
			if (tiles.Contains(owner.CurrentTile))
				owner.ChangeState<MoveSequenceState>();
		}
		else
		{
			owner.ChangeState<CommandSelectionState>();
		}
	}
}