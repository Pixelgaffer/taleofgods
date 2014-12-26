using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace tog {

	public class Util : MonoBehaviour {

        public static Util getUtil() {
            GameObject util = GameObject.FindGameObjectWithTag("Util");
            if (util == null) {
                return null;
            }
            return util.GetComponent<Util>();
        }

        public GameObject multiplayerManager;

		private string joinName;

		public void switchToScene(string scene) {
			Application.LoadLevel (scene);
		}

		public MultiplayerManager getMM() {
            GameObject mm = GameObject.FindGameObjectWithTag ("MultiplayerManager");
            if(mm == null) {
                mm = (GameObject) Instantiate(multiplayerManager);
            }
			return mm.GetComponent<MultiplayerManager>();
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