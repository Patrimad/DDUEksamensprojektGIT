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
    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & collectibleLayer) != 0)
        {
            var col = other.gameObject.GetComponent<Collectibles>();
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
