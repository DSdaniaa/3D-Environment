using UnityEngine;

public class RunState : IPlayerState
{
    private PlayerController player;

    public RunState(PlayerController player)
    {
        this.player = player;
    }

    public void Enter()
    {
        player.Animator.Play("Running");
    }

    public void Update()
    {
    }

    public void Exit()
    {
    }
}