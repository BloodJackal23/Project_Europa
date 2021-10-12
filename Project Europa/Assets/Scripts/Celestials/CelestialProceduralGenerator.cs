using UnityEngine;

namespace Procedural
{
    public static class CelestialProceduralGenerator
    {
        public static CelestialAttributes RandomizedAttributes(ProceduralPlanetLibrary _library)
        {
            float randMass = Random.Range(_library.MassRange[0], _library.MassRange[1]);
            float randScale = Random.Range(_library.ScaleRange[0], _library.ScaleRange[1]);
            float randSpeed = Random.Range(_library.OrbitalSpeedRange[0], _library.OrbitalSpeedRange[1]);
            int namesLength = _library.PlanetsNames.Length;
            string randName = _library.PlanetsNames[Random.Range(0, namesLength)] + "-" + Random.Range(0, 1000).ToString("000");
            Material randMat = _library.Materials[Random.Range(0, _library.Materials.Length)];
            return new CelestialAttributes(randMass, randScale, randSpeed, randName, randMat);
        }
    }
}

