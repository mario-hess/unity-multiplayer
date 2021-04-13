using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class ButtonExtension
{
    public static void AddEventListener<T>(this Button button, T param, Action<T> OnClick)
    {
        button.onClick.AddListener(delegate ()
       {
           OnClick(param);
       });
    }
}
public class CharacterSelectionMenu : MonoBehaviour
{

    private string[] classes = { "Fireformer", "Tideweaver", "Stoneshaper", "Stormcaller" }; 

    public SpriteRenderer sprite;
    public List<Sprite> options = new List<Sprite>();

    public static List<CharactersData> characters = new List<CharactersData>();
    private void Start()
    {
        GameObject buttonTemplate = transform.GetChild(0).gameObject;
        GameObject g;
        for (int i = 0; i < characters.Count; i++)
        {
            g = Instantiate(buttonTemplate, transform);
            g.transform.GetChild(0).GetComponent<Text>().text = characters[i].characterName;
            g.transform.GetChild(1).GetComponent<Text>().text = classes[characters[i].characterClass];
            g.transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = options[characters[i].characterClass];

            g.GetComponent<Button>().AddEventListener(i, ItemClicked);
        }
        Destroy(buttonTemplate);
    }

    public void createNewCharacter()
    {
        Loader.LoadLevel(Loader.Scene.CharacterCreation, Loader.Scene.CharacterSelection);
    }

   void ItemClicked(int _itemIndex)
    {
        ClientSend.SendIntoGame(_itemIndex);
        Loader.LoadLevel(Loader.Scene.LoadingSpinner, Loader.Scene.CharacterSelection);
    }
}
