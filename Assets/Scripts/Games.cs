using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class Games : MonoBehaviour {

	public GameObject nextPageButton;
	public GameObject prevPageButton;

	private int page = 1;
	private GameObject[] gamePanels;

	void Start () {
		gamePanels = GameObject.FindGameObjectsWithTag ("GamePanelPrefab");
		PhotonNetwork.ConnectUsingSettings("0.1");
	}

	void OnJoinedLobby() {
		StartCoroutine ("UpdateGameRooms");
		if (PhotonNetwork.GetRoomList ().Length > gamePanels.Length) {
			nextPageButton.SetActive (true);
		}
	}

	IEnumerator UpdateGameRooms() {
		while (true) {
			updateButtons ();
			for (int i = 0; i < gamePanels.Length; i++) {
				if(gamePanels.Length * (page - 1) + i < PhotonNetwork.GetRoomList().Length) {
					gamePanels[i].GetComponent<Game>().setInfo (PhotonNetwork.GetRoomList()[gamePanels.Length * (page - 1) + i]);
				}
				else {
					gamePanels[i].GetComponent<Game>().clear ();
				}
			}
			yield return new WaitForSeconds (2);
		}
	}

	public int getPages() {
		return (int) Math.Max (Math.Ceiling(PhotonNetwork.GetRoomList ().Length * 1.0 / gamePanels.Length), 1);
	}

	public void nextPage() {
		page++;
		updateButtons ();
	}

	public void prevPage() {
		page--;
		updateButtons ();
	}

	public void updateButtons () {
		if(page < 1) {
			page = 1;
		}
		if(page > getPages ()) {
			page = getPages ();
		}
		if (page == 1) {
			prevPageButton.SetActive (false);
		} else {
			prevPageButton.SetActive (true);
		}
		if (page == getPages ()) {
			nextPageButton.SetActive (false);
		} else {
			nextPageButton.SetActive (true);
		}
	}
}
