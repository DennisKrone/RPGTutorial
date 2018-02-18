using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Board : MonoBehaviour
{
	[SerializeField]
	GameObject tilePrefab;

	public Dictionary<Point, Tile> Tiles = new Dictionary<Point, Tile>();

	Color selectedTileColor = new Color(0, 1, 1, 1);
	Color defaultTileColor = new Color(1, 1, 1, 1);
	
	Point[] dirs = new Point[4]
	{
		new Point(0, 1),
		new Point(0, -1),
		new Point(1, 0),
		new Point(-1, 0)
	};

	public void SelectTiles(List<Tile> tiles)
	{
		for(int i = tiles.Count -1; i >= 0; --i)
		{
			tiles[i].GetComponent<Renderer>().material.SetColor("_Color", selectedTileColor);
		}
	}

	public void DeselectTiles(List<Tile> tiles)
	{
		for(int i = tiles.Count -1; i >= 0; --i)
		{
			tiles[i].GetComponent<Renderer>().material.SetColor("_Color", defaultTileColor);
		}
	}

	public void Load(LevelData data)
	{
		for (int i = 0; i < data.Tiles.Count; ++i)
		{
			GameObject instance = Instantiate(tilePrefab, transform, false) as GameObject;
			Tile t = instance.GetComponent<Tile>();
			t.Load(data.Tiles[i]);
			Tiles.Add(t.Pos, t);
		}
	}

	public List<Tile> Search (Tile start, Func<Tile, Tile, bool> addTile)
	{
		List<Tile> retValue = new List<Tile>();
		retValue.Add(start);
		ClearSearch();

		Queue<Tile> checkNext = new Queue<Tile>();
		Queue<Tile> checkNow = new Queue<Tile>();

		start.Distance = 0;
		checkNow.Enqueue(start);

		while(checkNow.Count > 0)
		{
			Tile t = checkNow.Dequeue();

			for(int i = 0; i < 4; ++i)
			{
				Tile next = GetTile(t.Pos + dirs[i]);

				if(next == null || next.Distance <= t.Distance + 1)
				{
					continue;
				}

				if(addTile(t, next))
				{
					next.Distance = t.Distance + 1;
					next.Prev = t;
					checkNext.Enqueue(next);
					retValue.Add(next);
				}
			}

			if(checkNow.Count == 0)
			{
				SwapReference(ref checkNow, ref checkNext);
			}
		}

		return retValue;
	}

	private void SwapReference(ref Queue<Tile> a, ref Queue<Tile> b)
	{
		Queue<Tile> temp = a;
		a = b;
		b = temp;
	}

	public Tile GetTile(Point p)
	{
		return Tiles.ContainsKey(p) ? Tiles[p] : null;
	}

	void ClearSearch()
	{
		foreach(Tile t in Tiles.Values)
		{
			t.Prev = null;
			t.Distance = int.MaxValue;
		}
	}
}
