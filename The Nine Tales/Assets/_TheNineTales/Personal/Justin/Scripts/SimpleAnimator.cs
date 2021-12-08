using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAnimator : MonoBehaviour
{
    private Animation an;

    public enum PlayerAnimation { Idle, Run, Jump };

    // Start is called before the first frame update
    void Start()
    {
        an = GetComponent<Animation>();    
    }

    private void LateUpdate()
    {
        Player p = Player.player.GetComponent<Player>();
        if(Mathf.Abs(p.m_PlayerVelocity.x) > 0 && Mathf.Abs(p.m_PlayerVelocity.y) < 0.1f)
        {
            PlayAnimation(PlayerAnimation.Run);
        } else {

        }
    }

    public void PlayAnimation(PlayerAnimation anim)
    {
        switch (anim)
        {
            case PlayerAnimation.Idle:
                an.CrossFade("T_an_FoxIdle");
                break;
            case PlayerAnimation.Run:
                an.CrossFade("T_an_FoxRun");
                break;
            case PlayerAnimation.Jump:
                an.CrossFade("T_an_FoxRun");
                break;
        }
    }
}
