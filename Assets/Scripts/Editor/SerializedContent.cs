using UnityEngine;
using UnityEditor;
using System.Collections;

public struct SerializedContent {
	public SerializedProperty prop;
	public GUIContent guiContent;
		
	public SerializedContent(SerializedProperty prop, GUIContent guiContent) {
		this.prop = prop;
		this.guiContent = guiContent;
	}
}