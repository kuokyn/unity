using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Chat;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Pun.Demo.Cockpit;
using UnityEngine.UI;
using TMPro;

public class PhotonChatManager : MonoBehaviour, IChatClientListener
{

    ChatClient chatClient;
    bool isConnected;
    public GameObject joinChatButton;
	public string username;
    public GameObject chatPanel;
    string privateReciever = "";
    string currentChat;
    public TMP_InputField chatField;
    public TextMeshProUGUI chatDisplay;

	// Update is called once per frame
	void Update()
	{
        if (isConnected)
        {
            chatClient.Service();
        }

        if (chatField.text != "" && Input.GetKey(KeyCode.Return))
        {
            SubmitPublicChatOnClick();
		    SubmitPrivateChatOnClick();
		}
		chatClient.Service();
	}

    public void UsernameOnValueChange(string valueIn)
    {
		username = valueIn;
    }

    public void ChatConnectOnClick()
    {
        isConnected = true;
        chatClient = new ChatClient(this);
		chatClient.Connect(PhotonNetwork.PhotonServerSettings.AppSettings.AppIdChat, PhotonNetwork.AppVersion, new AuthenticationValues(username));
        Debug.Log("User " + username + " connecting");
	}

    public void TypeChatOnValueChange(string valeueIn)
    {
        currentChat = valeueIn;
    }

    public void SubmitPublicChatOnClick()
    {
        if (privateReciever == "")
        {
            chatClient.PublishMessage("RegionChannel", currentChat);
            chatField.text = "";
            currentChat = "";
        }
    }

    public void SubmitPrivateChatOnClick()
    {
        if (privateReciever != "")
        {
            chatClient.SendPrivateMessage(privateReciever, currentChat);
            chatField.text = "";
            currentChat = "";
        }
    }

    public void ReceiverOnValueChange(string valueIn)
    {
        privateReciever = valueIn;
    }

    public void OnConnected()
    {
		Debug.Log("User " + username + " connected");
        joinChatButton.SetActive(false);
        chatClient.Subscribe(new string[] { "RegionChannel" });
    }
    public void OnGetMessages(string channelName, string[] senders, object[] messages)
        {
            string msgs = "";
            for (int i = 0; i < senders.Length; i++)
            {
                msgs = string.Format("{0}: {1}", senders[i], messages[i]);
                chatDisplay.text += "\n" + msgs;
                Debug.Log(msgs);
            }
        }

     public void OnPrivateMessage(string sender, object message, string channelName)
     {
           string msgs = "";
           msgs = string.Format("(Private) {0}: {1}", sender, message);
           chatDisplay.text += "\n" + msgs;
           Debug.Log(msgs);
     }


    public void OnDisconnected()
    {
        throw new System.NotImplementedException();
    }


	// inherited methods
	public void DebugReturn(DebugLevel level, string message)
	{
		// throw new System.NotImplementedException();
	}

	public void OnChatStateChange(ChatState state)
	{
		// throw new System.NotImplementedException();
	}


	public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {
        throw new System.NotImplementedException();
    }

    public void OnSubscribed(string[] channels, bool[] results)
    {
        chatPanel.SetActive(true);
    }

    public void OnUnsubscribed(string[] channels)
    {
        throw new System.NotImplementedException();
    }

    public void OnUserSubscribed(string channel, string user)
    {
        throw new System.NotImplementedException();
    }

    public void OnUserUnsubscribed(string channel, string user)
    {
        throw new System.NotImplementedException();
    }

    
}
