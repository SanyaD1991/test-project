using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ManagerGame : MonoBehaviour
{
    [SerializeField] private float SpeedGame;                                       //�������� ����
    [SerializeField] private Transform Playground;                                  //������������ ������ ��� �������
    [SerializeField] private GameObject BtStartGame;                                //������ ������ �����
    [SerializeField] private TextMeshProUGUI TextPoints;                            //������ �������� �����
    [SerializeField] private ManagerLevel _ManagerLevel;                            //������� ����


    [HideInInspector] public static ManagerGame Core = null;                        //������ ��������� ������ ��������� ����
    private ManagerLevel ActiveManagerLevel;                                        //�������� �������
    private int Points;                                                             //���� ����


    private void Awake()
    {
        Core = this;                                                                //����������� �������� ��������� (�� �� ����� ��������� �.� ����� �� ��������� �� ���� ���������� ����)                                                      
    }
    
    //����� ����, ���������� � ������
    public void StartGame()
    {
        TextPoints.text = Points.ToString();                                       //����������� ������� �������� ����
        Time.timeScale = 1;                                                        //������� ���� � �����
        BtStartGame.SetActive(false);                                              //�������� ������ �����
       
        if (ActiveManagerLevel!=null)
        {
           Destroy(ActiveManagerLevel.gameObject);                                 //���� ������� ������, �� ������� ���
        }   
        
        ActiveManagerLevel = Instantiate(_ManagerLevel, Playground, false);       //������� ����� �������
    }

    //����� ��� ���������
    public void GameOver()
    {
        BtStartGame.SetActive(true);                                              //�������� ������ �����
        Points = 0;                                                               //�������� �������
        Time.timeScale = 0;                                                       //��������� ���� �� �����
    }

    //��������� �������� ����
    public void AddPoints()
    {
        Points = Points + 1;                                                     //��������� ����
        TextPoints.text = Points.ToString();                                     //��������� ������ � �������� �������
    }

    //�������� �������� �������� ����
    public float GetSpeedGame()
    {
        return SpeedGame;
    }
}
