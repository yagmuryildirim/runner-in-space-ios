using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeDetection : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private Player player;
    [SerializeField] private LayerMask whatIsGround;

    //private bool canDetect;

    private BoxCollider2D boxCollider => GetComponent<BoxCollider2D>();

    private void Update()
    {
        //if (canDetect) player.ledgeDetected = Physics2D.OverlapCircle(transform.position, radius, whatIsGround);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            //canDetect = false;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        /*  Collider2D[] colliders = Physics2D.OverlapBoxAll(boxCollider.bounds.center, boxCollider.size, 0);

         foreach (var hit in colliders)
         {
             if hit.platform controller != null 
                return
         } */

        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            //canDetect = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
