using UnityEngine;
using Zenject;

public class Environement : MonoBehaviour
{

    [SerializeField] public Transform startPoint;
    [SerializeField] public Transform endPoint;
    private EnvironementLoader _environementLoader;

    [Inject]
    private void Construct(EnvironementLoader loader)
    {
        _environementLoader = loader;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _environementLoader.LoadPlane(endPoint);
        }
    }
}
