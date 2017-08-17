using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnimationStatus { stand, walk, attack, attack_wait, attack_alter, dead, critical, awakening, sleep, alter_attack_1, alter_attack_2, focus, stunned };
public enum GroupStatus { stand, attack };

public class AnimationController : MonoBehaviour {

    public AnimationStatus status;
    public Animator ComponentAnimator;
    public GroupStatus group_status;
    public bool isDead = false;

    void Start () {
        status = AnimationStatus.stand;
        ComponentAnimator = GetComponent<Animator>();
    }

    public void ChangeStatus(AnimationStatus animation)
    {
        if(isDead == false)
        {
            if (animation == AnimationStatus.attack || animation == AnimationStatus.attack_alter || animation == AnimationStatus.critical || animation == AnimationStatus.alter_attack_1 || animation == AnimationStatus.alter_attack_2)
                group_status = GroupStatus.attack;
            else group_status = GroupStatus.stand;

            if (animation == AnimationStatus.dead) isDead = true;

            status = animation;

            if (group_status == GroupStatus.attack) ComponentAnimator.speed = GetComponent<UnitOptions>().attack_speed;
            else ComponentAnimator.speed = 1f;
            if (animation == AnimationStatus.attack_wait) ComponentAnimator.speed = 1f;

            ComponentAnimator.Play(animation.ToString());
        }
    }
}
