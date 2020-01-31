using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{

    //オブジェクトと接触した瞬間に呼び出される
    void OnTriggerEnter(Collider collider)
    {
        var targetMob = collider.GetComponent<MobStatus>();

        if (null == targetMob) return;

        //攻撃した相手がEnemyの場合
        if (collider.CompareTag("Enemy"))
        {
            targetMob.Damage(1);
        }
    }
}