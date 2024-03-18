using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;

[InitializeOnLoad]
public static class OnEnterPlaySystem
{
    static OnEnterPlaySystem()
    {
        EditorApplication.playModeStateChanged += OnPlayModeStateChaged; 
    }

    static void OnPlayModeStateChaged(PlayModeStateChange state)
    {
        if (state != PlayModeStateChange.ExitingEditMode)
            return;

        foreach (FieldInfo field in TypeCache.GetFieldsWithAttribute<OnEnterPlay_BaseAttribute>())
        {
            foreach (OnEnterPlay_BaseAttribute attr in (OnEnterPlay_BaseAttribute[])field.GetCustomAttributes(typeof(OnEnterPlay_BaseAttribute), false))
            {
                //Debug.Log("Field "+field.Name);
                attr.OnEnterPlay(field);
                break;
            }
        }

        foreach (MethodInfo method in TypeCache.GetMethodsWithAttribute<OnEnterPlay_BaseAttribute>())
        {
            foreach (OnEnterPlay_BaseAttribute attr in (OnEnterPlay_BaseAttribute[])method.GetCustomAttributes(typeof(OnEnterPlay_BaseAttribute), false))
            {
                //Debug.Log("Method " + method.Name);
                attr.OnEnterPlay(method);
                break;
            }
        }
    }
}
