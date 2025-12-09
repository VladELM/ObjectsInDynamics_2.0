using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent (typeof(TMP_Text))]
public class NumberViewer : MonoBehaviour
{
    private TMP_Text _text;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
    }

    public void AssigneText(int number)
    {
        _text.text = Convert.ToString(number);
    }

    public void AssigneText()
    {
        _text.text = Convert.ToString(Convert.ToInt32(_text.text) + 1);
    }
}
