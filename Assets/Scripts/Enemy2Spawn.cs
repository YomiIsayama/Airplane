using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Spawn : MonoBehaviour
{
    public GameObject m_enemyPrefab;
    protected Transform m_player;
    IEnumerator SpawnEnemy()
    {
        while(true)
        {
            
            yield return new WaitForSeconds(Random.Range(3, 4));
            GameObject obj = GameObject.FindGameObjectWithTag("Player");
            if (obj != null)
            {
                    Debug.Log("found player");
                    m_player = obj.transform;
            }
            try
            { 
                Vector3 relativePos = m_player.position - transform.position;
                Instantiate(m_enemyPrefab, transform.position, Quaternion.LookRotation(-relativePos));
            }
            catch(System.Exception)
            {
                //m_player null 
            }


        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position,"item.png",true);
    }

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(SpawnEnemy());
        StartCoroutine("SpawnEnemy");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
