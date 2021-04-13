using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ServerHandle
{
   

    public static void LoginReceived(int _fromClient, Packet _packet)
    {
        int _clientIdCheck = _packet.ReadInt();
        string _email = _packet.ReadString();
        string _password = _packet.ReadString();

        GameObject login;

        
        
        Debug.Log($"{Server.clients[_fromClient].tcp.socket.Client.RemoteEndPoint} connected successfully and is now player {_fromClient}.");
        if (_fromClient != _clientIdCheck)
        {
            Debug.Log($"Client \"{_email}\" (ID: {_fromClient}) has assumed the wrong client ID ({_clientIdCheck})!");
        }


        login = GameObject.Find("WebRequestObject");
        login.GetComponent<Requests>().LoginRequest(_fromClient, _email, _password);

    }

    public static void SendIntoGame(int _fromClient, Packet _packet)
    {
        int clientId = _fromClient;
        int characterIndex = _packet.ReadInt();

        // Fetch Character Data
        GameObject fetchCharacterData;

        fetchCharacterData = GameObject.Find("WebRequestObject");
        fetchCharacterData.GetComponent<Requests>().FetchCharacterDataRequest(clientId, characterIndex);

    }


    public static void PlayerMovement(int _fromClient, Packet _packet)
    {
        bool[] _inputs = new bool[_packet.ReadInt()];
        for (int i = 0; i < _inputs.Length; i++)
        {
            _inputs[i] = _packet.ReadBool();
        }

        Server.clients[_fromClient].player.SetInput(_inputs);
    }

    public static void CreateCharacter(int _fromClient, Packet _packet)
    {
        int _clientId = _fromClient;
        int _classOption = _packet.ReadInt();
        string _characterName = _packet.ReadString();

        GameObject createCharacter;

        createCharacter = GameObject.Find("WebRequestObject");
        createCharacter.GetComponent<Requests>().CreateCharacterRequest(_clientId, _classOption, _characterName);
    }
   
}
