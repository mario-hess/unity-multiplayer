using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class LoginObject
{

    public string _id;
    public string email;
    public string password;
    public CharacterLoginObject[] characters;


    public static LoginObject createFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<LoginObject>(jsonString);
    }

}

