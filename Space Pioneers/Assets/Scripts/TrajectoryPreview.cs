using UnityEngine;

[AddComponentMenu("SpacePioneers/Mechanics/Trajectory Preview")]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(LineRenderer))]
public class TrajectoryPreview : MonoBehaviour
{
    [SerializeField] public TrajectoryRS trajectorySet;
    [SerializeField] TrajectorySystem trajectorySystem;
    
    LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
       Vector3[] positions = trajectorySystem.getPositions(this);

       lineRenderer.positionCount = 0;
       lineRenderer.positionCount = positions.Length;

       for(int i = 0; i< positions.Length; i++)
       {
           lineRenderer.SetPosition(i, positions[i]);
       }
    }

    void OnEnable()
    {
        trajectorySet.Add(this);

        if(lineRenderer != null)
        {
            lineRenderer.enabled = true;
        }
    }

    void OnDisable()
    {
        trajectorySet.Remove(this);

        if(lineRenderer != null)
        {
            lineRenderer.positionCount = 0;
            lineRenderer.enabled = false;
        }
    }
}
