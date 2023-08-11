using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Rigidbody2D _Rigidbody2D;                                                        
    private Transform Target;                                                                
    
    private void Start()
    {
        _Rigidbody2D = GetComponent<Rigidbody2D>();                                                                                
    }

    
    private void Update()
    {
        MovePlayer();
        MoveFood();                                                                         
    }


   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Dead") 
        {
            ManagerGame.Core.GameOver();                                                     
            Target = null;                                                                  
        }

        if (collision.gameObject.tag == "Food")
        {
            ManagerGame.Core.AddPoints();                                                   
            Destroy(collision.gameObject);                                                   
        }
    }

   
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Food")
        {
            if (Target == null)
            {
                Target = collision.transform;                                               
            }
          
        }
    }

   
    private void MovePlayer()       
    {   
        if (Input.GetMouseButton(0))                                                                                                                  
        {
#if UNITY_EDITOR
            _Rigidbody2D.AddForce(new Vector3(0, 3));                           
#else
            _Rigidbody2D.AddForce(new Vector3(0, 50));                          
#endif
        }
    }

   
    private void MoveFood()
    {
        if (Target != null)
        {
            float distance = Vector2.Distance(Target.transform.position, transform.position);       
#if UNITY_EDITOR
            Target.position = Vector2.Lerp(Target.transform.position, transform.position, 0.002f);   
#else
            Target.position = Vector2.Lerp(Target.transform.position, transform.position, 0.02f);   
#endif


            if (distance>2.8f)
            {
                Target = null; 
            }
        }
    }
}
