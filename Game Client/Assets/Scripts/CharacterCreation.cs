using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCreation : MonoBehaviour
{
    public static CharacterCreation instance;

    [Header("Sprite to change")]
    public SpriteRenderer bodyPart;

    [Header("Sprite to cycle through")]
    public List<Sprite> options = new List<Sprite>();

    public TextMeshProUGUI className;

    [Header("Character Name")]
    public InputField charName;

    public int currentOption = 0;

    private string[] classNames = {"Fireformer", "Tideweaver", "Stoneshaper", "Stormcaller" };

    

    private void Start()
    {
        className.text = "Fireformer";
    }

    public void NextOption()
    {
        currentOption++;
        if(currentOption >= options.Count)
        {
            currentOption = 0;
        }

        bodyPart.sprite = options[currentOption];
        className.text = classNames[currentOption];
    }

    public void PreviousOption()
    {
        currentOption--;
        if(currentOption < 0)
        {
            currentOption = options.Count -1;
        }

        bodyPart.sprite = options[currentOption];
        className.text = classNames[currentOption];
    }

    public void createCharacter()
    {
        string characterName = charName.text;
        ClientSend.CreateCharacter(currentOption, characterName);

        Loader.LoadLevel(Loader.Scene.LoadingSpinner, Loader.Scene.CharacterCreation);
    }

}
