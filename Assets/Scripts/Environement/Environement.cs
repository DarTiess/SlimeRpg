using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Environement
{
    public class Environement : MonoBehaviour
    {
        public event Action<Environement> TriggerPlayer;
        [SerializeField] private Transform _endPoint;
        public Transform EndPoint => _endPoint;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Player.Player>(out Player.Player player))
            {
                TriggerPlayer?.Invoke(this);
            }
        }

        public void SetOnPosition(Transform pointToLoad)
        {
            transform.position = pointToLoad.position;
        }
    }
}
