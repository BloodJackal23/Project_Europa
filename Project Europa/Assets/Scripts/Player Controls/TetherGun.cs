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
    public Transform TetherTarget { get; private set; }

    public bool ReadyToFire { get; private set; }

    private void OnEnable()
    {
        InputManager.P_Input.PlayerActions.Fire.performed += context => Shoot();
        ReadyToFire = true;
        tetherLine.enabled = false;
    }

    private void OnDisable()
    {
        InputManager.P_Input.PlayerActions.Fire.performed -= context => Shoot();
    }

    private void Shoot()
    {
        if (!IsTethered && ReadyToFire)
        {
            GameObject newShot = Instantiate(tetherShotPrefab, transform.position, transform.rotation);
            newShot.GetComponent<Rigidbody>().AddForce(transform.forward * shotSpeed, ForceMode.Impulse);
            newShot.GetComponent<TetherShot>().onTethered += AttachTether;
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
        TetherTarget = _target.transform;
        Vector3 targetDirection = (transform.position - TetherTarget.position).normalized;
        tetherLine.enabled = true;
        while(IsTethered && Vector3.Distance(transform.position, TetherTarget.position) > tetherLength)
        {
            _target.MovePosition(TetherTarget.position + targetDirection * tetherPullForce * Time.fixedDeltaTime);
            DisplayTetherLine();
            yield return new WaitForFixedUpdate();
        }
        _target.velocity *= 0;
        tetherJoint.connectedBody = _target;
        while (IsTethered)
        {
            DisplayTetherLine();
            yield return new WaitForFixedUpdate();
        }
        DetachTether(_target);
    }

    private void DisplayTetherLine()
    {
        tetherLine.SetPosition(0, transform.position);
        tetherLine.SetPosition(1, TetherTarget.position);
    }

    private void AttachTether(Rigidbody _target)
    {
        IsTethered = true;
        StartCoroutine(StartTethering(_target));
    }

    private void DetachTether(Rigidbody _target)
    {
        IsTethered = false;
        tetherLine.enabled = false;
        tetherJoint.connectedBody = null;
    }
}
