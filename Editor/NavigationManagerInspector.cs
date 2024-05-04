using UnityEditor;
using UnityEngine;

namespace IronMountain.NavigationUtilities.Editor
{
    [CustomEditor(typeof(NavigationManager))]
    public class NavigationManagerInspector : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            EditorGUILayout.LabelField("Waypoints");
            foreach (Waypoint waypoint in NavigationManager.Waypoints)
            {
                if (!waypoint) continue;
                if (GUILayout.Button(waypoint.DisplayName))
                {
                    EditorGUIUtility.PingObject(waypoint);
                }
            }
            
            EditorGUILayout.LabelField("Zones");
            foreach (Zone zone in NavigationManager.Zones)
            {
                if (!zone) continue;
                if (GUILayout.Button(zone.DisplayName))
                {
                    EditorGUIUtility.PingObject(zone);
                }
            }
            
            EditorGUILayout.LabelField("Trackers");
            foreach (ZoneTracker tracker in NavigationManager.Trackers)
            {
                if (!tracker) continue;
                if (GUILayout.Button(tracker.DisplayName))
                {
                    EditorGUIUtility.PingObject(tracker);
                }
            }
        }
    }
}