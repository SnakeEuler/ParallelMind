using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

// A custom attribute used to specify the bounds for editing an AnimationCurve.
// bounds: xMin to xMin + xLength on x axis, yMin to yMin + yLength on y axis
public class BoundCurveAttribute: PropertyAttribute {
  public Rect bounds;
  public int height;

  // Constructor for creating a BoundCurveAttribute with specified xMin, yMin, xLength, yLength, and optional height.
  public BoundCurveAttribute (
  float xMin, float yMin, float xLength, float yLength, int height = 1) {
    this.bounds = new Rect (xMin, yMin, xLength, yLength);
    this.height = height;
  }
  // Constructor for BoundCurveAttribute class with an optional height parameter.
  public BoundCurveAttribute (int height = 1) {
    this.bounds = new Rect (0, 0, 1, 1);
    this.height = height;
  }
}
#if UNITY_EDITOR
[CustomPropertyDrawer (typeof (BoundCurveAttribute))]
public class BoundedCurveDrawer: PropertyDrawer {
  public override float GetPropertyHeight (SerializedProperty property, GUIContent label) {
    BoundCurveAttribute boundCurve = (BoundCurveAttribute)attribute;
    return EditorGUIUtility.singleLineHeight * boundCurve.height;
  }

  public override void OnGUI (Rect position, SerializedProperty property, GUIContent label) {
    BoundCurveAttribute boundCurve = (BoundCurveAttribute)attribute;

    EditorGUI.BeginProperty (position, label, property);
    property.animationCurveValue = EditorGUI.CurveField (position, label,
    property.animationCurveValue, Color.white, boundCurve.bounds);
    EditorGUI.EndProperty ();
  }
}
#endif
