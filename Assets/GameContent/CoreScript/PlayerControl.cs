using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private float LiftingForce;                                             //Параметр подъемной силы
    private Rigidbody2D _Rigidbody2D;                                                        //Пустой объект иметации физики 
    private Transform Target;                                                                //Еда
    
    private void Start()
    {
        _Rigidbody2D = GetComponent<Rigidbody2D>();                                          //Присваеваем физический объект                                       
    }

    
    private void Update()
    {
        MovePlayer();                                                                        //Движение игрока
        MoveFood();                                                                          //Движение еды к игроку
    }


    //Проверяем столкновение объекта с колизией
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Dead") 
        {
            ManagerGame.Core.GameOver();                                                     //Если каснулись объект препятствия
            Target = null;                                                                   //Убираем цель
        }

        if (collision.gameObject.tag == "Food")
        {
            ManagerGame.Core.AddPoints();                                                    //Если коснулись еды, получаем очко
            Destroy(collision.gameObject);                                                   //Удаляем объект еда
        }
    }

    //Проверяем столкновение объекта с триггером
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Food")
        {
            if (Target == null)
            {
                Target = collision.transform;                                               //Если коснулись область тригерра еды и объект цели пуст, то получаем его объект положения
            }
          
        }
    }

    //Управление игроком
    private void MovePlayer()       
    {   
        if (Input.GetMouseButton(0))                                                                                                                  
        {            
            _Rigidbody2D.AddForce(new Vector3(0, LiftingForce));                           //Если нажатие левая кнопка мыши, то поднимаем игрока вверх, если не нажата, то падает ввниз
        }
    }

    //управление едой
    private void MoveFood()
    {
        if (Target != null)
        {
            float distance = Vector2.Distance(Target.transform.position, transform.position);       //Если объект не пуст то проверяем дистанцию от еды до игрока
            Target.position = Vector2.Lerp(Target.transform.position, transform.position, 0.002f);  //Подътягиваем объект еды на сябя
            if (distance>3)
            {
                Target = null;                                                                      //Если объект далеко, то отпускаем его и не притгиваем 
            }
        }
    }
}
