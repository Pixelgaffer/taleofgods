using UnityEngine;
using System.Collections;
using System;

public class TileMap : MonoBehaviour {

	private MapGenerator generator;
	private ArrayList tiles = new ArrayList ();
	private ArrayList buildings = new ArrayList ();
	private ArrayList entites = new ArrayList ();
	private bool changed = false;
	private Tile[,] storedTileGrid;

	void Start () {
		generator = GetComponent<MapGenerator> ();
		Debug.Log (generator);
		if (generator != null) {
			generator.generateMap (this);
		}
	}
	
	void Update () {
		
	}	

	public void addTile(Tile tile) {
		tiles.Add (tile);
		changed = true;
	}

	public void removeTile(Tile tile) {
		tiles.Remove (tile);
		changed = true;
	}

	public Vector2 getPosition(Tile tile) {
		Tile[,] tileGrid = convertToArray ();
		for (int i = 0; i < tileGrid.GetLength(0); i++) {
			for (int j = 0; j < tileGrid.GetLength(0); j++) {
				if(tileGrid[i, j] == tile) {
					return new Vector2 (i, j);
				}
			}
		}
		return new Vector2 (-1, -1);
	}

	public Tile getTile(int x, int y) {
		Tile[,] tileGrid = convertToArray();
		if(x >= tileGrid.GetLength (0) || x < 0 || y >= tileGrid.GetLength (1) || y < 0) {
			return null;
		}
		return tileGrid[x, y];
	}

	public Tile[,] convertToArray() {
		if (!changed) {
			return storedTileGrid;
		}
		int maxX = Int32.MinValue;
		int maxY = Int32.MinValue;
		foreach (Tile tile in tiles) {
			int x = Mathf.FloorToInt(tile.gameObject.transform.localPosition.x);
			int y = Mathf.FloorToInt(tile.gameObject.transform.localPosition.y);
			if(x > maxX) {
				maxX = x;
			}
			if(y > maxY) {
				maxY = y;
			}
		}
        Tile[,] tileGrid = new Tile[maxX, maxY];
		foreach(Tile tile in tiles) {
			int x = Mathf.FloorToInt(tile.gameObject.transform.localPosition.x) - 1;
			int y = Mathf.FloorToInt(tile.gameObject.transform.localPosition.y) - 1;
            if (x < 0 || y < 0) {
                continue;
            }
            tileGrid[x, y] = tile;
			
		}
		storedTileGrid = tileGrid;
		changed = false;
		return tileGrid;
	}
}
