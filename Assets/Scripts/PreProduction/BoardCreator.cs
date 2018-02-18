using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;

public class BoardCreator : MonoBehaviour
{
	[SerializeField]
	Tile tileViewPrefab;
	[SerializeField]
	GameObject tileSelectionPrefab;
	[SerializeField]
	int width = 10;
	[SerializeField]
	int depth = 10;
	[SerializeField]
	int height = 8;
	[SerializeField]
	Point pos;
	[SerializeField]
	LevelData levelData;

	Transform _marker;
	Dictionary<Point, Tile> tiles = new Dictionary<Point, Tile>();

	public void Clear()
	{
		for(int i = transform.childCount -1; i >= 0; i--)
		{
			DestroyImmediate(transform.GetChild(i).gameObject);
			tiles.Clear();
		}
	}

	public void UpdateMarker()
	{
		Tile t = tiles.ContainsKey(pos) ? tiles[pos] : null;
		marker.localPosition = t != null ? t.Center : new Vector3(pos.x, 0, pos.y);
	}
	public void Grow()
	{
		GrowSingle(pos);
	}

	public void Shrink()
	{
		ShrinkSingle(pos);
	}

	public void GrowArea()
	{
		Rect r = RandomRect();
		GrowRect(r);
	}

	public void ShrinkArea()
	{
		Rect r = RandomRect();
		ShrinkRect(r);
	}

	public void Save()
	{
		string filePath = Application.dataPath + "/Resources/Levels";
		if (!Directory.Exists(filePath))
			CreateSaveDirectory();

		LevelData board = ScriptableObject.CreateInstance<LevelData>();
		board.Tiles = new List<Vector3>(tiles.Count);
		foreach(Tile t in tiles.Values)
		{
			board.Tiles.Add(new Vector3(t.Pos.x, t.Height, t.Pos.y));
		}

		string fileName = string.Format("Assets/Resources/Levels/{1}.asset", filePath, name);
		AssetDatabase.CreateAsset(board, fileName);
	}

	public void Load()
	{
		Clear();
		if (levelData == null)
			return;
		foreach (Vector3 v in levelData.Tiles)
		{
			Tile t = Create();
			t.Load(v);
			tiles.Add(t.Pos, t);
		}
	}

	void CreateSaveDirectory()
	{
		string filePath = Application.dataPath + "/Resources";

		if (!Directory.Exists(filePath))
		{
			AssetDatabase.CreateFolder("Assets", "Resources");
		}

		filePath += "Levels";

		if (!Directory.Exists(filePath))
		{
			AssetDatabase.CreateFolder("Assets/Resources", "Levels");
		}

		AssetDatabase.Refresh();
	}

	Tile Create()
	{
		GameObject instance = Instantiate(tileViewPrefab.gameObject, transform, false);
		return instance.GetComponent<Tile>();
	}

	Tile GetOrCreate(Point p)
	{
		if (tiles.ContainsKey(p))
			return tiles[p];

		Tile t = Create();
		t.Load(p, 0);
		tiles.Add(p, t);

		return t;
	}

	Rect RandomRect()
	{
		int x = UnityEngine.Random.Range(0, width);
		int y = UnityEngine.Random.Range(0, depth);
		int w = UnityEngine.Random.Range(1, width - x + 1);
		int h = UnityEngine.Random.Range(1, depth - y + 1);
		return new Rect(x, y, w, h);
	}

	void GrowRect(Rect rect)
	{
		for (int y = (int)rect.yMin; y < (int)rect.yMax; ++y)
		{
			for (int x = (int)rect.xMin; x < (int)rect.xMax; ++x)
			{
				Point p = new Point(x, y);
				GrowSingle(p);
			}
		}
	}

	void ShrinkRect(Rect rect)
	{
		for (int y = (int)rect.yMin; y < (int)rect.yMax; ++y)
		{
			for (int x = (int)rect.xMin; x < (int)rect.xMax; ++x)
			{
				Point p = new Point(x, y);
				ShrinkSingle(p);
			}
		}
	}

	void GrowSingle(Point p)
	{
		Tile t = GetOrCreate(p);
		if(t.Height < height)
		{
			t.Grow();
		}
	}

	void ShrinkSingle(Point p)
	{
		if (!tiles.ContainsKey(p))
			return;

		Tile t = tiles[p];
		t.Shrink();

		if (t.Height <= 0)
		{
			tiles.Remove(p);
			DestroyImmediate(t.gameObject);
		}
	}

	Transform marker
	{
		get
		{
			if(_marker == null)
			{
				GameObject instance = Instantiate(tileSelectionPrefab) as GameObject;
				_marker = instance.transform;
			}

			return _marker;
		}
	}
}
