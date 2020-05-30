using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// All kinds of helper functions
public class MeshUtils {
    // Create Text in the world
    public static TextMesh createWorldText(Transform parent, string text, Vector3 localPosition,
                                           Color color, TextAnchor textAnchor, TextAlignment textAlignment,
                                           int fontSize){
        GameObject gameObject = new GameObject("WorldText", typeof(TextMesh));

        Transform transform = gameObject.transform;
        transform.SetParent(parent, false);
        transform.localPosition = localPosition;

        TextMesh textMesh = gameObject.GetComponent<TextMesh>();
        textMesh.text = text;
        textMesh.anchor = textAnchor;
        textMesh.color = color;
        textMesh.alignment = textAlignment;
        textMesh.fontSize = fontSize;
        
        return textMesh;
    }

    // Get Mouse Position in World
    public static Vector3 GetMouseWorldPosition(){
        Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        vec.z = 0f;
        return vec;
    }
    public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera){
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }
}
