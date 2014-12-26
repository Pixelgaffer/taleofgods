using UnityEngine;
using System.Collections;

public class SimpleGenerator : MapGenerator {

	public int size;
	public GameObject tile1;
	public GameObject tile2;

	public override void generateMap(TileMap map) {
		for (int i = 0; i < size; i++) {
			GameObject instance1 = (GameObject) Instantiate (tile1);
			move (instance1, i, 0);
            instance1.transform.parent = map.transform;

			if(i == 0) {
				continue;
			}

			GameObject instance2 = (GameObject) Instantiate (tile1);
			move (instance2, 0, i);
            instance2.transform.parent = map.transform;

			GameObject instance3 = (GameObject) Instantiate (tile1);
			move (instance3, i, size - 1);
            instance3.transform.parent = map.transform;

			if(i == size - 1) {
				continue;
			}

			GameObject instance4 = (GameObject) Instantiate (tile1);
			move (instance4, size - 1, i);
            instance4.transform.parent = map.transform;

			for (int j = 1; j < size - 1; j++) {
				GameObject instance5 = (GameObject) Instantiate (tile2);
				move (instance5, i, j);
                instance5.transform.parent = map.transform;
			}
		}
	}

}
