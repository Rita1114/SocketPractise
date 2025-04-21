using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  

public class ChatUI : MonoBehaviour
{
    public static ChatUI instance;
    [Header("UI")]
    public Transform messageContainer;
    public GameObject messageprefab;


    public InputField nameInputField;
    public string userName;
    public InputField messageInputField;
    [SerializeField] Button sendbtn;

    public void Awake()
    {
        instance = this;
        sendbtn.onClick.AddListener(()=>OnSendClick());
    }
    public void OnEnterChat()
    {
        userName = nameInputField.text;
        nameInputField.interactable = false;
    }
    public void OnSendClick()
    {
        OnEnterChat();
        if (!string.IsNullOrWhiteSpace(nameInputField.text))
        {
            string currentName = nameInputField.text;
            Client.instance.SendMessageToServer(currentName, messageInputField.text);
            messageInputField.text = "";
        }
    }
    public void AddMessage(string user, string message)
    {
        GameObject newMsg = Instantiate(messageprefab, messageContainer);
        Text text =newMsg.GetComponent<Text>();
        text.text = $"<b>{user}:</b> {message}";
    }
}
