using UnityEngine;

namespace PlayerSystem
{
    public class CoordinateFromCamera : MonoBehaviour
    {
        [SerializeField] private Player _player;

        private Vector3 _offset;

        private void Start()
        {
            _offset = _player.transform.position - transform.position;
        }

        private void LateUpdate()
        {
            transform.position = _player.transform.position - _offset;
        }
    }
}