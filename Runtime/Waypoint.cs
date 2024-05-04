using UnityEditor;
using UnityEngine;

namespace IronMountain.NavigationUtilities
{
    public class Waypoint : NavigationElement
    {
        protected virtual void OnEnable() => NavigationManager.Register(this);
        protected virtual void OnDisable() => NavigationManager.Unregister(this);

#if UNITY_EDITOR
    
        public static bool DrawShapes = true;
        public static Color ShapeColorA = new Color(1, 0, 0, .9f);
        public static Color ShapeColorB = new Color(1, 1, 1, .8f);
        public static float Radius = .08f;

        protected override void OnDrawGizmos()
        {
            base.OnDrawGizmos();
            if (DrawShapes)
            {
                float shapeRadius = Radius * .6f;
            
                Gizmos.color = ShapeColorA;
                Gizmos.DrawSphere(transform.position, shapeRadius);
                Handles.color = ShapeColorB;
                Handles.DrawWireDisc(transform.position, transform.up, shapeRadius, 3f);
            
                Handles.color = Color.blue;
                Handles.DrawLine(transform.position + transform.forward * shapeRadius, transform.position + transform.forward * Radius, 3);
                Handles.color = Color.red;
                Handles.DrawLine(transform.position + transform.right * shapeRadius, transform.position + transform.right * Radius, 3);
                Handles.color = Color.green;
                Handles.DrawLine(transform.position + transform.up * shapeRadius, transform.position + transform.up * Radius, 3);
            }
        }
    
#endif
    }
}
