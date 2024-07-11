using System;
using UnityEngine;

public static class TriggerUtility
{
    public static void ExecuteTrigger(Transform transform, string triggerName)
    {
        var animator = transform.GetComponent<Animator>();
        animator.SetTrigger(triggerName);
    }
}