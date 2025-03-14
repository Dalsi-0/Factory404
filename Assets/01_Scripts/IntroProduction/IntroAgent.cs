using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class IntroAgent : MonoBehaviour
{
    [SerializeField] private NavMeshSurface surface;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
       // StartCoroutine(UpdateNavmesh());
    }

    void Update()
    {
        /*if (Vector3.Distance(surface.transform.position, transform.position) > 5f)
        {
            surface.transform.position = transform.position;
            surface.BuildNavMesh();
        }*/
    }

    public void PassivityUpdateNavmesh()
    {
        surface.transform.position = transform.position;
        surface.BuildNavMesh();
    }

    IEnumerator UpdateNavmesh()
    {
        while (true)
        {
            surface.transform.position = transform.position;
            surface.BuildNavMesh();

            yield return new WaitForSeconds(1f);
        }
    }

    public void SetNewDestination(Vector3 destination)
    {
        PassivityUpdateNavmesh();
        agent.SetDestination(destination);
    }
}
