using UnityEngine;

public class Blind : Enemy
{
    public bool isBlind = true;

    public override void serang()
    {
        if (isBlind)
        {
            Debug.Log("Enemy is blind and cannot attack, but it can still chase the player.");
        }
        else
        {
            base.serang();
        }
    }
}
