using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Renderer))]
public class DrawGizmoPlane : MonoBehaviour
{
    private Renderer render;
    [Range(0f, 1f)] public float alpha = .5f;
    private void Awake() {
        render = GetComponent<Renderer>();
    }
    private void OnDrawGizmos()
    {
        if (render is null) 
            render = GetComponent<Renderer>();
        
        FindCornerPosition();
        FindMidPointFromCenter();
    }

    private void FindCornerPosition() {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(render.bounds.max, .15f);
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(render.bounds.min, .15f);
        Gizmos.color = Color.cyan;

        float maxZ = (render.bounds.max.z * -1) + (transform.position.z * 2);
        float minZ = (render.bounds.min.z * -1) + (transform.position.z * 2);
        
        var maxReverse = new Vector3(render.bounds.max.x, render.bounds.max.y, maxZ);
        var minReverse = new Vector3(render.bounds.min.x, render.bounds.min.y, minZ);
        Gizmos.DrawSphere(maxReverse, .15f);
        Gizmos.DrawSphere(minReverse, .15f);
        
        Gizmos.color = Color.Lerp(Color.blue, Color.red, alpha);
        Gizmos.DrawLine(render.bounds.center, render.bounds.max);
        Gizmos.color = Color.Lerp(Color.blue, Color.yellow, alpha);
        Gizmos.DrawLine(render.bounds.center, render.bounds.min);
        Gizmos.color = Color.Lerp(Color.blue, Color.cyan, alpha);
        Gizmos.DrawLine(render.bounds.center, maxReverse);
        Gizmos.DrawLine(render.bounds.center, minReverse);
    }

    private void FindMidPointFromCenter() {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(render.bounds.center, .1f);

        for (int i = 0; i < 4; i++) {
            Vector3 door = Vector3.zero;
            Gizmos.color = Color.magenta;
            switch (i) {
                case 0 :
                    var midZ1 = render.bounds.center.z + (render.bounds.size.z / 2);
                    door = new Vector3(render.bounds.center.x, render.bounds.center.y, midZ1);
                    break;
                case 1 :
                    var midZ2 = render.bounds.center.z + (render.bounds.size.z / 2) * -1;
                    door = new Vector3(render.bounds.center.x, render.bounds.center.y, midZ2);
                    break;
                case 2 :
                    var midX1 = render.bounds.center.x + (render.bounds.size.x / 2);
                    door = new Vector3(midX1, render.bounds.center.y, render.bounds.center.z);
                    break;
                case 3 :
                    var midX2 = render.bounds.center.x + (render.bounds.size.x / 2) * -1;
                    door = new Vector3(midX2, render.bounds.center.y, render.bounds.center.z);
                    break;
            }
            Gizmos.DrawSphere(door, .1f);
            Gizmos.color = Color.Lerp(Color.blue, Color.magenta, alpha);
            Gizmos.DrawLine(render.bounds.center, door);
        }
    }
}
