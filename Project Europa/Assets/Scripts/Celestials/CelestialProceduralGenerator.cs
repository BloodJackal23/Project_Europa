using UnityEngine;

namespace Procedural
{
    public static class CelestialProceduralGenerator
    {
        public static CelestialAttributes RandomizedAttributes(ProceduralPlanetLibrary _library)
        {
            float randDensity = Random.Range(_library.DensityRange[0], _library.DensityRange[1]);
            float randVolMul = Random.Range(_library.VolumeMultiplierRange[0], _library.VolumeMultiplierRange[1]);
            int namesLength = _library.PlanetsNames.Length;
            string randName = _library.PlanetsNames[Random.Range(0, namesLength)] + "-" + Random.Range(0, 1000).ToString("000");
            Material randMat = _library.Materials[Random.Range(0, _library.Materials.Length)];
            return new CelestialAttributes(randDensity, randVolMul, randName, randMat);
        }
    }
}

