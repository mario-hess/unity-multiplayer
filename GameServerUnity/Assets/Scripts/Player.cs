using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int id;
    public string username;
    public int characterClass;
    public string objectId;

    public Vector3 inputDirection;



    private float moveSpeed = 5f / Constants.TICKS_PER_SEC;
    private bool[] inputs;

    public void Initialize(int _id, string _username, string _objectId, int _characterClass)
    {
        id = _id;
        username = _username;
        objectId = _objectId;
        characterClass = _characterClass;
        inputs = new bool[4];
    }

    public void FixedUpdate()
    {
        Vector2 _inputDirection = Vector2.zero;
        if (inputs[0])
        {
            _inputDirection.y += 1;
        }
        if (inputs[1])
        {
            _inputDirection.y -= 1;
        }
        if (inputs[2])
        {
            _inputDirection.x -= 1;
        }
        if (inputs[3])
        {
            _inputDirection.x += 1;
        }

        Move(_inputDirection);
    }


    private void Move(Vector2 _inputDirection)
    {
        inputDirection = _inputDirection;
        transform.position += inputDirection * moveSpeed;

        ServerSend.PlayerPosition(this);
    }

    public void SetInput(bool[] _inputs)
    {
        inputs = _inputs;
    }
}
