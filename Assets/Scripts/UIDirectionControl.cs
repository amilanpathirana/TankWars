using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDirectionControl : MonoBehaviour
{
    // Start is called before the first frame update
    public bool m_UseRelativeLocation = true;
    private Quaternion m_RelativeRotation;


    private void Start()
    {
        m_RelativeRotation = transform.parent.localRotation;
    }
    // Update is called once per frame
    void Update()
    {
        if (m_UseRelativeLocation)
            transform.rotation = m_RelativeRotation;
    }
}
