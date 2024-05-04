using UnityEditor;
using UnityEngine;

namespace IronMountain.NavigationUtilities.Editor
{
    [CustomEditor(typeof(NavigationElement), true)]
    public class NavigationElementInspector : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.BeginHorizontal();
            bool drawLabelsBefore = NavigationElement.DrawLabels;
            NavigationElement.DrawLabels = EditorGUILayout.Toggle("Draw Labels", drawLabelsBefore);
            if (drawLabelsBefore != NavigationElement.DrawLabels) SceneView.RepaintAll();
            Color labelColorBefore = NavigationElement.LabelColor;
            NavigationElement.LabelColor = EditorGUILayout.ColorField(labelColorBefore);
            if (labelColorBefore != NavigationElement.LabelColor) SceneView.RepaintAll();
            EditorGUILayout.EndHorizontal();
        }
    }
}