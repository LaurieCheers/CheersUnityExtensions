using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct AnimHashId
{
    string hashText;
    int hashValue;
    public AnimHashId(string hashText)
    {
        this.hashText = hashText;
        this.hashValue = Animator.StringToHash(hashText);// Shader.PropertyToID(hashText);
    }

    public static implicit operator int(AnimHashId h) => h.hashValue;
}
