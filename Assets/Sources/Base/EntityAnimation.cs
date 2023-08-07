using UnityEngine;

public sealed class EntityAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private EntitySpecification _entitySpecification;

    public void SetSpecification(EntitySpecification specification)
    {
        _entitySpecification = specification;
    }

    public void AnimationPlay(AnimationType animationType)
    {
        switch (animationType)
        {
            case AnimationType.AnimationIdle: _animator.Play("idleAnim"); break;
            case AnimationType.AnimationRun: _animator.Play("runAnim"); break;
            case AnimationType.AnimationAttack:
                _animator.SetFloat("AttackSpeedMultiply", _entitySpecification.AttackSpeed);
                _animator.Play("attackAnim");
                break;
            case AnimationType.AnimationDie: _animator.Play("dieAnim"); break;
        }
    }
}