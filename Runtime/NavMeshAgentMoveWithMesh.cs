using UnityEngine;
using UnityEngine.AI;
using Object = UnityEngine.Object;

namespace IronMountain.NavigationUtilities
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class NavMeshAgentMoveWithMesh : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent navMeshAgent;

        private Object _navMesh;
        private Transform _navMeshTransform;
        private Vector3 _previousMeshPosition;
        private Quaternion _previousMeshRotation;
        
        private Object NavMesh
        {
            get => _navMesh;
            set
            {
                if (_navMesh == value) return;
                _navMesh = value;
                if (_navMesh is Component component)
                {
                    _navMeshTransform = component.transform;
                    _previousMeshPosition = _navMeshTransform.position;
                    _previousMeshRotation = _navMeshTransform.rotation;
                }
                else _navMeshTransform = null;
            }
        }

        private void OnValidate()
        {
            if (!navMeshAgent) navMeshAgent = GetComponent<NavMeshAgent>();
        }

        private void Awake() => OnValidate();

        private void Update()
        {
            NavMesh = navMeshAgent ? navMeshAgent.navMeshOwner : null;

            if (!navMeshAgent || !_navMeshTransform) return;
            
            Vector3 platformMovement = _navMeshTransform.position - _previousMeshPosition;
            if (platformMovement != Vector3.zero)
            {
                navMeshAgent.Move(platformMovement);
            }

            Quaternion platformRotationDelta = _navMeshTransform.rotation * Quaternion.Inverse(_previousMeshRotation);
            if (platformRotationDelta != Quaternion.identity)
            {
                Vector3 localPosition = _navMeshTransform.InverseTransformPoint(navMeshAgent.transform.position);
                Vector3 rotatedPosition = _navMeshTransform.TransformPoint(platformRotationDelta * localPosition);
                navMeshAgent.Move(rotatedPosition - navMeshAgent.transform.position);
                navMeshAgent.transform.rotation = _navMeshTransform.rotation * Quaternion.Inverse(_previousMeshRotation) * navMeshAgent.transform.rotation;
            }

            _previousMeshPosition = _navMeshTransform.position;
            _previousMeshRotation = _navMeshTransform.rotation;
        }
    }
}
