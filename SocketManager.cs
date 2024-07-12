using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;
using Newtonsoft.Json.Linq;

public class SocketManager : MonoBehaviour
{
    WebSocket socket;
    public GameObject player;
    public PlayerData playerData;

    void Start()
    {
        socket = new WebSocket("ws://loaclhost:8080");

        socket.Connect();

        socket.OnMessage += (sender, e) =>
        {
            //If data is text
            if (e.IsText)
            {
                JObject jsonObj = JObject.Parse(e.Data);

                //Get initial ID
                if (jsonObj["id"] != null)
                {
                    //Convert JSON to player data object
                    PlayerData tempPlayerData = JsonUtility.FromJson<PlayerData>(e.Data);
                    playerData = tempPlayerData;
                    Debug.Log("player ID is " + playerData.id);
                    return;
                }
            }

        };

        //If connection is closed w/o client requesting close
        socket.OnClose += (sender, e) =>
        {
            Debug.Log(e.Code);
            Debug.Log(e.Reason);
            Debug.Log("Connection Closed!");

        };
     }

    void Update()
    {
        if (socket == null)
        {
            return;
        }

        //Send player data to server if correctly configured
        if (player != null && playerData.id != "")
        {
            //Player current position and rotation data
            playerData.xPos = player.transform.position.x.ToString();
            playerData.yPos = player.transform.position.y.ToString();
            playerData.zPos = player.transform.position.z.ToString();

            System.DateTime epochStart = new System.DateTime(1970, 1, 1, 8, 0, 0, System.DateTimeKind.Utc);
            double timestamp = (System.DateTime.UtcNow - epochStart).TotalSeconds;

            playerData.timestamp = timestamp;

            string playerDataJSON = JsonUtility.ToJson(playerData);
            socket.Send(playerDataJSON);
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            string messageJSON = "{\"message\": \"Client Message\"}";
            socket.Send(messageJSON);
        }

    }

    private void OnDestroy()
    {
        //Closes socket when exiting application
        socket.Close();
    }
}
