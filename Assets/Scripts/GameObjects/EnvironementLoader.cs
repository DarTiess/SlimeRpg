using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnvironementLoader : MonoBehaviour
{
    [SerializeField]private List<Environement> planes = new List<Environement>();
    private int _numPlanes = 0;
    private bool _starting;

    private EnemyLoader _enemyLoader;
    private NavMeshSettings _navMeshSettings;

    [Inject]

    private void Initialize(EnemyLoader loader, NavMeshSettings navMesh)
    {
        _enemyLoader= loader;
        _navMeshSettings= navMesh;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadPlane(Transform pointToLoad)
    { 
        if (_numPlanes > 0)
        {
            planes[_numPlanes-1].gameObject.SetActive(false);
        }
        else
        {
            planes[planes.Count-1].gameObject.SetActive(false);
        }
        _numPlanes++;
        if (_numPlanes >= planes.Count)
        {
            _numPlanes= 0;
        }
        planes[_numPlanes].gameObject.SetActive(true);
        planes[_numPlanes].startPoint.position= pointToLoad.position;
        _enemyLoader.gameObject.transform.position=planes[_numPlanes].endPoint.position;
        _navMeshSettings.UpdateNavMesh();

    }
    
  
}
