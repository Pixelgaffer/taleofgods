using UnityEngine;
using UnityEngine.UI;
using ExitGames.Client.Photon;
using System;
using System.Globalization;
using System.Text.RegularExpressions;
using PlayFab;
using PlayFab.ClientModels;

public class MultiplayerManager : MonoBehaviour {
	
	void Start () {
		
	}
	
	void Update () {
        
	}

	void Awake() {
		DontDestroyOnLoad (transform.gameObject);
	}

	public void LogIn () {
		InputField username = FindGameObject<InputField> ("Username");
		InputField password = FindGameObject<InputField> ("Password");
		if (username.text.Length <= 5) {
			error("Username has to be at least 5 characters long!");
			return;
		}
		if (password.text.Length <= 5) {
			error("Password has to be at least 5 characters long!");
			return;
		}
		LogIn (username.text, password.text);
	}

	public void LogIn(string username, string password) {
		LoginWithPlayFabRequest request = new LoginWithPlayFabRequest ();
		request.Username = username;
		request.Password = password;
		request.TitleId = "7FA5";
		PlayFabClientAPI.LoginWithPlayFab (request, LogInCallback, PlayFabError);
	}

	public void LogInCallback (LoginResult result) {
		Debug.Log (result.PlayFabId);
		Application.LoadLevel ("mainmenu");
	}

	public void RegisterCallback (RegisterPlayFabUserResult result) {
		Debug.Log (result.PlayFabId);
		Application.LoadLevel ("mainmenu");
	}

	public void PlayFabError(PlayFabError pfError) {
		if (pfError == null || pfError.ErrorMessage == null) {
			error("An unknown Error occured!");
			return;
		}
		Debug.Log (pfError.ErrorMessage);
		error(pfError.ErrorMessage);
	}

	public void RegisterUser () {
		InputField username = FindGameObject<InputField> ("Username");
		InputField password = FindGameObject<InputField> ("Password");
		InputField password2 = FindGameObject<InputField> ("Password2");
		InputField email = FindGameObject<InputField> ("Email");
		if (username.text.Length < 5) {
			error("Username has to be at least 5 characters long!");
			return;
		}
		if (password.text.Length < 5) {
			error("Password has to be at least 5 characters long!");
			return;
		}
		if (!password2.text.Equals (password.text)) {
			error("Passwords have to be the same!");
			return;
		}
		if (!IsValidEmail (email.text)) {
			error("The Email entered is not valid!");
			return;
		}
		RegisterUser (username.text, password.text, email.text);
	}

	public void RegisterUser (string username, string password, string email) {
		RegisterPlayFabUserRequest request = new RegisterPlayFabUserRequest ();
		request.Username = username;
		request.Password = password;
		request.Email = email;
		request.TitleId = "7FA5";
		PlayFabClientAPI.RegisterPlayFabUser(request, RegisterCallback, PlayFabError);
	}

	public static T FindGameObject<T>(string tag) where T : MonoBehaviour {
		return GameObject.FindGameObjectWithTag(tag).GetComponent<T>();
	}
	
	public bool IsValidEmail(string strIn) {
		return Regex.IsMatch(strIn,
			@"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
			@"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
			RegexOptions.IgnoreCase);
	}

	public void JoinRoom () {
		InputField roomName = FindGameObject<InputField>("RoomName");
		InputField roomKey = FindGameObject<InputField>("RoomKey");
		if(roomName.text.Length < 5) {
			error ("Room name must be at least 5 characters long!");
			return;
		}
		if(roomName.text.Length > 20) {
			error ("Room name can't be longer than 20 characters!");
			return;
		}
		JoinRoom (roomName.text, roomKey.text);
	}

	public void JoinRoom(string name, string key) {
		if (!PhotonNetwork.connectedAndReady) {
			return;
		}
		foreach (RoomInfo info in PhotonNetwork.GetRoomList ()) {
			if(info.name == name) {
				if(!info.customProperties["key"].Equals(key)) {
					error("Wrong Key!");
					return;
				}
				PhotonNetwork.JoinRoom (name);
				return;
			}
		}
		error("This Room does not exist!");
	}

	void OnJoinedRoom() {
		Application.LoadLevel ("lobby");
	}

	void OnPhotonJoinRoomFailed(object[] codeAndMsg) {
		error ("Failed to join Room!");
		Debug.LogError (codeAndMsg[1]);
	}

	void OnPhotonCreateRoomFailed(object[] codeAndMsg) {
		error("Failed to create Room!");
		Debug.LogError (codeAndMsg[1]);
	}

	public void CreateRoom () {
		InputField name = FindGameObject<InputField> ("RoomName");
		InputField key = FindGameObject<InputField> ("RoomKey");
		InputField maxPlayers = FindGameObject<InputField> ("MaxPlayers");
		int maxPlayersInt = Int32.Parse(maxPlayers.text);
		if (name.text.Length < 5) {
			error ("Room name must be at least 5 characters long.");
			return;
		}
		if (name.text.Length > 20) {
			error ("Room name can't be longer than 20 characters.");
			return;
		}
		if (maxPlayersInt < 1) {
			error ("Max Players must be at least 1");
			return;
		}
		if (maxPlayersInt > 5) {
			error ("Max Players can't be larger than 5");
			return;
		}
		CreateRoom (name.text, key.text, maxPlayersInt);
	}

	public void CreateRoom (string name, string key, int maxPlayers) {
		if (!PhotonNetwork.connectedAndReady) {
			Debug.LogError ("Photon not ready yet");
			return;
		}
		RoomOptions options = new RoomOptions ();
		options.maxPlayers = maxPlayers;
		options.customRoomPropertiesForLobby = new String[] {"key"};
		options.customRoomProperties = new Hashtable ();
		options.customRoomProperties.Add("key", key);
        options.customRoomProperties.Add("started", false);
		PhotonNetwork.CreateRoom (name, options, null);
	}

	void OnCreatedRoom () {
		Debug.Log ("Created Room!");
	}

	public void error(string error) {
		FindGameObject<Text>("Error").text = error;
	}

    [RPC]
    public void StartGameRPC() {
        Application.LoadLevel("game");
    }

}
