using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class ObjectName : MonoBehaviour
{
    public void Initialize(string objectName)
    {
        TMP_Text text = GetComponent<TMP_Text>();
        text.text = objectName;
    }
}
