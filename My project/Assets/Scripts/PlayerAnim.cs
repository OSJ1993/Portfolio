using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    Animator _anim;
    // Start is called before the first frame update
    void Start()
    {
        _anim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        bool isForward = Input.GetKey(KeyCode.W);
        _anim.SetBool("isForward", isForward);
        if (isForward)
        {
            _anim.SetBool("isForward", true);
        }
        else
        {
            _anim.SetBool("isForward", false);
        }
        bool isBackward = Input.GetKey(KeyCode.S);
        _anim.SetBool("isBackward", isBackward);
        if (isBackward)
        {
            _anim.SetBool("isBackward", true);
        }
        else
        {
            _anim.SetBool("isBackward", false);
         }

    }
}
