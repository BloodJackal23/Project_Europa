using UnityEngine;

public class MovingObjectMarker : ObjectMarker
{
    private void Update()
    {
        transform.position = new Vector3(targetObject.position.x, yOffset, targetObject.position.z);
    }

    virtual protected void OnTargetDestroyed()
    {
        Destroy(gameObject);
    }
}
