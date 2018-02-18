using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleState : State
{
	public CameraRig CameraRig { get { return owner.CameraRig; } }
	public Board Board { get { return owner.Board; } }
	public LevelData LevelData { get { return owner.LevelData; } }
	public Transform TileSelectionIndicator { get { return owner.TileSelectionIndicator; } }
	public Point Pos { get { return owner.Pos; } set { owner.Pos = value; } }
	public AbilityMenuPanelController AbilityMenuPanelController { get { return owner.AbilityMenuPanelController; } }
	public Turn Turn { get { return owner.Turn; } }
	public List<Unit> Units { get { return owner.Units; } }

	protected BattleController owner;
	
	protected virtual void Awake()
	{
		owner = GetComponent<BattleController>();
	}

	protected override void AddListeners()
	{
		InputController.MoveEvent += OnMove;
		InputController.FireEvent += OnFire;
	}

	protected override void RemoveListeners()
	{
		InputController.MoveEvent -= OnMove;
		InputController.FireEvent -= OnFire;
	}
	protected virtual void OnMove(object sender, InfoEventArgs<Point> e)
	{

	}

	protected virtual void OnFire(object sender, InfoEventArgs<int> e)
	{

	}
	protected virtual void SelectTile(Point p)
	{
		if (Pos == p || !Board.Tiles.ContainsKey(p))
			return;
		Pos = p;
		TileSelectionIndicator.localPosition = Board.Tiles[p].Center;
	}
}
