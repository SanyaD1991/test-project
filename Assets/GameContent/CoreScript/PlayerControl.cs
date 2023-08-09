using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private float LiftingForce;                                             //Ïàðàìåòð ïîäúåìíîé ñèëû
    private Rigidbody2D _Rigidbody2D;                                                        //Ïóñòîé îáúåêò èìåòàöèè ôèçèêè 
    private Transform Target;                                                                //Åäà
    
    private void Start()
    {
        _Rigidbody2D = GetComponent<Rigidbody2D>();                                          //Ïðèñâàåâàåì ôèçè÷åñêèé îáúåêò                                       
    }

    
    private void Update()
    {
        MovePlayer();                                                                        //Äâèæåíèå èãðîêà
        MoveFood();                                                                          //Äâèæåíèå åäû ê èãðîêó
    }


    //Ïðîâåðÿåì ñòîëêíîâåíèå îáúåêòà ñ êîëèçèåé
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Dead") 
        {
            ManagerGame.Core.GameOver();                                                     //Åñëè êàñíóëèñü îáúåêò ïðåïÿòñòâèÿ
            Target = null;                                                                   //Óáèðàåì öåëü
        }

        if (collision.gameObject.tag == "Food")
        {
            ManagerGame.Core.AddPoints();                                                    //Åñëè êîñíóëèñü åäû, ïîëó÷àåì î÷êî
            Destroy(collision.gameObject);                                                   //Óäàëÿåì îáúåêò åäà
        }
    }

    //Ïðîâåðÿåì ñòîëêíîâåíèå îáúåêòà ñ òðèããåðîì
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Food")
        {
            if (Target == null)
            {
                Target = collision.transform;                                               //Åñëè êîñíóëèñü îáëàñòü òðèãåððà åäû è îáúåêò öåëè ïóñò, òî ïîëó÷àåì åãî îáúåêò ïîëîæåíèÿ
            }
          
        }
    }

    //Óïðàâëåíèå èãðîêîì
    private void MovePlayer()       
    {   
        if (Input.GetMouseButton(0))                                                                                                                  
        {            
            _Rigidbody2D.AddForce(new Vector3(0, LiftingForce));                           //Åñëè íàæàòèå ëåâàÿ êíîïêà ìûøè, òî ïîäíèìàåì èãðîêà ââåðõ, åñëè íå íàæàòà, òî ïàäàåò ââíèç
        }
    }

    //óïðàâëåíèå åäîé
    private void MoveFood()
    {
        if (Target != null)
        {
            float distance = Vector2.Distance(Target.transform.position, transform.position);       //Åñëè îáúåêò íå ïóñò òî ïðîâåðÿåì äèñòàíöèþ îò åäû äî èãðîêà
            Target.position = Vector2.Lerp(Target.transform.position, transform.position, 0.02f);   //Ïîäúòÿãèâàåì îáúåêò åäû íà ñÿáÿ
            if (distance>2.8f)
            {
                Target = null;                                                                      //Åñëè îáúåêò äàëåêî, òî îòïóñêàåì åãî è íå ïðèòãèâàåì 
            }
        }
    }
}
