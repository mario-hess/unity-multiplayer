using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClientHandle : MonoBehaviour
{
    public static void Welcome(Packet _packet)
    {
        string _msg = _packet.ReadString();
        int _myId = _packet.ReadInt();

        Debug.Log($"Message from server: {_msg}");
        Client.instance.myId = _myId;

        ClientSend.Login();
        Client.instance.udp.Connect(((IPEndPoint)Client.instance.tcp.socket.Client.LocalEndPoint).Port);
        
    }

    public static void LoginFailed(Packet _packet)
    {
        int clientId = _packet.ReadInt();

        Debug.Log("Login Failed");

        Client.instance.Disconnect();

        Loader.LoadLevel(Loader.Scene.Menu, Loader.Scene.LoadingSpinner);
    }

    public static void SpawnPlayer(Packet _packet)
    {
        int _id = _packet.ReadInt();
        string _username = _packet.ReadString();
        int _characterClass = _packet.ReadInt();
        Vector2 _position = _packet.ReadVector2();
        Quaternion _rotation = new Quaternion(0, 0, 0, 0);
        
        GameManager.instance.SpawnPlayer(_id, _username, _characterClass, _position, _rotation);
    }

    public static void PlayerPosition(Packet _packet)
    {
        int _id = _packet.ReadInt();
        Vector3 _position = _packet.ReadVector2();
        Vector2 _inputs = _packet.ReadVector2();

        if (GameManager.players.TryGetValue(_id, out PlayerManager _player))
        {
            _player.transform.position = _position;
            _player.inputsAnimation = _inputs;
        }
    }

    public static void PlayerDisconnected(Packet _packet)
    {
        int _id = _packet.ReadInt();
        Destroy(GameManager.players[_id].gameObject);
        GameManager.players.Remove(_id);
        
    }

    public static void CharacterCreation(Packet _packet)
    {
        int _clientId = _packet.ReadInt();

        Loader.LoadLevel(Loader.Scene.CharacterCreation, Loader.Scene.LoadingSpinner);
    }

    public static void CharacterSelection(Packet _packet)
    {
        bool[] _characterLength = new bool[_packet.ReadInt()];

        List<CharactersData> chars = new List<CharactersData>();

        for(int i = 0; i < _characterLength.Length; i++)
        {
            CharactersData character = new CharactersData(_packet.ReadString(), _packet.ReadInt());
            chars.Add(character);
        }

        CharacterSelectionMenu.characters = chars;

        Loader.LoadLevel(Loader.Scene.CharacterSelection, Loader.Scene.LoadingSpinner);
    }

}