using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeMove : MonoBehaviour
{
    // Start is called before the first frame update

    public float m_Delay = 0.4f;

    public AudioSource Sound;
    private WaitForSeconds m_WaitToReset;

    private Animator anim;

    void Start()
    {

        m_WaitToReset = new WaitForSeconds(m_Delay);

        anim = GetComponent<Animator>();
        Sound = GetComponent<AudioSource>();

        if(!anim)
            Debug.Log("Anim Not Found");

    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Tank"))



        {

        


            anim.SetTrigger("ShakeTrigger");
            Sound.Play();
            // anim.SetBool("Shake",true);
            StartCoroutine(Delay());
            Debug.Log("Triggered");

        }else if(other.CompareTag("Shell"))
        {

            
            anim.SetTrigger("ShakeTrigger");
            Sound.Play();
            StartCoroutine(Delay());
            // anim.SetBool("Shake", true);
            Debug.Log("Triggered");

        }




        //anim.ResetTrigger("ShakeTrigger");
        Debug.Log("Trigger Reset");


    }

    private IEnumerator Delay()
    {
        yield return m_WaitToReset;
    }

}
