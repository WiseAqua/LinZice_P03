using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class AIEnemy : MonoBehaviour
{
    public Transform Player;
    [SerializeField] private float MoveSpeed = 2.5f;
    [SerializeField] private int MaxDist = 10;
    [SerializeField] private int MinDist = 1;


    void Start()
    {
    }

    void Update()
    {
        transform.LookAt(Player);
        

        if (Vector3.Distance(transform.position, Player.position) <= MaxDist)
        {
            transform.position += transform.forward * MoveSpeed * Time.deltaTime;
        }
        if (Vector3.Distance(transform.position, Player.position) <= MinDist)
        {
            transform.position -= transform.forward * MoveSpeed * Time.deltaTime;
        }
     }

}
