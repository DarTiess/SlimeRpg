using System;
using System.Collections.Generic;
using Infrastructure.GameStates;
using UnityEngine;

namespace Environement
{
    public class EnvironementLoader
    {
        public event Action<Transform> CreatePlane;
        private List<Environement> _planes = new List<Environement>();
        private int _numPlanes = 0;

        private IGameStates _gameStates;
        private INavMeshSettings _navMeshSettings;
        public EnvironementLoader(List<Environement> planes, IGameStates gameStates, INavMeshSettings navMesh)
        {
            _gameStates = gameStates;
            _navMeshSettings = navMesh;
            foreach (Environement plane in planes)
            {
                _planes.Add(plane);
                plane.TriggerPlayer += OnTriggerPlayer;
            }

            for (int i = 1; i < _planes.Count; i++)
            {
                _planes[i].gameObject.SetActive(false);
            }
        }

        private void OnTriggerPlayer(Environement obj)
        {
            obj.gameObject.SetActive(false);
            LoadPlane(obj.EndPoint);
        }
        private void LoadPlane(Transform pointToLoad)
        {
            _numPlanes++;
       
            if (_numPlanes >= _planes.Count)
            {
                _numPlanes = 0;
            }
       
            _planes[_numPlanes].SetOnPosition(pointToLoad);
            _planes[_numPlanes].gameObject.SetActive(true);
            _navMeshSettings.UpdateNavMesh();
          _gameStates.OnCreatePlane(_planes[_numPlanes].EndPoint);
        }
    }
}
