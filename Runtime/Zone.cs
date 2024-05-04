using UnityEngine;

namespace IronMountain.NavigationUtilities
{
    [RequireComponent(typeof(Collider))]
    public class Zone : NavigationElement
    {
        [SerializeField] private Collider collider;

        public Collider Collider => collider;
        
        protected virtual void OnEnable() => NavigationManager.Register(this);
        protected virtual void OnDisable() => NavigationManager.Unregister(this);
            
        protected override void OnValidate()
        {
            base.OnValidate();
            if (!collider) collider = GetComponent<Collider>();
            if (collider) collider.isTrigger = true;
        }
        
#if UNITY_EDITOR
    
        public static bool DrawShapes = true;
        public static Color Color = new Color(1, 0, 0, .5f);

        protected override void OnDrawGizmos()
        {
            base.OnDrawGizmos();
            if (DrawShapes && collider)
            {
                if (collider is BoxCollider boxCollider)
                {            
                    Gizmos.matrix = transform.localToWorldMatrix;
                    Gizmos.color = Color;
                    Gizmos.DrawCube(boxCollider.center, boxCollider.size);
                }
            }
        }
    
#endif
    }
}
