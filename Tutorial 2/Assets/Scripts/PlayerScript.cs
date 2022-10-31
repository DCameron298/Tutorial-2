using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerScript : MonoBehaviour
{
    Animator anim;

   private Rigidbody2D rd2d;
   
   public float speed;

   public TextMeshProUGUI score;
   
   private int scoreValue = 0;

   private int Lives;

   public GameObject LoseTextObject;

    public GameObject winTextObject;

    public TextMeshProUGUI LivesText;

    public GameObject Player;

    public AudioSource musicSource;

    public AudioClip musicClipOne;

    


    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = scoreValue.ToString();
        anim = GetComponent<Animator>();
        Lives = 3;

        SetLivesText();
        winTextObject.SetActive(false);
        LoseTextObject.SetActive(false);

       
    }

   void Update()
   {
        if (Input.GetKeyDown(KeyCode.W))
        {
            anim.SetInteger("State", 1);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            anim.SetInteger("State", 3);
            anim.SetBool("Mirror", true);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            anim.SetInteger("State", 2);
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            anim.SetInteger("State", 0);
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            anim.SetInteger("State", 0);
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            anim.SetInteger("State", 0);
        }
        if(Lives <= 0)
        {
            LoseTextObject.SetActive(true);
            
        }

        if(scoreValue >= 8)
        {
            winTextObject.SetActive(true);
            musicSource.clip = musicClipOne;
            musicSource.Play();
        }

        if(Lives <= 0)
        {
            Destroy(Player);
        }
       
      
   }
   
   
   
   
    // Update is called once per frame
    //When I change "Update" to "FixedUpdate" My player stops moving so I'm gonna keep it as Update to maintain functionality
    void FixedUpdate()
    {
       float hozMovement = Input.GetAxis ("Horizontal");
       float vertMovement = Input.GetAxis ("Vertical");
       rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed)); 
       
        if(scoreValue >= 4)
        {
           Player.gameObject.SendMessage("React");
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Coin")
        {
         Destroy(collision.collider.gameObject);
        
        scoreValue += 1;
        score.text= scoreValue.ToString();
        }
        
        if (collision.collider.tag == "Enemy")
        {
            Destroy(collision.collider.gameObject);

            Lives -= 1;
            LivesText.text= "Lives: " + Lives.ToString();
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
       if (collision.collider.tag == "Ground")
       {
            if (Input.GetKey(KeyCode.W))
                 {
                         rd2d.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);
                 }
     }
        
    }
    void SetLivesText()
    {
        LivesText.text = "Lives: " + Lives.ToString();

        if(Lives <= 0)
        {
            LoseTextObject.SetActive(true);
            
        }
    
  

 
    }

    void React()
    {
        transform.position = new Vector2(66, 0);
        Lives = 3;
    }

    
    }

