using UnityEditor;
using UnityEngine;

namespace IronMountain.NavigationUtilities.Editor
{
    [CustomEditor(typeof(Zone), true)]
    public class ZoneInspector : NavigationElementInspector
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.BeginHorizontal();
            bool drawShapesBefore = Zone.DrawShapes;
            Zone.DrawShapes = EditorGUILayout.Toggle("Draw Shapes", drawShapesBefore);
            if (drawShapesBefore != Zone.DrawShapes) SceneView.RepaintAll();
            Color colorBefore = Zone.Color;
            Zone.Color = EditorGUILayout.ColorField(colorBefore);
            if (colorBefore != Zone.Color) SceneView.RepaintAll();
            EditorGUILayout.EndHorizontal();
        }
    }
}