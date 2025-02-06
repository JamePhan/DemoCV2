using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager2 : MonoBehaviour
{
    [Header("Reference")]
    public CharacterBehaviour2  _charBehaviour;
    
    [Header("Other")]
    public Character _character;

    public void Init(Character mainCharacter)
    {
        this._character = mainCharacter;
        this._charBehaviour.Init(mainCharacter);
    }
}
