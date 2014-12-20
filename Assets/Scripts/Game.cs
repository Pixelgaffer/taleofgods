using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Game : MonoBehaviour {

	public GameObject info;
	public Button button;

	void Start () {
		clear ();
	}
	
	void Update () {
		
	}

	public void setInfo(int maxPlayers, int players, string name) {
		info.GetComponent<Text>().text = name + "    " + players + "/" + maxPlayers;
		button.interactable = true;
	}

	public void clear() {
		info.GetComponent<Text> ().text = "-";
		button.interactable = false;
	}
}
