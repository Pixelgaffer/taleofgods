using UnityEngine;
using System.Collections;

namespace tog {
	public class Util : MonoBehaviour {

		public void switchToScene(string scene) {
			Application.LoadLevel (scene);
		}

	}
}