using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FlyMovement : Movement
{
	public override IEnumerator Traverse(Tile tile)
	{
		// Store the distance between the start tile and target tile
		float dist = Mathf.Sqrt(Mathf.Pow(tile.Pos.x - unit.Tile.Pos.x, 2) + Mathf.Pow(tile.Pos.y - unit.Tile.Pos.y, 2));
		unit.Place(tile);

		// Fly high enough not to clip through any ground tiles
		float y = Tile.StepHeight * 10;
		float duration = (y - jumper.position.y) * 0.5f;
		Tweener tweener = jumper.MoveToLocal(new Vector3(0, y, 0), duration, EasingEquations.EaseInOutQuad);
		while (tweener != null)
			yield return null;

		// Turn to face the general direction
		Directions dir;
		Vector3 toTile = (tile.Center - transform.position);

		if (Mathf.Abs(toTile.x) > Mathf.Abs(toTile.z))
			dir = toTile.x > 0 ? Directions.East : Directions.West;
		else
			dir = toTile.z > 0 ? Directions.North : Directions.South;

		yield return StartCoroutine(Turn(dir));
		
		// Move to the correct position
		duration = dist * 0.5f;
		tweener = transform.MoveTo(tile.Center, duration, EasingEquations.EaseInOutQuad);
		while (tweener != null)
			yield return null;

		// Land
		duration = (y - tile.Center.y) * 0.5f;
		tweener = jumper.MoveToLocal(Vector3.zero, 0.5f, EasingEquations.EaseInOutQuad);

		while (tweener != null)
			yield return null;
	}
}
