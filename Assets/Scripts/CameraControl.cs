using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float m_DampTime = 0.2f;
    public float m_ScreenEdgeBuffer = 4f;
    public float m_MinSize = 5f;
    [HideInInspector]public Transform[] m_Targets;

    private Camera m_Camera;
    private float m_ZoomSpeed;
    private Vector3 m_MoveVelocity;
    private Vector3 m_DesiredPosition;


    
    // Start is called before the first frame update  
     // Start is called before the first frame update  
    void Awake()
    {
        m_Camera = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        Zoom();

    }


    private void Move()
    {
        FindAveragePosition();
        transform.position = Vector3.SmoothDamp(transform.position, m_DesiredPosition, ref m_MoveVelocity, m_DampTime);

    }

    private void FindAveragePosition()
    {
        Vector3 averagePos = new Vector3();
        averagePos = Vector3.zero;
        int numTargets = 0;

        for (int i=0; i < m_Targets.Length; i++)
        {

            if (!m_Targets[i].gameObject.activeSelf)
                continue;

            averagePos += m_Targets[i].position;
            numTargets++;
        }

        if (numTargets > 0)
        {
            averagePos /= numTargets;
            averagePos.z = transform.position.z;
            m_DesiredPosition = averagePos;
        }

    }

    private void Zoom()
    {
        float requiredSize = FindRequiredSize();
        m_Camera.orthographicSize = Mathf.SmoothDamp(m_Camera.orthographicSize, requiredSize, ref m_ZoomSpeed, m_DampTime);
     }


    private float FindRequiredSize()
    {
        Vector3 desiredLocalPos = transform.InverseTransformPoint(m_DesiredPosition);
        float size = 0f;
        for (int i = 0; i < m_Targets.Length; i++)
        {
            if (!m_Targets[i].gameObject.activeSelf)
                continue;

            Vector3 targetLoclPos = transform.InverseTransformPoint(m_Targets[i].position);
            Vector3 distanceToTank = targetLoclPos - desiredLocalPos;
            size = Mathf.Max(size, Mathf.Abs(distanceToTank.y));
            size = Mathf.Max(size, Mathf.Abs(distanceToTank.x/m_Camera.aspect));
        }

        size += m_ScreenEdgeBuffer;
        size = Mathf.Max(size, m_MinSize);
        return size;

    }

    public void SetStartPositionAndSize()
    {
        FindAveragePosition();
        transform.position = m_DesiredPosition;
        m_Camera.orthographicSize = FindRequiredSize();
    }

}
