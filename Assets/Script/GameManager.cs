using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int totalPoint;
    public int stagePoint;
    public int stageIndex;
    public int health;
    public PlayerMove player;
    public GameObject[] Stages;

    // UI
    public Image[] UIhealth;
    public Text UIPoint;
    public Text UIStage;
    public GameObject UIRestartBtn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HealthDown()
    {
        if(health > 1)
        {
            health--;
            UIhealth[health].color = new Color(1, 0, 0, 0.4f);
        }
        else
        {
            // All Health UI Off
            UIhealth[0].color = new Color(1, 0, 0, 0.4f);

            // Player Die Effect
            //player.OnDie();

            // Result UI
            Debug.Log("ав╬З╫ю╢о╢ы");

            // Retry Button UI
            //UIRestartBtn.SetActive(true);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            // Player Reposition
            //if (health > 1)
            //{
            //    PlayerReposition();
            //}

            // Health Down
            HealthDown();
        }
    }

    void PlayerReposition()
    {
        player.transform.position = new Vector3(0, 0, -1);
        //player.VelocityZero();

    }

    public void Restart()
    {
        Time.timeScale = 1;
        //SceneManager.LoadScene(0);
    }

}
