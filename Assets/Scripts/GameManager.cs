using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject m_canvas_main;
    public GameObject m_canvas_gameover;
    public Text m_text_score;
    public Text m_text_best;
    public Text m_text_life;
    public Button restart_button;

    protected int m_score = 0;
    public static int m_hiscore = 0;
    protected Player m_player;

    public AudioClip m_musicClip;
    protected AudioSource m_audio;

    void Awake()
    {
        Instance = this;
        m_audio = this.gameObject.AddComponent<AudioSource>();
        m_player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        m_text_score = m_canvas_main.transform.Find("Text_score").GetComponent<Text>();
        m_text_best = m_canvas_main.transform.Find("Text_best").GetComponent<Text>();
        m_text_life = m_canvas_main.transform.Find("Text_life").GetComponent<Text>();
        restart_button = m_canvas_gameover.transform.Find("Button_restart").GetComponent<Button>();


    }




    // Start is called before the first frame update
    void Start()
    {

        m_audio.clip = m_musicClip;
        m_audio.loop = true;
        m_audio.Play();

        m_text_score.text = string.Format("Score {0}", m_score);
        m_text_best.text = string.Format("Best Score {0}", m_hiscore);
        m_text_life.text = string.Format("Life {0}", m_player.m_life);

        restart_button.onClick.AddListener(delegate ()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        });
        m_canvas_gameover.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (m_player.m_life <= 0)
        {
            m_canvas_gameover.gameObject.SetActive(true);
        }
    }

    public void AddScore(int point)
    {
        m_score += point;

        if (m_hiscore < m_score)
        {
            m_hiscore = m_score;
        }
        m_text_score.text = string.Format("Score {0}", m_score);
        m_text_best.text = string.Format("Best Score {0}", m_hiscore);
    }

    public void ChangeLife(int life)
    {
        m_text_life.text = string.Format("Life {0}", life);

    }
}
