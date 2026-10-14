using UnityEngine;

public class JumpState : IPlayerState
{
    private PlayerController player;

    public JumpState(PlayerController player)
    {
        this.player = player;
    }

    public void Enter()
    {
        player.Animator.Play("Jumping");
    }

    public void Update()
    {
    }

    public void Exit()
    {
    }
}