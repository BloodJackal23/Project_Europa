using System.Collections;
using UnityEngine;

public class ObjectLifespan : MonoBehaviour
{
    [SerializeField] private float lifeSpan = 4f;
    void Start()
    {
        StartCoroutine(RunLifeSpan());
    }

    private IEnumerator RunLifeSpan()
    {
        yield return new WaitForSeconds(lifeSpan);
        Destroy(gameObject);
    }

    public void PauseDestruction()
    {
        StopAllCoroutines();
    }
}
