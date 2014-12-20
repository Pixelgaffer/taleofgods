using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace tog {

	public class Util : MonoBehaviour {

		private string joinName;

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

		public void JoinRoom () {
			getMM ().JoinRoom ();
		}

		public void setJoinRoom(string name) {
			joinName = name;
		}

		public void JoinRoomButton() {
			InputField joinKey = MultiplayerManager.FindGameObject<InputField> ("JoinKey");
			getMM ().JoinRoom (joinName, joinKey.text);
		}

		public void CreateRoom () {
			getMM ().CreateRoom ();
		}

	}

}