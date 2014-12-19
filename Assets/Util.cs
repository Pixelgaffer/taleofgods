using UnityEngine;
using System.Collections;

public class Util : MonoBehaviour {

	public void switchToScene(string scene) {
		Application.LoadLevel (scene);
	}

}
