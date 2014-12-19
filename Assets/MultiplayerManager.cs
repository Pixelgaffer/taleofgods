using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MultiplayerManager : MonoBehaviour {

	public InputField username;
	public InputField password;

	void Start () {
		
	}
	
	void Update () {
		
	}

	void Awake() {
		DontDestroyOnLoad (transform.gameObject);
	}

	public void LogIn() {
		LogIn (username.text, password.text);
	}

	public void LogIn(string username, string password) {
		Application.LoadLevel ("mainmenu");
	}
}
