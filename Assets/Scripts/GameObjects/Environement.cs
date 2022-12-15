using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Environement : MonoBehaviour
{
    
    [SerializeField]
    public Transform startPoint;
    [SerializeField]
    public Transform endPoint;
    private EnvironementLoader _environementLoader;

    [Inject]

    private void InitiallizeComponent(EnvironementLoader loader)
    {
       _environementLoader= loader;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _environementLoader.LoadPlane(endPoint);
        }
    }
}
