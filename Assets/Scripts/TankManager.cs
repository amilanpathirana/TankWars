//using JetBrains.Annotations;
using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class TankManager
{
    public Color m_TankColor;
    public Transform m_SpawnPoint;

    [HideInInspector] public int m_TankrNumber;
    [HideInInspector] public string m_ColoredPlayerText;
    [HideInInspector] public GameObject m_Instance;
    [HideInInspector] public int m_Wins;

    private TankMovement m_Movement;
    private TankShooting m_Shooting;
    private GameObject m_CanvasGameObject;

    public void Setup()
    {
        m_Movement = m_Instance.GetComponent<TankMovement>();
        m_Shooting = m_Instance.GetComponent<TankShooting>();
        m_CanvasGameObject = m_Instance.GetComponentInChildren<Canvas>().gameObject;

        m_Movement.m_TankNumber = m_TankrNumber;
        m_Shooting.m_TankNumber = m_TankrNumber;
        m_ColoredPlayerText = "<color=#" + ColorUtility.ToHtmlStringRGB(m_TankColor) + ">TANK" + m_TankrNumber + "</color>";
        SpriteRenderer renderers = m_Instance.GetComponent<SpriteRenderer>();
        renderers.color = m_TankColor;
    }

        public void DisableControl()
        {
        m_Movement.enabled = false;
        m_Shooting.enabled = false;
        m_CanvasGameObject.SetActive(false);

        }

    public void EnableControl()
    {
        m_Movement.enabled = true;
        m_Shooting.enabled = true;
        m_CanvasGameObject.SetActive(true);

    }

    public void Reset()
    {
        m_Instance.transform.position = m_SpawnPoint.position;
        m_Instance.transform.rotation = m_SpawnPoint.rotation;

        m_Instance.SetActive(false);
        m_Instance.SetActive(true);
    }



}
