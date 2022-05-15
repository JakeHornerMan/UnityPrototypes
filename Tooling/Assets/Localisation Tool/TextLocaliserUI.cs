using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextLocaliserUI : MonoBehaviour
{
    TextMeshProUGUI textField;

    public LocalisedString localisedString;

    void Start()
    {
        textField = GetComponent<TextMeshProUGUI>();
        //string value = LocalisationSystem.GetLocalisedValue(key);
        textField.text = localisedString.value;
    }
}
