using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="SpawnUnitEffect",menuName ="Effect/SpawnUnitEffect")]
public class SpawnUnitEffect : Effect
{
    [SerializeField] Unit unitPrefab;

    [SerializeField] int Amount;

    public override void GenerateNewEffectValues()
    {
        //lol
    }

    public override string RetrieveEffectToolTip()
    {
        return "Powerstation spawns a new Drone.";
    }

    override public void TriggerEffect()
    {
        for(int i = 0; i < Amount; i++)
        {
            MonoBehaviour.Instantiate(unitPrefab);
        }

        
    }
}
