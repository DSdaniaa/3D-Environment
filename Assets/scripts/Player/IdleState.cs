using UnityEngine;

public class IdleState : IPlayerState
{
    private PlayerController player;

    public IdleState(PlayerController player)
    {
        this.player = player;
    }

    public void Enter()
    {
        player.Animator.Play("Idle");
    }

    public void Update()
    {
    }

    public void Exit()
    {
    }
}