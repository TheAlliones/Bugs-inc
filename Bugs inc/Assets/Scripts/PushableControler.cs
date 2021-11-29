using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableControler : MonoBehaviour
{

    private Rigidbody2D rb;
    private PlayerControler rbM;
    public enum PlayerBug { ladybug, grasshopper, dung_beetle, stag_beetle, water_strider }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rbM = GameObject.Find("Player").GetComponent<PlayerControler>();
    }

    void Update()
    {
        if(rbM.playerBug == PlayerControler.PlayerBug.dung_beetle)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;

        }
        else
        {
            if(rb.velocity.y == 0)
            {
                rb.bodyType = RigidbodyType2D.Static;
            }
        }
        
    }
}
