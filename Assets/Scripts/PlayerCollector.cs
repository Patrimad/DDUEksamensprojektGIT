using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    //public KeyCode interactKey = KeyCode.E;
    //public float interactRadius = 1.2f;
    public LayerMask collectibleLayer;

    //void Update()
    //{
    //    // simpelt nærheds-tjek + keypress
    //    if (Input.GetKeyDown(interactKey))
    //    {
    //        //Collider[] hits = Physics.OverlapSphere(transform.position, interactRadius, collectibleLayer);
    //        //if (hits.Length > 0)
    //        //{
    //        //    // vælg første collectible
    //        //    var col = hits[0].GetComponent<Collectibles>();
    //        //    if (col != null)
    //        //    {
    //        //        col.Collect();
    //        //    }
    //        //}
    //    }
    //}
    private void OnCollisionEnter(Collision collision)
    {
        if (((1 << collision.gameObject.layer) & collectibleLayer) != 0)
        {
            var col = collision.gameObject.GetComponent<Collectibles>();
            if (col != null)
            {
                col.Collect();
            }
        }
    }


    //void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawWireSphere(transform.position, interactRadius);
    //}
}
