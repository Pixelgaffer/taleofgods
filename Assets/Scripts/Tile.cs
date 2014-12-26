using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {

	public bool solid;

	void Start () {
		getTileMap ().addTile (this);
		Vector3 position = transform.localPosition;
		position.x = Mathf.FloorToInt (position.x);
		position.y = Mathf.FloorToInt (position.y);
		position.z = Mathf.FloorToInt (position.z);
		transform.localPosition = position;
	}

	void Destroy() {
		getTileMap ().removeTile (this);
	}

	void Update () {
		
	}

	private TileMap getTileMap() {
		return MultiplayerManager.FindGameObject<TileMap> ("TileMap");
	}

}
