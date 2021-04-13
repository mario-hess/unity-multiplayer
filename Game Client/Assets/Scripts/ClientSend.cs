using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientSend : MonoBehaviour
{
    private static void SendTCPData(Packet _packet)
    {
        _packet.WriteLength();
        Client.instance.tcp.SendData(_packet);
    }

    private static void SendUDPData(Packet _packet)
    {
        _packet.WriteLength();
        Client.instance.udp.SendData(_packet);
    }

    #region Packets
    public static void Login()
    {
        using (Packet _packet = new Packet((int)ClientPackets.Login))
        {
            _packet.Write(Client.instance.myId);
            _packet.Write(UIManager.instance.emailField.text);
            _packet.Write(UIManager.instance.passwordField.text);

            SendTCPData(_packet);
        }
    }

    public static void PlayerMovement(bool[] _inputs)
    {
        using (Packet _packet = new Packet((int)ClientPackets.playerMovement))
        {
            _packet.Write(_inputs.Length);
            foreach (bool _input in _inputs)
            {
                _packet.Write(_input);
            }
            _packet.Write(GameManager.players[Client.instance.myId].transform.position);

            SendUDPData(_packet);
        }
    }

    public static void CreateCharacter(int _currentOption, string _characterName )
    {
        int currentOption = _currentOption;
        string characterName = _characterName;
        
        using (Packet _packet = new Packet((int)ClientPackets.createCharacter))
        {
            _packet.Write(currentOption);
            _packet.Write(characterName);

            SendTCPData(_packet);
        }
        
    }

    public static void SendIntoGame(int _itemIndex)
    {
        int itemIndex = _itemIndex;

        using (Packet _packet = new Packet((int)ClientPackets.sendIntoGame))
        {
            _packet.Write(itemIndex);

            SendTCPData(_packet);
        }
    }

    #endregion
}