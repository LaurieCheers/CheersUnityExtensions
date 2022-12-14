using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class OnEnterPlay_BaseAttribute : System.Attribute
{
    public virtual void OnEnterPlay(FieldInfo field)
    {
    }

    public virtual void OnEnterPlay(MethodInfo method)
    {
    }
}

public class OnEnterPlay_SetNull : OnEnterPlay_BaseAttribute
{
    public override void OnEnterPlay(FieldInfo field) => field.SetValue(null, null);
}

public class OnEnterPlay_Set : OnEnterPlay_BaseAttribute
{
    object value;

    public OnEnterPlay_Set(object value) => this.value = value;

    public override void OnEnterPlay(FieldInfo field) => field.SetValue(null, value);
}

public class OnEnterPlay_SetNew : OnEnterPlay_BaseAttribute
{
    public override void OnEnterPlay(FieldInfo field)
    {
        ConstructorInfo ctor = field.FieldType.GetConstructor(new System.Type[0]);
        object instance = ctor.Invoke(new object[0]);
        field.SetValue(null, instance);
    }
}

public class OnEnterPlay_Clear : OnEnterPlay_BaseAttribute
{
    public override void OnEnterPlay(FieldInfo field)
    {
        MethodInfo method = field.FieldType.GetMethod("Clear");
        method.Invoke(field.GetValue(null), new object[0]);
    }
}

public class OnEnterPlay_Run : OnEnterPlay_BaseAttribute
{
    public override void OnEnterPlay(MethodInfo method) => method.Invoke(null, new object[0]);
}

// For situations (e.g. ScriptableObjects that have extra non-serialized state) where you can't
// make a static function that reinitializes your object:
// Store an EnterPlayID somewhere when you initialize your object, and check its IsCurrent
// property to decide whether to reinitialize the object.
public struct EnterPlayID
{
    static int currentID = 1;

    [OnEnterPlay_Run]
    static void NextID()
    {
        currentID++;
    }

    int id;
    public static EnterPlayID GetCurrent() => new EnterPlayID { id = currentID };

    public bool IsCurrent => id == currentID;
}