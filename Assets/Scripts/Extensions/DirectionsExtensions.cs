using UnityEngine;
using System.Collections;

public static class DirectionsExtensions 
{
	public static Directions GetDirection (this Tile t1, Tile t2)
	{
		if (t1.Pos.y < t2.Pos.y)
			return Directions.North;
		if (t1.Pos.x < t2.Pos.x)
			return Directions.East;
		if (t1.Pos.y > t2.Pos.y)
			return Directions.South;

		return Directions.West;
	}

	public static Vector3 ToEuler (this Directions d)
	{
		return new Vector3(0, (int)d * 90, 0);
	}
}
