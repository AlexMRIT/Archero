using UnityEngine;

public abstract class HookEntity : MonoBehaviour
{
    [SerializeField] private LayerMask _mask;

    private Transform _target;

    public virtual bool TryFindUnhiddenTarget(Transform prefferedTarget, out float distance)
    {
        Vector3 direction = (prefferedTarget.position - transform.position).normalized;
        float distanceToPrefferedTarget = Vector3.Distance(transform.position, prefferedTarget.position);

        distance = distanceToPrefferedTarget;
        return !Physics.Raycast(transform.position, direction, distanceToPrefferedTarget, _mask);
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    public void ResetTarget()
    {
        _target = null;
    }

    public bool HasTargetTake()
    {
        return _target != null;
    }
}