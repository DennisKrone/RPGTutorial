using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour
{
	public Tile Tile { get; protected set; }
	public Directions Dir;

	public void Place(Tile target)
	{
		if(Tile != null && Tile.Content == gameObject)
		{
			Tile.Content = null;
		}

		Tile = target;

		if(target != null)
		{
			target.Content = gameObject;
		}
	}

	public void Match()
	{
		transform.localPosition = Tile.Center;
		transform.localEulerAngles = Dir.ToEuler();
	}
}
