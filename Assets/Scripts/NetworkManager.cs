using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : Photon.PunBehaviour {

    private const string roomName = "RoomName";
    private RoomInfo[] roomsList;
    public List<PhotonPlayer> curPlayersInRoom = new List<PhotonPlayer>();
    public string playerName = "defaultName";

	// Use this for initialization
	void Start () {

        PhotonNetwork.logLevel = PhotonLogLevel.ErrorsOnly;
        PhotonNetwork.ConnectUsingSettings("0.1");

		
	}
	
	// Update is called once per frame
	void Update () {
        OnRecievedRoomListUpdate();

    }

    void OnGUI()
    {
        GUI.contentColor = Color.red;
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());

        if (PhotonNetwork.room == null)
        {
            playerName = GUI.TextArea(new Rect(200, 100, 200, 30), playerName);

            //Create room
            if(GUI.Button(new Rect(200, 130, 200, 75), "Start Server"))
            {
                //Room name has to be unique, use Guid
                PhotonNetwork.CreateRoom(roomName + System.Guid.NewGuid().ToString());
            }

            if(roomsList != null)
            {
                for(int i = 0; i < roomsList.Length; i++)
                {
                    //Creating a button for each room
                    if(GUI.Button(new Rect(200, 240+(110*i), 200, 100), "Join" + roomsList[i].Name.ToString()))
                    {

                        PhotonNetwork.JoinRoom(roomsList[i].Name);

                    }
                }
            }
        }
    }

    void OnRecievedRoomListUpdate()
    {
        roomsList = PhotonNetwork.GetRoomList();
    }

    void ConnectedToMaster()
    {

    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Entered lobby");
    }

    void OnJoinedRoom()
    {
        Debug.Log("Joined room");

        //Instaciate player

        GameObject player = PhotonNetwork.Instantiate("PlayerPreFab", new Vector3(0, 1.5f, -40), Quaternion.Euler(0, 180, 0), 0);
    }

    void OnPlayerDisc(NetworkPlayer other)
    {
        
    }

    void OnCreatedRoom()
    {

    }
}
