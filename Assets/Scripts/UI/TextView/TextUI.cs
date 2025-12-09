using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextUI : MonoBehaviour
{
    [SerializeField] private string _uiName;
    [SerializeField] private ObjectName _objectName;

    private void Start()
    {
        _objectName.Initialize(_uiName);
    }

#if UNITY_EDITOR
    [ContextMenu("AssigneObjectNameField")]
    private void AssigneObjectNameField()
    {
        int childAmount = transform.childCount;

        for (int i = 0; i < childAmount; i++)
        {
            Transform childTransform = transform.GetChild(i);

            if (childTransform.TryGetComponent(out ObjectName childObjectName))
            {
                _objectName = childObjectName;
                break;
            }
        }
    }

#endif
}
