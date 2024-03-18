using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
public class UniversalInitializeOnLoadMethodAttribute : UnityEditor.InitializeOnLoadMethodAttribute
{
}
#else
public class UniversalInitializeOnLoadMethodAttribute : RuntimeInitializeOnLoadMethodAttribute
{
}
#endif
