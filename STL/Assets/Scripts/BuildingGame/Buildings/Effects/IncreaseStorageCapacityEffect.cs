using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChangeStorageCapacityEffect", menuName = "Effect/ChangeStorageCapacityEffect")]
public class IncreaseStorageCapacityEffect : Effect
{
    PlayerInventory inventory;

    Ressource resType;
    int amount;
    [SerializeField] List<Ressource> ressources;
    [SerializeField] int minAmount;
    [SerializeField] int maxAmount;

    public override void GenerateNewEffectValues()
    {
        GenerateEffect();
    }

    public override string RetrieveEffectToolTip()
    {
        GenerateEffect();
        return $"Increase Storage for Ressource:{resType} by {amount}";
    }

    public override void TriggerEffect()
    {
        if(amount == 0)
        {
            GenerateEffect();
        }
        Debug.Log(RetrieveEffectToolTip());
        GameManager.Instance.playerInventory.ChangeCapacity(resType, amount);
    }

    private void GenerateEffect()
    {
        resType = ressources[Random.Range(0, ressources.Count)];
        amount = Random.Range(minAmount, maxAmount);
    }
}
