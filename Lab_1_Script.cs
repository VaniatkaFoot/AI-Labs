using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject pl;
    [SerializeField]
    public float distance,speed;

    private Vector3 startPos;
    
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;  //��������� ��������� �������
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, pl.transform.position) <= distance)  //���� ��������� �� ������ ������ ��������� 
        {
            agent.speed = speed * (Vector3.Distance(transform.position, pl.transform.position) / distance) + 1f;    //��� ����� ���� � ������ ��� �������� �� ��������

            agent.SetDestination(pl.transform.position);    //������� ������ ���������� �����
            
        }
        else//���� ��������� �� ������ ������ ��������� 
        {
            agent.SetDestination(startPos);//���� ���� �� ��������� �������
        }
    }
}
