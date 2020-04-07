using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float m_speed = 1;
    public float m_life = 10;
    protected float m_rotSpeed = 30;
    public int m_point = 10;

    internal Renderer m_renderer;
    internal bool m_isActiv = false;
    

    // Start is called before the first frame update

    void Awake()
    {
        m_renderer = this.GetComponent<Renderer>();
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMove();
        if(m_isActiv && !this.m_renderer.isVisible)
        {
            Destroy(this.gameObject);
        }
    }

    void OnBecameraVisble()
    {
        m_isActiv = true;
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "PlayerRocket")
        {
            Rocket rocket = col.GetComponent<Rocket>();
            if(rocket != null)
            {
                m_life -= rocket.m_power;

                if(m_life<0)
                {
                    GameManager.Instance.AddScore(m_point);
                    Destroy(this.gameObject);
                }
            }
        }
        else if(col.tag == "Player")
        {
            m_life = 0;
            Destroy(this.gameObject);
        }
    }

    protected virtual void UpdateMove()
    {
        float rx = Mathf.Sin(Time.time) * Time.deltaTime;
        transform.Translate(new Vector3(rx, 0, m_speed * Time.deltaTime));
    }
}
