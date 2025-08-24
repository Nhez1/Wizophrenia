using UnityEngine;

public class PlayerAnimations
{
    [Header("Animator")]
    private Animator animator;
    private readonly string xAxisName = "xAxis";
    private readonly string zAxisName = "zAxis";
    private readonly string animBoolName = "IsMoving";
    //private readonly string animAttackName = "OnAttack";

    public PlayerAnimations(Animator a)
    {
        animator = a;
    }

    public void CheckInputs(float xAxis, float zAxis)
    {
        animator.SetFloat(xAxisName, xAxis);
        animator.SetFloat(zAxisName, zAxis);
        animator.SetBool(animBoolName, IsMoving(xAxis, zAxis));

    }

    private bool IsMoving(float x, float z)
    {
        return x != 0 || z != 0;
    }
}
