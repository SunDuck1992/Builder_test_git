using UnityEngine;
using UnityEngine.AI;

namespace PlayerSystem
{
    [RequireComponent(typeof(Rigidbody))]

    public class PlayerMovement : MonoBehaviour
    {
        private const string VerticalDirection = "Vertical";
        private const string HorizontalDirection = "Horizontal";
        private const string SpeedMultyPlie = "speedMulti";

        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private float _speed;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private Joystick _joystick;

        private Vector3 movement;

        public Animator Animator { get; set; }

        private void Start()
        {
            _agent.speed = _speed * UpgradePlayer.Instance.LevelSpeed;
        }

        private void FixedUpdate()
        {
            Move();
            Rotate();
        }

        private void Move()
        {
            float xDirection = _joystick.Horizontal;
            float zDirection = _joystick.Vertical;

            if (Input.GetAxisRaw(HorizontalDirection) != 0
                | Input.GetAxisRaw(VerticalDirection) != 0)
            {
                xDirection = Input.GetAxisRaw(HorizontalDirection);
                zDirection = Input.GetAxisRaw(VerticalDirection);
            }

            movement = new Vector3(xDirection, 0, zDirection);
            Animator.SetFloat(SpeedMultyPlie, UpgradePlayer.Instance.LevelSpeed);
            _agent.speed = _speed * UpgradePlayer.Instance.LevelSpeed;
            _agent.velocity = movement.normalized * _agent.speed;

            if (xDirection != 0 || zDirection != 0)
            {
                Animator.SetBool(HashPlayerAnimations.Walk, true);
            }
            else
            {
                Animator.SetBool(HashPlayerAnimations.Walk, false);
            }
        }

        private void Rotate()
        {
            if (movement != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(movement);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, _rotationSpeed);
            }
        }
    }
}