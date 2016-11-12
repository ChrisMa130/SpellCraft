using UnityEngine;
public class Projectile : MonoBehaviour
{
    // stats of the medium spell
    //private string spellName = "incendio";
    public int damage = 20;
    public int mpCost = 10;

    void Awake()
    {
        GetComponent<EffectSettings>().CollisionEnter += Instance_OnCollisionEnter;
    }

    // reduce Player health on collision, and destroy spell instance
    void Instance_OnCollisionEnter(object sender, CollisionInfo e)
    {
        Debug.Log("OnCollisionEnter");
        if (e.Hit.collider.tag.Equals("MainCamera"))
        {
            Debug.Log("InsideFunction");
            // reduce player health
            e.Hit.collider.gameObject.GetComponent<Player>().modifyHealth(this.damage);
        }
    }
}