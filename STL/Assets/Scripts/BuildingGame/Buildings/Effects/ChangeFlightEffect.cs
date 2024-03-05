using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using static UnityEngine.CullingGroup;

public enum FlightCategory { Pitch, Yaw, Roll, Fly }

[System.Serializable]
public class FlyStatChange
{
    [SerializeField] public FlightCategory Category;
    [SerializeField] public float amount;
    [SerializeField] public int weight;
}

[CreateAssetMenu(fileName = "ChangeFlightEffect", menuName = "Effect/ChangeFlightEffect")]
public class ChangeFlightEffect : Effect
{
    [SerializeField] ShipDataSO shipDataSO;

    [SerializeField] public List<FlyStatChange> statChangeList;

    [SerializeField] public int weigtSumm;

    Dictionary<FlightCategory, float> effect;

    public override void TriggerEffect()
    {
        if (effect == null)
        {
            Debug.Log("I Create a new Effect");
            createEffect();
        }
        foreach (KeyValuePair<FlightCategory, float> kvp in effect)
        {
            Debug.Log("I go through every effet");
            switch (kvp.Key)
            {
                case FlightCategory.Pitch:
                    Debug.Log("i increase pitch");
                    shipDataSO.pitchSpeed += kvp.Value;
                    break;
                case FlightCategory.Yaw:
                    Debug.Log("i increase yaw");
                    shipDataSO.yawSpeed += kvp.Value;
                    break;
                case FlightCategory.Roll:
                    Debug.Log("i increase roll");
                    shipDataSO.rollSpeed += kvp.Value;
                    break;
                case FlightCategory.Fly:
                    Debug.Log("i increase fly");
                    shipDataSO.flySpeed += kvp.Value;
                    break;
            }
        }
    }

    private void createEffect()
    {
        effect = new Dictionary<FlightCategory, float>();
        int currentweight = 0;
        while (currentweight < weigtSumm)
        {
            FlyStatChange statChange = statChangeList[Random.Range(0, statChangeList.Count)];

            if (currentweight + statChange.weight > weigtSumm)
            {
                continue;
            }

            currentweight += statChange.weight;
            if (effect.ContainsKey(statChange.Category))
            {
                effect[statChange.Category] += statChange.amount;
            }
            else
            {
                effect.Add(statChange.Category, statChange.amount);
            }
        }
    }

    public override string RetrieveEffectToolTip()
    {
        string result = "Turbine with: \n";
        createEffect();
        foreach (KeyValuePair<FlightCategory, float> kvp in effect)
        {
            switch (kvp.Key)
            {
                case FlightCategory.Pitch:
                    result += "Pitch: " + kvp.Value+"\n";
                    break;
                case FlightCategory.Yaw:
                    result += "Yaw: " + kvp.Value+"\n";
                    break;
                case FlightCategory.Roll:
                    result += "Roll: " + kvp.Value + "\n";
                    break;
                case FlightCategory.Fly:
                    result += "Velocity: " + kvp.Value + "\n";
                    break;
            }
        }
        return result;
    }
}
