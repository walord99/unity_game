using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    Rigidbody2D enemyRB;
    // Start is called before the first frame update
    void Start()
    {
        enemyRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int dmg, Vector2 dmgOrigin, float knockbackStrenght)
    {
        Debug.Log("Damage Taken: " + dmg);
        Vector2 knockbackDirection = enemyRB.position - dmgOrigin;
        enemyRB.velocity = knockbackDirection.normalized * knockbackStrenght;
    }
}
