using System;
using UnityEngine;

internal sealed class Player : Entity
{
    [SerializeField] private Joystick _joystick;

    private PlayerHookEntity _playerHookEntity;
    private Entity _target;
    private float _lastTimeShoot;

    private void Awake()
    {
        EntityCharacterController = GetComponent<CharacterController>();

        EntityAnimationController = GetComponent<EntityAnimation>();
        EntityAnimationController.SetSpecification(EntitySpec);

        _playerHookEntity = GetComponent<PlayerHookEntity>();
    }

    private void Update()
    {
        Vector3 direction = new Vector3(_joystick.Horizontal, 0.0f, _joystick.Vertical).normalized;
        direction = EntitySpec.MovementSpeed * Time.deltaTime * direction;

        if (direction.magnitude >= 0.01f)
        {
            transform.rotation = QuaternionExtension.RotateWithDirection(direction);
            EntityAnimationController.AnimationPlay(AnimationType.AnimationRun);
        }
        else
        {
            if (_target != null)
            {
                transform.LookAt(_target.transform);

                if (Time.time - _lastTimeShoot >= (unchecked(1 / EntitySpec.AttackSpeed)))
                {
                    _lastTimeShoot = Time.time;
                    EntityAnimationController.AnimationPlay(AnimationType.AnimationAttack);
                }
            }
            else
            {
                EntityAnimationController.AnimationPlay(AnimationType.AnimationIdle);
            }
        }

        Move(direction);
    }

    private void FixedUpdate()
    {
        if (!_playerHookEntity.HasTargetTake())
        {
            if (!_playerHookEntity.TryHook(out Entity entity))
                return;

            _target = entity;
            _playerHookEntity.SetTarget(entity.transform);
        }
        else
        {
            _playerHookEntity.ResetTarget();
        }

    }

    protected override void DeathEvent(Entity attacker)
    {
        
    }

    public override void Move(Vector3 direction)
    {
        base.Move(direction);
    }

    public override void Spawn(Location location)
    {
        int countCubeX = (int)location.GetSizeLocation().x;
        int countCubeY = (int)location.GetSizeLocation().y;

        for (int iterY = 0; iterY < countCubeY; iterY++)
        {
            for (int iterX = 0; iterX < countCubeX; iterX++)
            {
                if (iterY == 0 || iterX == 0 || iterY == countCubeY - 1 || iterX == countCubeX - 1)
                    continue;

                if (iterY < countCubeY - 2)
                    continue;

                if (location.GetLocation()[iterX, iterY] != null)
                    continue;

                Transform locTransf = location.GetPlaneLocation().transform;
                float lenPlane = location.GetLengthPlane();
                float spacing = location.GetSpacing();

                float posX = ((locTransf.position.x + 1) * iterX) - lenPlane - 1.25f + (spacing * iterX);
                float posY = locTransf.position.y;
                float posZ = (locTransf.position.z * lenPlane - iterY) - (spacing * iterY);

                transform.position = new Vector3(posX, posY, posZ);
                return;
            }
        }
    }

    public void SetHookEntity(PlayerHookEntity playerHookEntity)
    {
        _playerHookEntity = playerHookEntity;
    }
}