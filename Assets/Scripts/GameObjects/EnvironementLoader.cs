using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnvironementLoader : MonoBehaviour
{
    [SerializeField] private List<Environement> _planes;
    private int _numPlanes = 0;
    private bool _starting;

    private EnemyLoader _enemyLoader;
    private NavMeshSettings _navMeshSettings;

    [Inject]

    private void Construct(EnemyLoader loader, NavMeshSettings navMesh)
    {
        _enemyLoader = loader;
        _navMeshSettings = navMesh;
    }

    public void LoadPlane(Transform pointToLoad)
    {
        if (_numPlanes > 0)
        {
            _planes[_numPlanes - 1].gameObject.SetActive(false);
        }
        else
        {
            _planes[_planes.Count - 1].gameObject.SetActive(false);
        }
        _numPlanes++;
        if (_numPlanes >= _planes.Count)
        {
            _numPlanes = 0;
        }
        _planes[_numPlanes].gameObject.SetActive(true);
        _planes[_numPlanes].startPoint.position = pointToLoad.position;
        _enemyLoader.gameObject.transform.position = _planes[_numPlanes].endPoint.position;
        _navMeshSettings.UpdateNavMesh();

    }
}
