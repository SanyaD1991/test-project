using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerLevel : MonoBehaviour
{   
    [SerializeField] private GameObject Player;                             //������ ������
    [SerializeField] private GameObject Obstacle;                           //������ �����������
    [SerializeField] private GameObject Food;                               //������ ��� (�� ������� �������� ����)
    private void Start()
    {
        CreatePlayer();                                                     //������� ������                      
        StartCoroutine(AutoCreteObstacle());                                //�������� �������� � �������������
        StartCoroutine(AutoCreteFood());                                    //�������� �������� � ����
    }  

    //������� ������
    private void CreatePlayer()
    {
        �onstructor(Player);                                                //����������� ������
    }

    //������� �����������
    private void CreteObstacle(bool isUp)
    {
        GameObject obstacle = �onstructor(Obstacle);                       //����������� �����������
        obstacle.GetComponent<MoveObject>().isUp = isUp;                   //������ ������ �����������
    }

    //������� ���
    private void CreteFood()
    {
        �onstructor(Food);                                                 //����������� ���
    }

    //����� �������� ������� ������ ����������� 
    private IEnumerator AutoCreteObstacle()
    {
        float time = Random.Range(0.2f, 1.5f);                            //��������� �������� ������� ����� ����� ��������� �����������
        float pause = Random.Range(2f, 3f);                               //��������� �������� ������� ��������
        CreteObstacle(false);                                             //������� �����������
        yield return new WaitForSeconds(time);
        CreteObstacle(true);                                              //������� �����������
        yield return new WaitForSeconds(pause);
        StartCoroutine(AutoCreteObstacle());                              //�������� �������� �������� � �������������

    }

    //����� �������� ������� ������ ��� 
    private IEnumerator AutoCreteFood()
    {
        float time = Random.Range(0.2f, 1.5f);                          //��������� �������� ������� ����� ��������� ���
        float pause = Random.Range(2f, 3f);                             //��������� �������� ������� ��������
        yield return new WaitForSeconds(time);
        CreteFood();                                                    //������� ���
        yield return new WaitForSeconds(pause);
        StartCoroutine(AutoCreteFood());                                //�������� �������� �������� � ����

    }

    //����� �����������
    private GameObject �onstructor(GameObject _object)
    {
        return  Instantiate(_object, transform, false);               
    }
}
