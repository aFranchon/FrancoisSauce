using FrancoisSauce.Scripts.FSEvents.SO;
using UnityEngine;

public class BonusLife : Bonus
{
    [SerializeField] private FSIntEventSO onPlayerGainLife = null;
    public int lifeGained = 1;
    
    protected override void BonusEffect()
    {
        base.BonusEffect();
        
        onPlayerGainLife.Invoke(lifeGained);
    }
}
