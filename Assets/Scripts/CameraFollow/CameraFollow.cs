using UnityEngine;

namespace CameraFollow
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private float _distanceZ;
        [SerializeField] private float _distanceX;
        [SerializeField] private float _height;
        private GameObject _player;

    
        public void Init(Player.Player playerObj)
        {
            _player = playerObj.gameObject;
        }

        void LateUpdate()
        {
            if(_player!=null)
                transform.position = new Vector3(_player.transform.position.x - _distanceX,
                                                 _player.transform.position.y - _height, _player.transform.position.z - _distanceZ);
        }
    }
}
