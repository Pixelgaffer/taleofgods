using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using tog;

public class Game : MonoBehaviour {

	public GameObject infoText;
	public Button button;

	new private string name;

	void Start () {
		clear ();
	}
	
	void Update () {
		
	}

	public void setInfo(RoomInfo info) {
		infoText.GetComponent<Text>().text = info.name + "    " + info.playerCount + "/" + info.maxPlayers;
		button.interactable = true;
		name = info.name;
	}

	public void clear() {
		infoText.GetComponent<Text> ().text = "-";
		button.interactable = false;
		name = null;
	}

	public void JoinButton() {
		if (name == null) {
			return;
		}
		GameObject.Find ("Util").GetComponent<Util> ().setJoinRoom (name);
	}
}
