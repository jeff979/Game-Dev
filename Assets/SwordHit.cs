using UnityEngine;

public class SwordHit : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")) 
        {
            Destroy(other.gameObject); 
            FindObjectOfType<EnemySpawner>().EnemyDied();
        }
    }
}