using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperEnemy : Enemy
{
    public GameObject m_rocket;
    protected float m_fireTimer = 0;
    protected Transform m_player;

    protected override void UpdateMove()
    {
        m_fireTimer -= Time.deltaTime;
        if(m_fireTimer<0)
        {
            m_fireTimer = 1;
            if(m_player != null)
            {
                Vector3 relativePos = m_player.position - transform.position;
                Instantiate(m_rocket, transform.position, Quaternion.LookRotation(relativePos));
            }
            else
            {
                GameObject obj = GameObject.FindGameObjectWithTag("Player");
                if(obj != null)
                {
                    Debug.Log("found player");
                    m_player = obj.transform;
                    m_fireTimer = 0.5f;
                }
            }
        }
        
        transform.Translate(new Vector3(0, 0, -m_speed * Time.deltaTime));
        
    }
}
