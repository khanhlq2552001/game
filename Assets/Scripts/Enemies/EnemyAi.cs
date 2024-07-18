using System.Collections;
using UnityEngine;

public class EnemiAi : MonoBehaviour
{
    private enum State
    {
        Roaming
    }
    [SerializeField] private float roamingChangeDir = 2f;
    private State state;
    private EnemyPathfinding enemyPathfinding;

    private void Awake()
    {
        enemyPathfinding = GetComponent<EnemyPathfinding>();
        state = State.Roaming;
    }
    private void Start()
    {
        StartCoroutine(RoamingRoutine());

    }
    private IEnumerator RoamingRoutine()
    {
        while (state == State.Roaming)
        {
            Vector2 roamingPos = GetRoamingPositon();
            enemyPathfinding.MoveTo(roamingPos);
            yield return new WaitForSeconds(roamingChangeDir);
        } 
    }

    private Vector2 GetRoamingPositon()
    {
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
    }

}
