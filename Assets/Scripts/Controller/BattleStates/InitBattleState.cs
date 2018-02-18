using UnityEngine;
using System.Collections;
using System;

public class InitBattleState : BattleState
{
	public override void Enter()
	{
		base.Enter();
		StartCoroutine(Init());
	}

	IEnumerator Init()
	{
		Board.Load(LevelData);
		Point p = new Point((int)LevelData.Tiles[0].x, (int)LevelData.Tiles[0].z);
		SelectTile(p);
		SpawnTestUnits();
		yield return null;
		owner.ChangeState<CutSceneState>();
	}

	private void SpawnTestUnits()
	{
		System.Type[] components = new System.Type[] { typeof(WalkMovement), typeof(FlyMovement), typeof(TeleportMovement) };

		for(int i = 0; i < 3; i++)
		{
			GameObject instance = Instantiate(owner.HeroPrefab) as GameObject;

			Point p = new Point((int)LevelData.Tiles[i].x, (int)LevelData.Tiles[i].z);

			Unit unit = instance.GetComponent<Unit>();
			unit.Place(Board.GetTile(p));
			unit.Match();

			Movement m = instance.AddComponent(components[i]) as Movement;
			m.Range = 5;
			m.JumpHeight = 10;
			Units.Add(unit);
		}
	}
}
