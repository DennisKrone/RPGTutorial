using UnityEngine;
using System.Collections;

public abstract class Feature : MonoBehaviour
{
	protected GameObject target { get; private set; }

	public void Activate(GameObject newTarget)
	{
		if(target == null)
		{
			target = newTarget;
			OnApply();
		}
	}

	public void Deactivate()
	{
		if(target != null)
		{
			OnRemove();
			target = null;
		}
	}

	public void Apply(GameObject newTarget)
	{
		target = newTarget;
		OnApply();
		target = null;
	}

	protected abstract void OnApply();
	protected abstract void OnRemove();
}
