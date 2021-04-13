using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public static Dictionary<int, PlayerManager> players = new Dictionary<int, PlayerManager>();


    public List<GameObject> options = new List<GameObject>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    public void SpawnPlayer(int _id, string _username, int _characterClass, Vector2 _position, Quaternion _rotation)
    {
        
        GameObject _player;
        if (_id == Client.instance.myId)
        {
            Loader.LoadLevel(Loader.Scene.Main, Loader.Scene.LoadingSpinner);
            _player = Instantiate(options[_characterClass], _position, _rotation);
            Camera.main.GetComponent<CameraController>().setTarget(_player.transform);
        }
        else
        {
            _player = Instantiate(options[_characterClass], _position, _rotation);
        }

        _player.GetComponent<PlayerManager>().id = _id;
        _player.GetComponent<PlayerManager>().username = _username;
        players.Add(_id, _player.GetComponent<PlayerManager>());
        
    }

}