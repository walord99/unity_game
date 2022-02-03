using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackScript : MonoBehaviour
{
    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] public Transform attackPos;
    [SerializeField] public LayerMask enemies;
    [SerializeField] private int attackDamage = 1;
    [SerializeField] private int knockbackStrenght;
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Collider2D[] hitList = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemies);
            for (int i = 0; i < hitList.Length; i++)
            {
                hitList[i].GetComponent<Enemy>().TakeDamage(attackDamage, attackPos.position, knockbackStrenght);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
