using UnityEngine;

public class WalkState : IPlayerState
{
    private PlayerController player;

    public WalkState(PlayerController player)
    {
        this.player = player;
    }

    public void Enter()
    {
        player.Animator.Play("Walking");
    }

    public void Update()
    {
    }

    public void Exit()
    {
    }
}