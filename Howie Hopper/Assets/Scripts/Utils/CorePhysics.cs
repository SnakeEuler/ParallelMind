using UnityEngine;
public class CorePhysics: MonoBehaviour {
  // This function returns the time value on an AnimationCurve where a given value is first reached
  // at that specified time.
  // The 'greater' parameter is used to specify whether the function should search for the first
  // time the curve value is greater than or equal to the given value.

  public static float SetCurveTimeByValue (
  AnimationCurve curve, float value, float maxTime, bool greaterValues = true) {
    float curveTime = 0f;
    while ((greaterValues && curve.Evaluate (curveTime) <= value)
      || (!greaterValues && curve.Evaluate (curveTime) >= value)) {
      curveTime += Time.fixedDeltaTime;
      if (curveTime >= maxTime) {
        break;
      }
    }
    return curveTime;
  }
}
