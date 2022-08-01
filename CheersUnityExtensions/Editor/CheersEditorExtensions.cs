using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CheersEditorExtensions
{
    // For some reason the GUI doesn't use the same coordinate space as the regular UI
    public static Vector2 GetRegularMousePosition(this Event evt)
    {
        Vector2 mousePos = evt.mousePosition;
        mousePos *= UnityEditor.EditorGUIUtility.pixelsPerPoint;
        mousePos.y = UnityEditor.SceneView.lastActiveSceneView.camera.pixelHeight - mousePos.y;
        return mousePos;
    }
}
