using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CompositeBehavior))]
public class CompositeBehaviorEditor : Editor
{
    public override void OnInspectorGUI() {
        CompositeBehavior cb = (CompositeBehavior)target;
        EditorGUILayout.BeginHorizontal();
        if (cb.behaviors == null || cb.behaviors.Length == 0) {
            EditorGUILayout.HelpBox("No Behaviors!", MessageType.Warning);
            EditorGUILayout.EndHorizontal();
        }
        else {
            EditorGUILayout.LabelField("Behaviors");
            EditorGUILayout.LabelField("Weights", GUILayout.Width(55f));
            EditorGUILayout.EndHorizontal();

            EditorGUI.BeginChangeCheck();
            for(int i = 0; i < cb.behaviors.Length; i++) {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(i.ToString(), GUILayout.MaxWidth(10f));
                cb.behaviors[i] = (FlockBehavior)EditorGUILayout.ObjectField(cb.behaviors[i], typeof(FlockBehavior), false);
                cb.weights[i] = EditorGUILayout.FloatField(cb.weights[i], GUILayout.MaxWidth(30f));
                EditorGUILayout.EndHorizontal();
            }
            if (EditorGUI.EndChangeCheck()) EditorUtility.SetDirty(cb);
        }
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Add Behavior")) {
            AddBehavior(cb);
            EditorUtility.SetDirty(cb);
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        if (cb.behaviors != null && cb.behaviors.Length > 0) {
            if (GUILayout.Button("Remove Behavior")) {
                RemoveBehavior(cb);
                EditorUtility.SetDirty(cb);
            }
        }
        EditorGUILayout.EndHorizontal();
    }
    void AddBehavior(CompositeBehavior cb) {
        int n = ((cb.behaviors != null) ? cb.behaviors.Length : 0) + 1;
        FlockBehavior[] newBehaviors = new FlockBehavior[n];
        float[] newWeights = new float[n];
        for(int i = 0; i < n - 1; i++) {
            newBehaviors[i] = cb.behaviors[i];
            newWeights[i] = cb.weights[i];
        }
        newWeights[n - 1] = 1f;
        cb.behaviors = newBehaviors;
        cb.weights = newWeights;
    }
    void RemoveBehavior(CompositeBehavior cb) {
        int n = cb.behaviors.Length - 1;
        if(n == 0) {
            cb.behaviors = null;
            cb.weights = null;
            return;
        }
        FlockBehavior[] newBehaviors = new FlockBehavior[n];
        float[] newWeights = new float[n];
        for (int i = 0; i < n; i++) {
            newBehaviors[i] = cb.behaviors[i];
            newWeights[i] = cb.weights[i];
        }
        cb.behaviors = newBehaviors;
        cb.weights = newWeights;
    }
}
