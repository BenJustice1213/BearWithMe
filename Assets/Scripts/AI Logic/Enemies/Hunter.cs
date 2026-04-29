using System.Collections;
using UnityEngine;

public class Hunter : AIEnemy
{
    protected override IEnumerator PerformAction()
    {
        yield return new WaitForSeconds(0.5f);
        base.PerformAction();
    }
}
