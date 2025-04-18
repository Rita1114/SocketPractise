using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  

public class ChatUI : MonoBehaviour
{
    public InputField nameInputField;
    public InputField messageInputField;
    [SerializeField] Button sendbtn;

    public void Awake()
    {
        sendbtn.onClick.AddListener(()=>OnSendClick());
    }
    public void OnSendClick()
    {
        string name = nameInputField.text;
        string message = messageInputField.text;

        Client.instance.SendMessageToServer(name, message);
        messageInputField.text = "";
    }
}
