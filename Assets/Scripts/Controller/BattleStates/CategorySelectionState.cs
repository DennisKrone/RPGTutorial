using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class CategorySelectionState : BaseAbilityMenuState
{
	protected override void LoadMenu()
	{
		if (menuOptions == null)
		{
			menuTitle = "Action";
			menuOptions = new List<string>(3);
			menuOptions.Add("Attack");
			menuOptions.Add("White Magic");
			menuOptions.Add("Black Magic");
		}

		AbilityMenuPanelController.Show(menuTitle, menuOptions);
	}
	protected override void Confirm()
	{
		switch (AbilityMenuPanelController.selection)
		{
			case 0:
				Attack();
				break;
			case 1:
				SetCategory(0);
				break;
			case 2:
				SetCategory(1);
				break;
		}
	}

	protected override void Cancel()
	{
		owner.ChangeState<CommandSelectionState>();
	}
	void Attack()
	{
		Turn.HasUnitActed = true;
		if (Turn.HasUnitMoved)
			Turn.LockMove = true;
		owner.ChangeState<CommandSelectionState>();
	}
	void SetCategory(int index)
	{
		ActionSelectionState.category = index;
		owner.ChangeState<ActionSelectionState>();
	}
}