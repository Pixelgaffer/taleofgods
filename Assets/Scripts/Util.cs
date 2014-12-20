using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace tog {

	public class Util : MonoBehaviour {

		public void switchToScene(string scene) {
			Application.LoadLevel (scene);
		}

		public MultiplayerManager getMM() {
			return GameObject.FindGameObjectWithTag ("MultiplayerManager").GetComponent<MultiplayerManager>();
		}

		public void LogIn() {
			getMM ().LogIn ();
		}

		public void Register() {
			getMM ().RegisterUser ();
		}

	}

}