using UnityEngine;
using UnityEngine.AI;

public class KnockBackManager : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;

    private float knockBackCounter;
    private Vector3 knockBackVelocity;

    void Awake()
    {
        this.navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (IsBeingKnockedBack())
        {
            this.navMeshAgent.Move(this.knockBackVelocity * Time.deltaTime);
        }
        else
        {
            this.navMeshAgent.isStopped = false;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            // this.KnockBack(new Vector3(1, 1, 1));
        }

        this.knockBackCounter = Utils.CountDownTimer(this.knockBackCounter);
    }

    private bool IsBeingKnockedBack()
    {
        return this.knockBackCounter > 0;
    }

    public void KnockBack(Vector3 sourcePosition, int force)
    {
        Vector3 knockDirection = this.transform.position - sourcePosition;
        knockDirection = knockDirection.normalized;
        this.KnockBack(knockDirection * force);
    }

    public void KnockBack(Vector3 knockBackVelocity)
    {
        Debug.Log(this.gameObject);
        this.knockBackCounter = 0.1f;
        this.knockBackVelocity = knockBackVelocity;
        this.navMeshAgent.isStopped = true;
    }
}