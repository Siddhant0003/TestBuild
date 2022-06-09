using CasualGame.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [Header("player_data")]
    [SerializeField] float speed = 8;
    float FallingThreshold = -20f;
    [SerializeField] int score_count;

    private void Update()
    {
        float x_Value = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        float z_Value = Input.GetAxis("Vertical") * Time.deltaTime * speed;
        transform.Translate(x_Value, 0, z_Value);

        if (gameObject.GetComponent<Rigidbody>().velocity.y < FallingThreshold)
        {
            GameManager.instance.gameOverScreen.SetActive(true);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Pulpit")
        {
            score_count++;
            GameManager.instance.score.text = "Score:" + score_count.ToString();
           
        }

    }
}
