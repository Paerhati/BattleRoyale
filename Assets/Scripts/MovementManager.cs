using UnityEngine;

public class MovementManager : MonoBehaviour
{
    public Joystick Joystick;

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
                var horizontal = Input.GetAxis("Horizontal");
                horizontal = horizontal == 0 ? Joystick.Horizontal : horizontal;

                var vertical = Input.GetAxis("Vertical");
                vertical = vertical == 0 ? Joystick.Vertical : vertical;

                moveDirection.x = horizontal;
                moveDirection.z = vertical;
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