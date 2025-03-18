using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class IntroAgent : MonoBehaviour
{
    // 네비메쉬를 Bake하기우한 NavMeshSurface 오브젝트
    [SerializeField] private NavMeshSurface surface;

    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    /// <summary>
    /// 동적 네비메쉬 Bake
    /// </summary>
    public void PassivityUpdateNavmesh()
    {
        surface.transform.position = transform.position;
        surface.BuildNavMesh();
    }

    /// <summary>
    /// 목표 지점 설정
    /// </summary>
    /// <param name="destination"></param>
    public void SetNewDestination(Vector3 destination)
    {
        PassivityUpdateNavmesh();
        agent.SetDestination(destination);
    }
}
