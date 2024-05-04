using UnityEngine;
using UnityEngine.AI;

namespace IronMountain.NavigationUtilities
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private GameObject pointer;
        [SerializeField] private Object navMeshOwner;
        [SerializeField] private NavMeshAgent navMeshAgent;
        [SerializeField] private Camera camera;

        private Object NavMeshOwner
        {
            get => navMeshOwner;
            set
            {
                if (navMeshOwner == value) return;
                navMeshOwner = value;
                if (navMeshOwner is Component component)
                {
                    transform.parent = component.transform;
                }
                else transform.parent = null;
            }
        }

        private void OnValidate()
        {
            if (!camera) camera = Camera.main;
            if (!navMeshAgent) navMeshAgent = GetComponent<NavMeshAgent>();
        }

        private void Awake()
        {
            OnValidate();
            pointer = new GameObject("Pointer");
        }

        private void Update()
        {
            if (!navMeshAgent) return;
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = camera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    pointer.transform.position = hit.point;
                    pointer.transform.parent = hit.transform;
                }
            }
            navMeshAgent.SetDestination(pointer.transform.position);
            NavMeshOwner = navMeshAgent.navMeshOwner;
        }
    }
}
