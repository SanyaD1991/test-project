using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerLevel : MonoBehaviour
{   
    [SerializeField] private GameObject Player;                             //Объект игрока
    [SerializeField] private GameObject Obstacle;                           //Объект препятствия
    [SerializeField] private GameObject Food;                               //объект еды (за который получаем очки)
    private void Start()
    {
        CreatePlayer();                                                     //Создаем игрока                      
        StartCoroutine(AutoCreteObstacle());                                //Вызываем карутину с препятствиями
        StartCoroutine(AutoCreteFood());                                    //Вызываем карутину с едой
    }  

    //Создать игрока
    private void CreatePlayer()
    {
        Сonstructor(Player);                                                //Конструктор игрока
    }

    //Создать препятствие
    private void CreteObstacle(bool isUp)
    {
        GameObject obstacle = Сonstructor(Obstacle);                       //Конструктор препятствия
        obstacle.GetComponent<MoveObject>().isUp = isUp;                   //Задать первое напровление
    }

    //Создать еду
    private void CreteFood()
    {
        Сonstructor(Food);                                                 //Конструктор еды
    }

    //Метод рандомно создает объект препятствия 
    private IEnumerator AutoCreteObstacle()
    {
        float time = Random.Range(0.2f, 1.5f);                            //Рандомный интервал времени между двумя объектами препятствия
        float pause = Random.Range(2f, 3f);                               //рандомный интервал времени ожидания
        CreteObstacle(false);                                             //Создаем препятствие
        yield return new WaitForSeconds(time);
        CreteObstacle(true);                                              //Создаем препятствие
        yield return new WaitForSeconds(pause);
        StartCoroutine(AutoCreteObstacle());                              //Повторно вызываем карутину с препятствиями

    }

    //Метод рандомно создает объект еды 
    private IEnumerator AutoCreteFood()
    {
        float time = Random.Range(0.2f, 1.5f);                          //Рандомный интервал времени перед созданием еды
        float pause = Random.Range(2f, 3f);                             //рандомный интервал времени ожидания
        yield return new WaitForSeconds(time);
        CreteFood();                                                    //Создаем еду
        yield return new WaitForSeconds(pause);
        StartCoroutine(AutoCreteFood());                                //Повторно вызываем карутину с едой

    }

    //Общий конструктор
    private GameObject Сonstructor(GameObject _object)
    {
        return  Instantiate(_object, transform, false);               
    }
}
