using System;
using UnityEditor;
using UnityEngine;

namespace IronMountain.NavigationUtilities
{
    [ExecuteAlways]
    public abstract class NavigationElement : MonoBehaviour
    {
        [SerializeField] private string displayName;
        [SerializeField] private string id;

        public virtual string ID => id;
        public virtual string DisplayName => displayName;

        protected virtual void Awake() => OnValidate();

        protected virtual void OnValidate()
        {
            ValidateID();
            ValidateName();
        }

        protected virtual void ValidateID()
        {
            if (!string.IsNullOrWhiteSpace(id)) return;
            id = Guid.NewGuid().ToString().Substring(0, 6).ToUpper();
        }
    
        protected virtual void ValidateName()
        {
            if (!string.IsNullOrWhiteSpace(displayName)) return;
            displayName = GetType().Name + " " + id;
        }
        
#if UNITY_EDITOR
    
        public static bool DrawLabels = true;
        public static Color LabelColor = Color.black;

        private static GUIStyle _guiStyle;
        private static GUIStyleState _gUIStyleState;
        private static RectOffset _rectOffset;

        private static GUIStyle GUIStyle => _guiStyle ??= new GUIStyle();
        private static GUIStyleState GUIStyleState => _gUIStyleState ??= new GUIStyleState();
        private static RectOffset RectOffset => _rectOffset ??= new RectOffset(2, 2, 0, 0);

        protected virtual void OnDrawGizmos()
        {
            if (DrawLabels)
            {
                GUIStyle.alignment = TextAnchor.UpperCenter;
                GUIStyle.fontSize = 8;
                GUIStyle.padding = RectOffset;
                GUIStyle.normal = GUIStyleState;
                GUIStyle.normal.background = Texture2D.whiteTexture;
                GUIStyleState.textColor = LabelColor;
                Handles.Label(transform.position, ID, GUIStyle);
                
                GUIStyle.alignment = TextAnchor.LowerCenter;
                GUIStyle.fontSize = 12;
                GUIStyle.padding = RectOffset;
                GUIStyle.normal = GUIStyleState;
                GUIStyle.normal.background = Texture2D.whiteTexture;
                GUIStyleState.textColor = LabelColor;
                Handles.Label(transform.position, DisplayName, GUIStyle);
            }
        }
    
#endif
    }
}
