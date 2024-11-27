using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorHandler : MonoBehaviour
{
    public Animator anim;
    float horizontal;
    float vertical;
    public bool canRotate;

    private void Start()
    {
        Initialize();
        CanRotate();
    }

    void Initialize()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateAnimatorValues(float verticalMovement, float horizontalMovement)
    {
        #region Vertical
        float v = 0;

        if (verticalMovement > 0 && verticalMovement < 0.55f)
        {
            v = 0.55f;
        }
        else if (verticalMovement > 0.55f)
        {
            v = 1;
        }
        else if (verticalMovement < 0 && verticalMovement > -0.55f)
        {
            v = -0.5f;
        }
        else if (verticalMovement < -0.55f)
        {
            v = -1;
        }
        else
        {
            v = 0;
        }
        #endregion

        #region Horizontal
        float h = 0;

        if (horizontalMovement > 0 && horizontalMovement < 0.55f)
        {
            h = 0.55f;
        }
        else if (horizontalMovement > 0.55f)
        {
            h = 1;
        }
        else if (horizontalMovement < 0 && horizontalMovement > -0.55f)
        {
            h = -0.5f;
        }
        else if (horizontalMovement < -0.55f)
        {
            h = -1;
        }
        else
        {
            h = 0;
        }
        #endregion

        anim.SetFloat("Vertical", v, 0.1f, Time.deltaTime);
        anim.SetFloat("Horizontal", h, 0.1f, Time.deltaTime);
    }

    public void HandleJumpAnim()
    {
        anim.SetTrigger("isJump");
    }

    public void HandleDodgeAnim()
    {
        anim.SetTrigger("isDodge");
    }

    public void HandleLightAttackAnim()
    {
        anim.SetTrigger("isUnarmedLightAttack");
    }

    public void CanRotate()
    {
        canRotate = true;
    }

    public void StopRotation()
    {
        canRotate = false;
    }
}
