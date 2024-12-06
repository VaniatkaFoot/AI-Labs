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
        startPos = transform.position;  //сохраняем стартовую позицию
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, pl.transform.position) <= distance)  //если растояние до игрока меньше указанной 
        {
            agent.speed = speed * (Vector3.Distance(transform.position, pl.transform.position) / distance) + 1f;    //чем ближе враг к игроку тем медленне он движется

            agent.SetDestination(pl.transform.position);    //позиция игрока становится целью
            
        }
        else//если растояние до игрока больше указанной 
        {
            agent.SetDestination(startPos);//враг идет на стартовую позицию
        }
    }
}
