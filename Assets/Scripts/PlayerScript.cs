using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerScript : MonoBehaviour
{

    private Rigidbody2D rd2d;
    public float speed;
    public Text score;
    public Text life;
    private int scoreValue = 0;
    private int lifeValue = 3;
    public GameObject winTextObject;
    public GameObject loseTextObject;
    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;
    public AudioSource musicSource;
    Animator anim;
    private bool facingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        winTextObject.SetActive(false);
        loseTextObject.SetActive(false);
        score.text = scoreValue.ToString();
        life.text = lifeValue.ToString();
        musicSource.clip = musicClipOne;
        musicSource.Play();
        anim = GetComponent<Animator>();
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float verMovement = Input.GetAxis("Vertical");

        rd2d.AddForce(new Vector2(hozMovement * speed, verMovement * speed));
        if (hozMovement > 0)
        {
            anim.SetInteger("State", 1);
        }
        if (hozMovement < 0)
        {
            anim.SetInteger("State", 1);
        }
        if (hozMovement == 0)
        {
            anim.SetInteger("State", 0);
        }
        if (verMovement > 0)
        {
            anim.SetInteger("State", 2);
        }

        if (facingRight == false && hozMovement > 0)
        {
            Flip();
        }
        else if (facingRight == true && hozMovement < 0)
        {
            Flip();
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = scoreValue.ToString();
            Destroy(collision.collider.gameObject);
            if(scoreValue == 4)
            {
                gameObject.transform.position = new Vector3(46.3f, -2.5f, 0f);
                lifeValue = 3;
                life.text = lifeValue.ToString();
            }
            if(scoreValue == 8)
            {                
                winTextObject.SetActive(true);
                musicSource.Stop();
                musicSource.clip = musicClipTwo;
                musicSource.Play();
            }
        }
        if(collision.collider.tag == "Enemy")
        {
            lifeValue -= 1;
            life.text = lifeValue.ToString();
            Destroy(collision.collider.gameObject);
            if(lifeValue ==0)
            {
                loseTextObject.SetActive(true);
                gameObject.SetActive(false);
            }
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.collider.tag == "Ground")
        {
            if(Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
        }
    }
}
