using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;

public class TetherShooter : MonoBehaviour
{
    [FoldoutGroup("Attributes"), SerializeField] private GameObject tetherShotPrefab;
    [FoldoutGroup("Attributes"), SerializeField, Range(0f, 200f)] private float force = 5;
    [FoldoutGroup("Attributes"), SerializeField, Range(0f, 200f)] private float rpm = 20f;

    public bool CanFire { get; private set; }

    private void OnEnable()
    {
        InputManager.P_Input.PlayerActions.Fire.performed += context => Shoot();
        CanFire = true;
    }

    private void OnDisable()
    {
        InputManager.P_Input.PlayerActions.Fire.performed -= context => Shoot();
    }

    private void Shoot()
    {
        if (CanFire)
        {
            GameObject newShot = Instantiate(tetherShotPrefab, transform.position, transform.rotation);
            newShot.GetComponent<Rigidbody>().AddForce(transform.forward * force, ForceMode.Impulse);
            CanFire = false;
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
        CanFire = true;
    }
}
