using UnityEngine;
using System.Collections;

public class MoveSequenceState : BattleState
{
	public override void Enter()
	{
		base.Enter();
		StartCoroutine("Sequence");
	}

	IEnumerator Sequence()
	{
		Movement m = Turn.Actor.GetComponent<Movement>();
		yield return StartCoroutine(m.Traverse(owner.CurrentTile));
		Turn.HasUnitMoved = true;
		owner.ChangeState<CommandSelectionState>();
	}
}
