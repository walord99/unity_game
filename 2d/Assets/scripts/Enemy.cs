using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D enemyRB;
    void Start()
    {
        enemyRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int dmg, Vector2 dmgOrigin, float knockbackStrenght = 0)
    {
        Debug.Log("Damage Taken: " + dmg);
        Vector2 knockbackDirection = enemyRB.position - dmgOrigin;
        enemyRB.velocity = knockbackDirection.normalized * knockbackStrenght;
    }
}
