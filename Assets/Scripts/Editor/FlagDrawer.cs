using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(Flag))]
public class FlagDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        //base.OnGUI(position, property, label);

        // Draw label
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        // Calculate rects
        Rect buttonRect = new Rect(position.x, position.y, 20, position.height);
        Rect stringRect = new Rect(position.x + 25, position.y, 100, position.height);
        Rect labelRect = new Rect(position.x + 130, position.y, 50, position.height);
        Rect whitelistRect = new Rect(position.x + 185, position.y, position.width - 185, position.height);

        //button for quick string filling from FlagManager
        if (GUI.Button(buttonRect, ""))
        {

        }

        //string field
        EditorGUI.PropertyField(stringRect, property.FindPropertyRelative("flag"), GUIContent.none);

        //whitelist label
        EditorGUI.LabelField(labelRect, "Whitelist");

        //whitelist checkbox
        EditorGUI.PropertyField(whitelistRect, property.FindPropertyRelative("whitelist"), GUIContent.none);

        EditorGUI.EndProperty();
    }
}
