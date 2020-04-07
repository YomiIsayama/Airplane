using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int m_life = 3;
    public float m_speed = 5;
    public GameObject m_rocket;
    protected Transform m_transform;


    public AudioClip m_shootClip;
    protected AudioSource m_audio;
    public GameObject m_explosionFX;

    protected Vector3 m_targetPos;
    public LayerMask m_inputMask;//mouse sense with layer.plane 

    void Awake()
    {
        m_audio = this.GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        m_transform = this.transform;
        m_targetPos = this.transform.position;
    }

    void OnTriggerEnter(Collider col)
    {
        
        if (col.tag != "PlayerRocket")
        {
            m_life -= 1;
            GameManager.Instance.ChangeLife(m_life);
            Instantiate(m_explosionFX, m_transform.position, Quaternion.identity);
            if (m_life<=0)
            {
                
                Destroy(this.gameObject);

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        MoveTo();
        //PlayerMove();
        AttackRocket();
    }
    // phone or mouse  ,  touch to ctrl move
    protected void MoveTo()
    {
        if (Input.GetMouseButton(0))
        {
            //get mouse position
            Vector3 ms = Input.mousePosition;
            //screen position translate to ray
            Ray ray = Camera.main.ScreenPointToRay(ms);
            //markdown ray sense information
            RaycastHit hitinfo;
            //make the ray
            //LayerMask mask = new LayerMask();
            //mask.value = (int)Mathf.Pow(2.0f, (float)LayerMask.NameToLayer("plane"));
            bool iscast = Physics.Raycast(ray, out hitinfo, 1000,m_inputMask);
            if(iscast)
            {
                //if ray sense into target , that Remember point
                m_targetPos = hitinfo.point;
            }
        }
        //use v3 's MoveTowards function ,to get the target  position
        Vector3 pos = Vector3.MoveTowards(this.m_transform.position, m_targetPos, m_speed * Time.deltaTime);
        //refresh current position
        this.m_transform.position = pos;
    }
    // pc keyboard ctrl move
    protected void PlayerMove()
    {
        float movev = 0;
        float moveh = 0;

        if (Input.GetKey(KeyCode.W))
        {
            movev += m_speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            movev -= m_speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveh -= m_speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveh += m_speed * Time.deltaTime;
        }

        this.transform.Translate(new Vector3(moveh, 0, movev));
    }

    protected void AttackRocket()
    {
        float m_rocketTimer = 0;
        m_rocketTimer -= Time.deltaTime;

        if (m_rocketTimer<=0)
        {
            m_rocketTimer = 1f;
            if (Input.GetKey(KeyCode.Space))
            {
                Instantiate(this.m_rocket, m_transform.position, m_transform.rotation);
                m_audio.PlayOneShot(m_shootClip);
            }
        }
    }
}
