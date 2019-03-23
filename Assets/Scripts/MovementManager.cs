using UnityEngine;

public class MovementManager : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 velocity;

    private float knockBackCounter;
    private Vector3 knockBackVelocity;

    public void KnockBack(Vector3 direction, float knockBackForce = 20f)
    {
        this.knockBackCounter = 0.1f;
        this.knockBackVelocity = direction * knockBackForce;
    }

    void Start()
    {
        this.controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        var moveDirection = new Vector3();

        if (BeingKnockedBack())
        {
            moveDirection.x = this.knockBackVelocity.x;
            moveDirection.z = this.knockBackVelocity.z;
            this.knockBackCounter -= Time.deltaTime;
        }
        else
        {
            if (controller.isGrounded)
            {
                moveDirection.x = Input.GetAxis("Horizontal");
                moveDirection.z = Input.GetAxis("Vertical");
                moveDirection *= 10f;
            }
        }

        float gravity = 10f;
        moveDirection.y = moveDirection.y - gravity;

        this.controller.Move(moveDirection * Time.deltaTime);
    }

    private bool BeingKnockedBack()
    {
        return this.knockBackCounter > 0;
    }

}