using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleController : StateMachine
{
	public AbilityMenuPanelController AbilityMenuPanelController;
	public Turn Turn = new Turn();
	public List<Unit> Units = new List<Unit>();
	public CameraRig CameraRig;
	public Board Board;
	public LevelData LevelData;
	public Transform TileSelectionIndicator;
	public Point Pos;
	public GameObject HeroPrefab;
	public Tile CurrentTile { get { return Board.GetTile(Pos); } }

	void Start()
	{
		ChangeState<InitBattleState>();
	}
}
