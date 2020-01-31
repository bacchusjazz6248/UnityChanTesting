using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
[RequireComponent(typeof(ThirdPersonCharacter))]
public class EnemyController : MonoBehaviour
{
    [System.NonSerialized]
    public bool isActive = false;
    [SerializeField]
    NavMeshAgent agent;
    BoxCollider bcollider;
    public GameObject target;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        bcollider = GameObject.Find("RIG").GetComponent<BoxCollider>();
        agent.updateRotation = false;
        agent.updatePosition = true;
    }

    void Update()
    {
        OnDetectObject(bcollider);
    }

    public void OnDetectObject(Collider collider)
    {
        if (isActive)
        {
            transform.LookAt(target.transform.position);
            agent.destination = target.transform.position;
        }
    }

    //検出範囲に入った時の処理
    void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            isActive = true;
        }
    }

    //検出範囲から出たときの処理
    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            isActive = false;
        }
    }
}