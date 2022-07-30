using UnityEngine;

public class Entity : MonoBehaviour
{
    public virtual void TakeDamage() { }
    
    protected virtual void Die() => this.gameObject.SetActive(false);
}