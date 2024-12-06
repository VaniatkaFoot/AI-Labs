using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy1 : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject pl;
    [SerializeField]
    public float distance,speed,RunDistance;

    private Vector3 startPos;
    
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;//��������� ��������� �������
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, pl.transform.position) <= distance)//���� ��������� �� ������ ������ ��������� 
        {
            agent.speed = speed * (distance / Vector3.Distance(transform.position, pl.transform.position)) + 1f; //��� ����� ����� � ����� ��� ������� �������� ����

            Vector3 directionAway = (transform.position - pl.transform.position).normalized;//��������� ������ ��������������� ���� ��� ��������� �����
            Vector3 runTarget = transform.position + directionAway * RunDistance;//���������� ����� �������� � ���� �����������

            agent.SetDestination(runTarget);//�������� � ������������� �����
        }

    }
}
