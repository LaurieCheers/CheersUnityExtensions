using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ShaderHashId
{
    string hashText;
    int hashValue;
    public ShaderHashId(string hashText)
    {
        this.hashText = hashText;
        this.hashValue = Shader.PropertyToID(hashText);
    }

    public static implicit operator int(ShaderHashId h) => h.hashValue;
    public static implicit operator ShaderHashId(string s) => new ShaderHashId(s);
}
