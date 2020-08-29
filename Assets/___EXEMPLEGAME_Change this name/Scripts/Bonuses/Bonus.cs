using System;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != 8) return;
        
        BonusEffect();
        Destroy(gameObject);
    }

    protected virtual void BonusEffect()
    {
        
    }
}
