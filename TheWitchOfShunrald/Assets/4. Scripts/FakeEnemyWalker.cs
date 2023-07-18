using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeEnemyWalker : MonoBehaviour
{
    [SerializeField] private Transform targetPosition;
    [SerializeField] private float speed = 5f;
    private Animator animator;

    private Coroutine moveCoroutine;

    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetFloat("BasicEnemyMove", 1f);
        moveCoroutine = StartCoroutine(MoveToPosition());
    }

    private IEnumerator MoveToPosition()
    {
        while ((transform.position - targetPosition.position).magnitude > 0.05f)
        {

            Vector3 direction = targetPosition.position - transform.position;

            transform.position += direction.normalized * speed * Time.deltaTime;

            yield return null;
        }

        animator.SetFloat("BasicEnemyMove", 0);
        StopCoroutine(moveCoroutine);
    }
}
