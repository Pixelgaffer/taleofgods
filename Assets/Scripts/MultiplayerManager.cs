using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
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
			FindGameObject<Text>("Error").text = "Username has to be at least 5 characters long!";
			return;
		}
		if (password.text.Length <= 5) {
			FindGameObject<Text>("Error").text = "Password has to be at least 5 characters long!";
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
	}

	public void RegisterCallback (RegisterPlayFabUserResult result) {
		Debug.Log (result.PlayFabId);
	}

	public void PlayFabError(PlayFabError error) {
		if (error == null || error.ErrorMessage == null) {
			Debug.Log ("An unknown Error occured!");
			FindGameObject<Text>("Error").text = "An unknown Error occured!";
			return;
		}
		Debug.Log (error.ErrorMessage);
		FindGameObject<Text>("Error").text = error.ErrorMessage;
	}

	public void RegisterUser () {
		InputField username = FindGameObject<InputField> ("Username");
		InputField password = FindGameObject<InputField> ("Password");
		InputField password2 = FindGameObject<InputField> ("Password2");
		InputField email = FindGameObject<InputField> ("Email");
		if (username.text.Length <= 5) {
			FindGameObject<Text>("Error").text = "Username has to be at least 5 characters long!";
			return;
		}
		if (password.text.Length <= 5) {
			FindGameObject<Text>("Error").text = "Password has to be at least 5 characters long!";
			return;
		}
		if (!password2.text.Equals (password.text)) {
			FindGameObject<Text>("Error").text = "Passwords have to be the same!";
			return;
		}
		if (!IsValidEmail (email.text)) {
			FindGameObject<Text>("Error").text = "The Email entered is not valid!";
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

	public T FindGameObject<T>(string tag) where T : MonoBehaviour {
		return GameObject.FindGameObjectWithTag(tag).GetComponent<T>();
	}
	
	public bool IsValidEmail(string strIn) {
		return Regex.IsMatch(strIn,
			@"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
			@"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
			RegexOptions.IgnoreCase);
	}

}
