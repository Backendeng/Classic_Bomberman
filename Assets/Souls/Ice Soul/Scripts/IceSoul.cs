using UnityEngine;

public class IceSoul : Soul
{
    protected override void Update()
    {
        base.Update();

        if (HasUsedAbility)
        {
            Destroy(gameObject);
        }
        else if (ShouldStartUsingAbility)
        {
            ShouldStartUsingAbility = false;
            StartUsingAbility();
        }
        else if (IsUsingAbility)
        {
            IsUsingAbility = false;
            ShouldStartUsingAbility = true;
            MyAnimator.SetBool("ability", true);
        }
    }

    protected override Vector2 GetSoulPosition(int range) => (Vector2)transform.position + range * AbilityDirection * transform.right;

    protected override Quaternion GetSoulRotation() => Quaternion.Euler(0, transform.right.x == 1 ? 0 : 180, 0);

    protected override string GetAnimationBoolName(int range) => "create";
}
