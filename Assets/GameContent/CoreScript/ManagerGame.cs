using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ManagerGame : MonoBehaviour
{
    [SerializeField] private float SpeedGame;                                       //Скорость игры
    [SerializeField] private Transform Playground;                                  //Родительский объект для уровней
    [SerializeField] private GameObject BtStartGame;                                //Объект кнопки Старт
    [SerializeField] private TextMeshProUGUI TextPoints;                            //Объект счетчика очков
    [SerializeField] private ManagerLevel _ManagerLevel;                            //Уровень игры


    [HideInInspector] public static ManagerGame Core = null;                        //Пустой статичный объект менеджера игры
    private ManagerLevel ActiveManagerLevel;                                        //Активный уровень
    private int Points;                                                             //Очки игры


    private void Awake()
    {
        Core = this;                                                                //Присваеваем текущего менеджера (он не будет удаляться т.к сцена не удаляется на всей протяжении игры)                                                      
    }
    
    //Старт игры, вызывается с кнопки
    public void StartGame()
    {
        TextPoints.text = Points.ToString();                                       //Присваеваем объекту счетчику очки
        Time.timeScale = 1;                                                        //Убираем игру с паузы
        BtStartGame.SetActive(false);                                              //Скрываем кнопку старт
       
        if (ActiveManagerLevel!=null)
        {
           Destroy(ActiveManagerLevel.gameObject);                                 //Если уровень создан, то удалить его
        }   
        
        ActiveManagerLevel = Instantiate(_ManagerLevel, Playground, false);       //Создаем новый уровень
    }

    //Метод для проигреша
    public void GameOver()
    {
        BtStartGame.SetActive(true);                                              //Показать кнопку старт
        Points = 0;                                                               //Очистить счетчик
        Time.timeScale = 0;                                                       //Поставить игру на паузу
    }

    //Добавляем счетчику очки
    public void AddPoints()
    {
        Points = Points + 1;                                                     //Добавляем очки
        TextPoints.text = Points.ToString();                                     //Обнавляем данные в счетчике объекта
    }

    //Передаем параметр скорости игры
    public float GetSpeedGame()
    {
        return SpeedGame;
    }
}
