using UnityEngine;
using Sirenix.OdinInspector;

public class PlanetaryOrbit : CelestialMovement
{
    [FoldoutGroup("External References"), SerializeField] private CelestialObject star;
    [FoldoutGroup("Attributes"), SerializeField] protected bool clockwiseOrbit = true;

    protected override void Awake()
    {
        base.Awake();
        if (!star)
            star = GameObject.FindGameObjectWithTag("Star").GetComponent<CelestialObject>();
    }

    private void Start()
    {
        StartPlanetaryOrbit();
    }

    private void FixedUpdate()
    {
        Vector3 starDirection = (star.transform.position - transform.position).normalized;
        celestialObject.RigidBody.AddForce(starDirection * celestialObject.GetGravitationalForce(star));
    }

    public void StartPlanetaryOrbit()
    {
        int cw = -1;
        if (clockwiseOrbit)
            cw *= cw;
        celestialObject.RigidBody.velocity += celestialObject.GetInitialVelocity(star) * cw;
    }
}
