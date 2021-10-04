using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;

public class TetherGun : MonoBehaviour
{
    [FoldoutGroup("Componenets"), SerializeField] private HingeJoint tetherJoint;
    [FoldoutGroup("Componenets"), SerializeField] private LineRenderer tetherLine;
    [FoldoutGroup("Attributes"), SerializeField] private GameObject tetherShotPrefab;
    [FoldoutGroup("Attributes"), SerializeField, Range(0f, 200f)] private float shotSpeed = 5;
    [FoldoutGroup("Attributes"), SerializeField, Range(0f, 200f)] private float rpm = 20f;
    [FoldoutGroup("Attributes"), SerializeField, Range(0f, 200f)] private float tetherPullForce = 20f;
    [FoldoutGroup("Attributes"), SerializeField, Range(0f, 200f)] private float tetherLength = 10f;

    public bool IsTethered { get; private set; }
    //public Transform TetherTarget { get; private set; }

    public bool ReadyToFire { get; private set; }

    private void OnEnable()
    {
        InputManager.P_Input.PlayerActions.Fire.performed += context => Shoot();
        InputManager.P_Input.PlayerActions.Detach.performed += context => StopTethering();
        ReadyToFire = true;
        tetherLine.enabled = false;
    }

    private void OnDisable()
    {
        InputManager.P_Input.PlayerActions.Fire.performed -= context => Shoot();
        InputManager.P_Input.PlayerActions.Detach.performed -= context => StopTethering();
    }

    private void Shoot()
    {
        if (!IsTethered && ReadyToFire)
        {
            GameObject newShot = Instantiate(tetherShotPrefab, transform.position, transform.rotation);
            newShot.GetComponent<Rigidbody>().AddForce(transform.forward * shotSpeed, ForceMode.Impulse);
            newShot.GetComponent<TetherShot>().onHit += AttachTether;
            ReadyToFire = false;
            StartCoroutine(WaitForNextShot());
        }
    }

    private IEnumerator WaitForNextShot()
    {
        float timer = 0;
        while(timer < 60f / rpm)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        ReadyToFire = true;
    }

    private IEnumerator StartTethering(Rigidbody _target)
    {
        Transform TetherTarget = _target.transform;
        PlanetaryMovement planetaryMovement = _target.GetComponent<PlanetaryMovement>();
        planetaryMovement.onDetached += StopTethering;
        Vector3 targetDirection = (transform.position - TetherTarget.position).normalized;
        tetherLine.enabled = true;
        while(IsTethered && Vector3.Distance(transform.position, TetherTarget.position) > tetherLength)
        {
            _target.AddForce(targetDirection * tetherPullForce, ForceMode.VelocityChange);
            DisplayTetherLine(_target.transform.position);
            yield return new WaitForFixedUpdate();
        }
        _target.velocity *= 0;
        tetherJoint.connectedBody = _target;
        while (IsTethered)
        {
            DisplayTetherLine(_target.transform.position);
            yield return new WaitForFixedUpdate();
        }
        planetaryMovement.onDetached?.Invoke();
        planetaryMovement.onDetached -= StopTethering;
    }

    private void DisplayTetherLine(Vector3 _target)
    {
        tetherLine.SetPosition(0, transform.position);
        tetherLine.SetPosition(1, _target);
    }

    private void AttachTether(Rigidbody _target)
    {
        IsTethered = true;
        StartCoroutine(StartTethering(_target));
    }

    private void StopTethering()
    {
        IsTethered = false;
        tetherLine.enabled = false;
        tetherJoint.connectedBody = null;
    }
}
