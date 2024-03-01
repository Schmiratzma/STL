using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu]
public class WorldSettings : ScriptableObject
{
    FastNoiseLite noise;

    public bool useCustomSeed;
    [EnableIf("useCustomSeed")]
    public int seed;

    public float frequency;
    public FastNoiseLite.NoiseType noiseType;

    [Header("Fractal")]
    [SerializeField] bool useFractal;
    [SerializeField,EnableIf("useFractal")] FastNoiseLite.FractalType fractalType;
    [SerializeField,EnableIf("useFractal")] int Octaves;
    [SerializeField,EnableIf("useFractal")] float Lacunarity;
    [SerializeField,EnableIf("useFractal")] float Gain;
    [SerializeField,EnableIf("useFractal")] float WeightedStrength;
    [SerializeField,EnableIf("useFractal")] float PingPongStrength;

    [Header("Cellular")]
    [SerializeField] bool useCellular;
    [SerializeField,EnableIf("useCellular")] FastNoiseLite.CellularDistanceFunction cellularDistanceFunction;
    [SerializeField,EnableIf("useCellular")] FastNoiseLite.CellularReturnType cellularReturnType;
    [SerializeField,EnableIf("useCellular")] float Jitter;

    [Header("DomainWarp")]
    [SerializeField] bool useDomainWarp;
    [SerializeField,EnableIf("useDomainWarp")] FastNoiseLite.DomainWarpType domainWarpType;
    [SerializeField,EnableIf("useDomainWarp")] FastNoiseLite.CellularReturnType returnType;
    [SerializeField,EnableIf("useDomainWarp")] float Amplitude;
    [SerializeField,EnableIf("useDomainWarp")] int Seed;
    [SerializeField,EnableIf("useDomainWarp")] float DomainFrequency;


    public void MakeNoise()
    {
        noise.SetNoiseType(noiseType);
        noise.SetFrequency(frequency);
        if (!useCustomSeed)
        {
            seed = Random.Range(0, 5000);
        }
        noise.SetSeed(seed);

        if (useFractal)
        {
            noise.SetFractalType(fractalType);
            noise.SetFractalOctaves(Octaves);
            noise.SetFractalLacunarity(Lacunarity);
            noise.SetFractalGain(Gain);
            noise.SetFractalWeightedStrength(WeightedStrength);
            noise.SetFractalPingPongStrength(PingPongStrength);
        }

        if (useCellular)
        {
            noise.SetCellularDistanceFunction(cellularDistanceFunction);
            noise.SetCellularReturnType(cellularReturnType);
            noise.SetCellularJitter(Jitter);
        }

        if (useDomainWarp)
        {
            noise.SetDomainWarpType(domainWarpType);
            noise.SetCellularReturnType(returnType);
            noise.SetDomainWarpAmp(Amplitude);
            //dunno how to set the rest
        }
    }

    public float GetNoise(Vector3 position)
    {
        return noise.GetNoise(position.x, position.y, position.z);
    }
}
