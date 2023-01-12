using UnityEngine;
using Zenject;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private float distanceZ;
    [SerializeField]
    private float distanceX;
    [SerializeField]
    private float height;
    private GameObject player;

    [Inject]

    private void InitiallizeComponent(Player playerObj)
    {
        player = playerObj.gameObject;
    }
  
    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new Vector3(player.transform.position.x-distanceX,
                                         player.transform.position.y-height, player.transform.position.z-distanceZ);
    }
}
