using UnityEngine;
using System.Collections;

public class MapGenerator : MonoBehaviour {

	public virtual void generateMap(TileMap map) {

	}

	protected void move(GameObject gameObject, int x, int y) {
		Vector3 position = gameObject.transform.localPosition;
		position.x = x;
		position.y = y;
		position.z = 0;
		gameObject.transform.localPosition = position;
	}

}
