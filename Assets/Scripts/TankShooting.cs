using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankShooting : MonoBehaviour
{


    public float m_StartDelay = 0.4f;

    private WaitForSeconds WaitABit;

    public int m_TankNumber = 1;
    public Rigidbody2D m_Shell;
    public Transform m_FireTrransform;
    public Slider m_AimSlider;
    public AudioSource m_ShootingAudio;
    public AudioClip m_ChargingClip;
    public AudioClip m_FireClip;
    public float m_MinLaunchForce=1f;
    public float m_MaxLaunchForce=30f;
    public float m_MaxChargeTime=0.75f;


    private string m_FireButton;
    private float m_CrrentLanchForce;
    private float m_ChargeSpeed;
    private bool m_Fired;
    private bool m_CanFire=true;




    // Start is called before the first frame update
    void OnEnable()
    {
        ///Fire();
        m_CrrentLanchForce = m_MinLaunchForce;
        m_AimSlider.value = m_MinLaunchForce;
    }

    // Update is called once per frame
    void Start()
    {

        WaitABit = new WaitForSeconds(m_StartDelay);

        m_FireButton = "Fire" + m_TankNumber;
        m_ChargeSpeed = (m_MaxLaunchForce - m_MinLaunchForce) / m_MaxChargeTime;
    }

    private void Update()

    {

        if (m_CanFire)
        {

            m_AimSlider.value = m_MinLaunchForce;

            if (m_CrrentLanchForce >= m_MaxLaunchForce && !m_Fired)
            {
                m_CrrentLanchForce = m_MaxLaunchForce;
                Fire();

            }
            else if (Input.GetButtonDown(m_FireButton) && m_Fired)
            {
                m_Fired = false;
                m_CrrentLanchForce = m_MinLaunchForce;
                m_ShootingAudio.clip = m_ChargingClip;
                m_ShootingAudio.Play();

            }
            else if (Input.GetButton(m_FireButton) && !m_Fired)
            {
                m_CrrentLanchForce += m_ChargeSpeed * Time.deltaTime;
                m_AimSlider.value = m_CrrentLanchForce;
            }
            else if (Input.GetButtonUp(m_FireButton) && !m_Fired)
            {
                Fire();
            }


        }


    }


    private IEnumerator Delay()
    {
        yield return WaitABit;
        m_CanFire = true;
    }

    private void Fire()
    {
        m_Fired = true;
        Rigidbody2D shellInstance = Instantiate(m_Shell, m_FireTrransform.position, m_FireTrransform.rotation) as Rigidbody2D;
        shellInstance.velocity = m_CrrentLanchForce * m_FireTrransform.up;
        m_ShootingAudio.clip = m_FireClip;
        m_ShootingAudio.Play();
        m_CrrentLanchForce = m_MinLaunchForce;
        m_CanFire = false;

        StartCoroutine(Delay());
    }

    /*IEnumerator waiter_not_that_waiter_just_waiter()
    {
        yield return new WaitForSeconds(1000000000000);
        //my code here after 3 seconds
    }

    */
}
