using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{   
    [SerializeField] private GameObject Body;                                           //Тело движущего объекта
    [SerializeField] private bool isMovevertical;                                       //Чек движение по вертикали   
    private Vector2 MyPosition;                                                         //Позиция движущего тела
    private Rigidbody2D _Rigidbody2D;                                                   //Физика движущего тела

    public bool isUp;                                                                   //Чек подимать вверх
    private float Limit;                                                                //Лимит на которое может подняться объект
    private float YStartPosition;                                                       //Стартовая Y позиция 
    private float Speed;                                                                //Скорость движения объекта
    private float DeltaTime;                                                            //Время перепада

    private void Start()
    {
        _Rigidbody2D = GetComponent<Rigidbody2D>();                                     //Присваеваем физический объект
        _Rigidbody2D.position = new Vector3(10, Random.Range(-3f, 3f));                 //Размещаем в стартовое положение (рандом по вертикали)
        MyPosition = _Rigidbody2D.position;                                             //Записываем стартовое положение
        YStartPosition = transform.position.y;                                          //Записываем стартовое положение по вертикали
        Speed = ManagerGame.Core.GetSpeedGame();                                        //Присваеваем скорость
        Limit = Random.Range(1, 2);                                                     //Присваеваем рендомный интервал перемещения по вертикали
        StartCoroutine(Show());                                                         //Показываем тело движущего объекта через 0.1f
    }

    private void Update()
    {
        Move();                                                                         //Двигаем объект
        AutoDestroy();                                                                  //Автоматически объект удалится если  улетит слишком далеко
    }


    //Метод перемещения объекта
    private void Move()
    {
        DeltaTime = Time.deltaTime;                                                     //Записываем врямя перепада
        MyPosition.x = MyPosition.x - DeltaTime * Speed;                                //Задаем положение по Х
        if (isMovevertical)
        {
            if (isUp)
            {
                MyPosition.y = MyPosition.y + DeltaTime * 1.5f;                         //Если чек true значит движемся вверх, задаем положение Y
                if (MyPosition.y >= YStartPosition + Limit)
                {
                    isUp = false;                                                       //Если положение по Y превысил лемит Max, то меняем чек на false
                }
            }
            else
            {
                MyPosition.y = MyPosition.y - DeltaTime * 1.5f;                         //Если чек false значит движемся вниз, задаем положение по Y
                if (MyPosition.y <= YStartPosition - Limit)                             
                {
                    isUp = true;                                                        //Если положение по Y превысил лемит Min, то меняем чек на true
                }
            }
        }
        _Rigidbody2D.MovePosition(MyPosition);                                          //Перемещаем объект
      
    }


    //Метод автоматического удаления движущегося объекта
    private void AutoDestroy()
    {
        if (Body != null)
        {
            if (Body.transform.position.x <= -10)
            {
                Destroy(gameObject);                                                    //Если есть тело у движущегося объекта и объект переместился за пределы экрана, то удаляем его
            }
        }
        else 
        {
            Destroy(gameObject);                                                        //Если нет тела у движущегося объекта, то удаляем его
        }
    }

    
   
    //карутина паказать объект
    private IEnumerator Show()
    {
        yield return new WaitForSeconds(0.1f);
        Body.SetActive(true);                                                           //Показываем объект
    }
}
