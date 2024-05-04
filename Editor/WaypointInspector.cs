using System;
using UnityEditor;
using UnityEngine;

namespace IronMountain.NavigationUtilities.Editor
{
    [CustomEditor(typeof(Waypoint), true)]
    public class WaypointInspector : NavigationElementInspector
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.BeginHorizontal();
            bool drawDiscsBefore = Waypoint.DrawShapes;
            Waypoint.DrawShapes = EditorGUILayout.Toggle("Draw Shapes", drawDiscsBefore);
            if (drawDiscsBefore != Waypoint.DrawShapes) SceneView.RepaintAll();
            Color shapeColorABefore = Waypoint.ShapeColorA;
            Waypoint.ShapeColorA = EditorGUILayout.ColorField(shapeColorABefore);
            if (shapeColorABefore != Waypoint.ShapeColorA) SceneView.RepaintAll();
            Color shapeColorBBefore = Waypoint.ShapeColorB;
            Waypoint.ShapeColorB = EditorGUILayout.ColorField(shapeColorBBefore);
            if (shapeColorBBefore != Waypoint.ShapeColorB) SceneView.RepaintAll();
            EditorGUILayout.EndHorizontal();
            
            float radius = Waypoint.Radius;
            Waypoint.Radius = EditorGUILayout.Slider("Radius", radius, .05f, 1f);
            if (Math.Abs(radius - Waypoint.Radius) > .001f) SceneView.RepaintAll();
        }
    }
}