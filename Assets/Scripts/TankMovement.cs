using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMovement : MonoBehaviour
{
 
    public int m_TankNumber = 1;
    public float m_Speed = 12f;
    public float m_TurnSpeed = 180f;
    public AudioSource m_MovementAudio;
    public AudioClip m_EngineIdlying;
    public AudioClip m_EngineDriving;
    public float m_PitchRange = 0.2f;

    string m_MovementAxisName;
    string m_TurnAxisName;
    Rigidbody2D m_rigidbody;
    float m_MovenentInptValue;
    float m_TurnInputValue;
    float m_OriginalPitch;


    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        m_rigidbody.isKinematic = false;
        m_MovenentInptValue = 0f;
        m_TurnInputValue = 0f;

    }


    private void OnDisable()
    {
        m_rigidbody.isKinematic = true;

    }

    private void Start()
    {
        m_MovementAxisName = "Vertical"+ m_TankNumber;
        m_TurnAxisName = "Horizontal" + m_TankNumber;
        m_OriginalPitch = m_MovementAudio.pitch;
    }


    // Update is called once per frame
    private void Update()
    {
        m_MovenentInptValue = Input.GetAxis(m_MovementAxisName);
        m_TurnInputValue = Input.GetAxis(m_TurnAxisName);
        EngineAudio();
    }



    private void EngineAudio()
    {
        if (Mathf.Abs(m_MovenentInptValue)<0.1f && Mathf.Abs(m_TurnInputValue)<0.1f)
        {
            if(m_MovementAudio.clip== m_EngineDriving)
            {
                m_MovementAudio.clip = m_EngineIdlying;
                m_MovementAudio.pitch = Random.Range(m_OriginalPitch - m_PitchRange, m_OriginalPitch + m_PitchRange);
                m_MovementAudio.Play();

            }
        }
        else
        {
            if (m_MovementAudio.clip == m_EngineIdlying)
            {
                m_MovementAudio.clip = m_EngineDriving;
                m_MovementAudio.pitch = Random.Range(m_OriginalPitch - m_PitchRange, m_OriginalPitch + m_PitchRange);
                m_MovementAudio.Play();

            }

            

        }
    }


    private void FixedUpdate()
    {
        Move();
        Turn();

    }


    private void Move()
    {
        Vector2 movedirection = transform.up * m_Speed;
        m_rigidbody.MovePosition(m_rigidbody.position + movedirection * m_MovenentInptValue * Time.fixedDeltaTime);

    }

    private void Turn()
    {
        float turn = m_TurnInputValue * m_TurnSpeed * Time.fixedDeltaTime;
      
        m_rigidbody.MoveRotation(m_rigidbody.rotation+ turn);
       
    }




}
