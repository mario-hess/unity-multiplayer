using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterObject
{
    public string _id;
    public string characterName;
    public int characterClass;
    public static CharacterObject createFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<CharacterObject>(jsonString);
    }
}