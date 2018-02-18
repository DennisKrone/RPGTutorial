using UnityEngine;
using System.Collections;
using System;

public class StatModifierFeature : Feature
{
	public StatTypes Type;
	public int Amount;

	Stats stats
	{
		get
		{
			return target.GetComponentInParent<Stats>();
		}
	}

	protected override void OnApply()
	{
		stats[Type] += Amount;
	}

	protected override void OnRemove()
	{
		stats[Type] -= Amount;
	}
}
