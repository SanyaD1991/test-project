using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{   
    [SerializeField] private GameObject Body;                                           //���� ��������� �������
    [SerializeField] private bool isMovevertical;                                       //��� �������� �� ���������   
    private Vector2 MyPosition;                                                         //������� ��������� ����
    private Rigidbody2D _Rigidbody2D;                                                   //������ ��������� ����

    public bool isUp;                                                                   //��� �������� �����
    private float Limit;                                                                //����� �� ������� ����� ��������� ������
    private float YStartPosition;                                                       //��������� Y ������� 
    private float Speed;                                                                //�������� �������� �������
    private float DeltaTime;                                                            //����� ��������

    private void Start()
    {
        _Rigidbody2D = GetComponent<Rigidbody2D>();                                     //����������� ���������� ������
        _Rigidbody2D.position = new Vector3(10, Random.Range(-3f, 3f));                 //��������� � ��������� ��������� (������ �� ���������)
        MyPosition = _Rigidbody2D.position;                                             //���������� ��������� ���������
        YStartPosition = transform.position.y;                                          //���������� ��������� ��������� �� ���������
        Speed = ManagerGame.Core.GetSpeedGame();                                        //����������� ��������
        Limit = Random.Range(1, 2);                                                     //����������� ��������� �������� ����������� �� ���������
        StartCoroutine(Show());                                                         //���������� ���� ��������� ������� ����� 0.1f
    }

    private void Update()
    {
        Move();                                                                         //������� ������
        AutoDestroy();                                                                  //������������� ������ �������� ����  ������ ������� ������
    }


    //����� ����������� �������
    private void Move()
    {
        DeltaTime = Time.deltaTime;                                                     //���������� ����� ��������
        MyPosition.x = MyPosition.x - DeltaTime * Speed;                                //������ ��������� �� �
        if (isMovevertical)
        {
            if (isUp)
            {
                MyPosition.y = MyPosition.y + DeltaTime * 1.5f;                         //���� ��� true ������ �������� �����, ������ ��������� Y
                if (MyPosition.y >= YStartPosition + Limit)
                {
                    isUp = false;                                                       //���� ��������� �� Y �������� ����� Max, �� ������ ��� �� false
                }
            }
            else
            {
                MyPosition.y = MyPosition.y - DeltaTime * 1.5f;                         //���� ��� false ������ �������� ����, ������ ��������� �� Y
                if (MyPosition.y <= YStartPosition - Limit)                             
                {
                    isUp = true;                                                        //���� ��������� �� Y �������� ����� Min, �� ������ ��� �� true
                }
            }
        }
        _Rigidbody2D.MovePosition(MyPosition);                                          //���������� ������
      
    }


    //����� ��������������� �������� ����������� �������
    private void AutoDestroy()
    {
        if (Body != null)
        {
            if (Body.transform.position.x <= -10)
            {
                Destroy(gameObject);                                                    //���� ���� ���� � ����������� ������� � ������ ������������ �� ������� ������, �� ������� ���
            }
        }
        else 
        {
            Destroy(gameObject);                                                        //���� ��� ���� � ����������� �������, �� ������� ���
        }
    }

    
   
    //�������� �������� ������
    private IEnumerator Show()
    {
        yield return new WaitForSeconds(0.1f);
        Body.SetActive(true);                                                           //���������� ������
    }
}
