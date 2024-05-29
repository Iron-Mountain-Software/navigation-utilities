using UnityEngine;
using UnityEngine.AI;

namespace IronMountain.NavigationUtilities
{
    public class NavMeshAgentFollowTransform : MonoBehaviour
    {
        public enum RotationType
        {
            None,
            LookAtTarget,
            AlignWithTarget,
        }
        
        [SerializeField] public NavMeshAgent navMeshAgent;
        [SerializeField] public Transform target;
        [SerializeField] public RotationType rotationType = RotationType.LookAtTarget;
        [SerializeField] [Range(0, 12)] public float rotationSpeed = 6;
        [SerializeField] [Range(0, 180)] public float idealAngleRange = 6;
        [SerializeField] [Range(0, 180)] public float maximumAngleRange = 6;

        private bool _isRotating;
        
        public bool HasRemainingDistance => 
            navMeshAgent
            && navMeshAgent.hasPath
            && navMeshAgent.remainingDistance >= navMeshAgent.stoppingDistance;

        private void OnValidate()
        {
            maximumAngleRange = Mathf.Clamp(maximumAngleRange, 0, 180);
            idealAngleRange = Mathf.Clamp(idealAngleRange, 0, maximumAngleRange);
            if (!navMeshAgent) navMeshAgent = GetComponent<NavMeshAgent>();
        }

        private void Awake() => OnValidate();

        private void Update()
        {
            if (!navMeshAgent) return;
            
            if (!target)
            {
                navMeshAgent.ResetPath();
                return;
            }

            if (Vector3.Distance(transform.position, target.position) <= navMeshAgent.stoppingDistance)
            {
                navMeshAgent.ResetPath();

                Vector3 currentDirection = Vector3.ProjectOnPlane(transform.forward, Vector3.up).normalized;
                Vector3 idealDirection = rotationType switch
                {
                    RotationType.AlignWithTarget => Vector3.ProjectOnPlane(target.forward, Vector3.up).normalized,
                    RotationType.LookAtTarget => Vector3.ProjectOnPlane(target.position - transform.position, Vector3.up).normalized,
                    _ => Vector3.zero
                };
                
                if (idealDirection == Vector3.zero || currentDirection == Vector3.zero) return;
                
                float angle = Vector3.Angle(idealDirection, currentDirection);
                _isRotating = angle > maximumAngleRange || (_isRotating && angle > idealAngleRange);
                if (_isRotating)
                {
                    Quaternion idealRotation = Quaternion.LookRotation(idealDirection, Vector3.up);
                    transform.rotation = Quaternion.Slerp(transform.rotation, idealRotation, Time.deltaTime * rotationSpeed);
                }
            }
            else if (navMeshAgent.destination != target.position)
            {
                navMeshAgent.SetDestination(target.position);
            }
        }
    }
}