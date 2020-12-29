using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AgentMovement : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private Camera currentCamera;
    
    // Start is called before the first frame update
    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        currentCamera = Camera.current;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            SetDirectionByMouse();
        }
    }

    private void SetDirectionByMouse()
    {
        var mouse = Input.mousePosition;
        var ray = currentCamera.ScreenPointToRay(mouse, Camera.MonoOrStereoscopicEye.Mono);
        if (Physics.Raycast(ray.origin, ray.direction, out var hit, Mathf.Infinity))
        {
            navMeshAgent.SetDestination(hit.point);
        }
    }

    private void OnDrawGizmos()
    {
        var ray = currentCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray.origin, ray.direction, out var hit, Mathf.Infinity))
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawSphere(hit.point, .5f);
            Gizmos.color = Color.green;
            Gizmos.DrawLine(currentCamera.transform.position, hit.point);
        }
    }
}
