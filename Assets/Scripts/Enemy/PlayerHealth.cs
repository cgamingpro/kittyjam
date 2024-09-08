using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float hitpoints = 100f;
     Weapon weapon;

    //Create a public method which reduces HP by amount of damage   
    public void TakeDamage(float damage)
    {
       
        hitpoints -= damage;

        if (hitpoints <= 0)
        {
            GameObject.Destroy(gameObject);
        }
    }

    void Start()
    {
        weapon = GetComponent<Weapon>();
    }
}


