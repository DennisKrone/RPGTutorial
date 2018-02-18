using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CommandSelectionState : BaseAbilityMenuState
{
	protected override void LoadMenu()
	{
		if (menuOptions == null)
		{
			menuTitle = "Commands";
			menuOptions = new List<string>(3);
			menuOptions.Add("Move");
			menuOptions.Add("Action");
			menuOptions.Add("Wait");
		}
		AbilityMenuPanelController.Show(menuTitle, menuOptions);
		AbilityMenuPanelController.SetLocked(0, Turn.HasUnitMoved);
		AbilityMenuPanelController.SetLocked(1, Turn.HasUnitActed);
	}

	protected override void Confirm()
	{
		switch (AbilityMenuPanelController.selection)
		{
			case 0: // Move
				owner.ChangeState<MoveTargetState>();
				break;
			case 1: // Action
				owner.ChangeState<CategorySelectionState>();
				break;
			case 2: // Wait
				owner.ChangeState<SelectUnitState>();
				break;
		}
	}

	protected override void Cancel()
	{
		if (Turn.HasUnitMoved && !Turn.LockMove)
		{
			Turn.UndoMove();
			AbilityMenuPanelController.SetLocked(0, false);
			SelectTile(Turn.Actor.Tile.Pos);
		}
		else
		{
			owner.ChangeState<ExploreState>();
		}
	}
}