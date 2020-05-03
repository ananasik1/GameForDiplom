using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EnemyState
{
    idle,
    walk,
    attack,
    stagger
}
public class Enemy : MonoBehaviour
{
    public EnemyState CurrentState;
    public FloatValue maxHealth;
    public float Health;
    public string EnemyName;
    public int baseAttack;
    public float MoveSpeed;


    private void Awake()
    {
        Health = maxHealth.initialValue;
    }
    private void TakeDamage(float damage)
    {
        Health -= damage;
        if (Health<=0)
        {
            this.gameObject.SetActive(false);
        }
    }
    public void Knock(Rigidbody2D rigidbody, float KnockTime, float damage)
    {
        StartCoroutine(KnockCo(rigidbody, KnockTime));
        TakeDamage(damage);
    }
    private IEnumerator KnockCo(Rigidbody2D rigidbody, float KnockTime)
    {
        if (rigidbody != null )
        {
            yield return new WaitForSeconds(KnockTime);
            rigidbody.velocity = Vector2.zero;
            CurrentState = EnemyState.idle;
            rigidbody.velocity = Vector2.zero;
        }
    }
}
