using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankHealth : MonoBehaviour
{
    public float m_StartingHealth = 100f;
    public Slider m_Slider;
    public Image m_FillImage;
    public Color m_FullHealthColor = Color.green;
    public Color m_ZeroHealthColor = Color.red;
    public GameObject m_ExplotionPrefab;


    private AudioSource m_ExplotionAudio;
    private ParticleSystem m_ExplotionParticles;
    private float m_CrrentHealth;
    private bool m_Dead;





    private void Awake()
    {
        m_ExplotionParticles = Instantiate(m_ExplotionPrefab).GetComponent<ParticleSystem>();
        m_ExplotionAudio = m_ExplotionParticles.GetComponent<AudioSource>();
        m_ExplotionParticles.gameObject.SetActive(false);

    }


    private void OnEnable()
    {
        m_CrrentHealth = 100f;
        m_Dead = false;
        SetHealthUI();

    }

    public void TakeDamage(float amount)
    {
        m_CrrentHealth -= amount;
        SetHealthUI();

        if (m_CrrentHealth <= 0f && !m_Dead)
        {
            OnDeath();
        }
    }


    private void SetHealthUI()
    {
        
         m_Slider.value = m_CrrentHealth;
         m_FillImage.color = Color.Lerp(m_ZeroHealthColor, m_FullHealthColor, m_CrrentHealth / m_StartingHealth);

        

    }


    private void OnDeath()
    {
        m_Dead = true;
        m_ExplotionParticles.transform.position = transform.position;
        m_ExplotionParticles.gameObject.SetActive(true);
        m_ExplotionParticles.Play();
        m_ExplotionAudio.Play();
        gameObject.SetActive(false);

    }


}
