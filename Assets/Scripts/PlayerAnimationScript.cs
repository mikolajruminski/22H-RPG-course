using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationScript : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private Player player;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        player = player.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("movementX", player.ReturnPlayerRB().velocity.x);
        animator.SetFloat("movementY", player.ReturnPlayerRB().velocity.y);

        if (player.ReturnPlayerRB().velocity.y != 0)
        {
            animator.SetFloat("lastY", player.ReturnPlayerRB().velocity.y);
            animator.SetFloat("lastX", 0);
        }
        if (player.ReturnPlayerRB().velocity.x != 0)
        {
            animator.SetFloat("lastX", player.ReturnPlayerRB().velocity.x);
            animator.SetFloat("lastY", 0);
        }

    }
}
