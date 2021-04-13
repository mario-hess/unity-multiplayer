using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Security.Cryptography;
using System;
using System.Text;

public class Requests : MonoBehaviour
{
    private static string baseUrl = "http://127.0.0.1:3000/";
    private static string authLoginUrl = "auth/login";
    private static string characterCreateUrl = "character/create";
    private static string fetchCharacterDataUrl = "character/fetchCharacterData";



    public void LoginRequest(int _fromClient, string _email, string _password)
    {
        int clientId = _fromClient;
        string email = _email;
        string password = _password;

        RequestHeader clientHeader = new RequestHeader
        {
            Key = "Content-Type",
            Value = "application/json"
        };

        LoginObject loginObject = new LoginObject
        {
            email = email,
            password = password
        };

        StartCoroutine(RestWebClient.Instance.HttpPost(baseUrl + authLoginUrl, JsonUtility.ToJson(loginObject), (r) => OnLoginRequestComplete(r, clientId), new List<RequestHeader>{
            clientHeader
        }));
    }

    void OnLoginRequestComplete(Response response, int _clientId)
    {
        Debug.Log($"Status Code: {response.StatusCode}");
        Debug.Log($"Data: {response.Data}");
        Debug.Log($"Error: {response.Error}");


        if (!string.IsNullOrEmpty(response.Error) && response.StatusCode != 200)
        {
            ServerSend.LoginFailed(_clientId);
        }
        else if (string.IsNullOrEmpty(response.Error) && !string.IsNullOrEmpty(response.Data) && response.StatusCode == 200)
        {

            LoginObject loginObject = LoginObject.createFromJSON(response.Data);

            Server.clients[_clientId].objectId = loginObject._id;


            if (loginObject.characters.Length == 0)
            {
                ServerSend.ToCharacterCreation(_clientId);
            }
            if (loginObject.characters.Length > 0)
            {

                ServerSend.ToCharacterSelection(_clientId, loginObject);
            }

        }
    }
    
    public void CreateCharacterRequest(int _fromClient, int _classOption, string _characterName)
    {
        int clientId = _fromClient;
        int classOption = _classOption;
        string characterName = _characterName;

        string clientObjectId = Server.clients[clientId].objectId;




        RequestHeader clientHeader = new RequestHeader
        {
            Key = "Content-Type",
            Value = "application/json"
        };

        CharacterLoginObject character = new CharacterLoginObject
        {
            characterName = characterName,
            characterClass = classOption
        };

        List<CharacterLoginObject> chars = new List<CharacterLoginObject>();

        chars.Add(character);

        CharacterLoginObject[] characters = chars.ToArray();

        LoginObject characterCreate = new LoginObject
        {
            _id = clientObjectId,
            characters = characters
        };

        
        
        
        StartCoroutine(RestWebClient.Instance.HttpPut(baseUrl + characterCreateUrl, JsonUtility.ToJson(characterCreate), (r) => OnCreateCharacterRequestComplete(r, clientId), new List<RequestHeader>{
            clientHeader
        }));
        
    }
    
    void OnCreateCharacterRequestComplete(Response response, int _clientId)
    {
        Debug.Log($"Status Code: {response.StatusCode}");
        Debug.Log($"Data: {response.Data}");
        Debug.Log($"Error: {response.Error}");

        
        if (!string.IsNullOrEmpty(response.Error) && response.StatusCode != 201)
        {
            Debug.Log("Failed");
        }
        else if (string.IsNullOrEmpty(response.Error) && !string.IsNullOrEmpty(response.Data) && response.StatusCode == 201)
        {

            LoginObject userData = LoginObject.createFromJSON(response.Data);

            ServerSend.ToCharacterSelection(_clientId, userData);
        }
       
    }
    

    public void FetchCharacterDataRequest(int _fromClient, int _characterIndex)
    {
        int clientId = _fromClient;
        int characterIndex = _characterIndex;

        string clientObjectId = Server.clients[clientId].objectId;

        RequestHeader clientHeader = new RequestHeader
        {
            Key = "Content-Type",
            Value = "application/json"
        };

        FetchCharacterObject fetchCharacter = new FetchCharacterObject
        {
            _id = Server.clients[clientId].objectId,
            characterIndex = characterIndex
        };

        StartCoroutine(RestWebClient.Instance.HttpPost(baseUrl + fetchCharacterDataUrl, JsonUtility.ToJson(fetchCharacter), (r) => OnFetchCharacterDataRequestComplete(r, clientId), new List<RequestHeader> {
            clientHeader
        }));
    }

    void OnFetchCharacterDataRequestComplete(Response response, int _clientId)
    {
        int clientId = _clientId;
        Debug.Log($"Status Code: {response.StatusCode}");
        Debug.Log($"Data: {response.Data}");
        Debug.Log($"Error: {response.Error}");


        if (!string.IsNullOrEmpty(response.Error) && response.StatusCode != 201)
        {
            Debug.Log("Failed");
        }
        else if (string.IsNullOrEmpty(response.Error) && !string.IsNullOrEmpty(response.Data) && response.StatusCode == 201)
        {
            
            CharacterObject character = CharacterObject.createFromJSON(response.Data);

            Server.clients[clientId].SendIntoGame(character.characterName, character._id, character.characterClass);
        }
    }
    
}
