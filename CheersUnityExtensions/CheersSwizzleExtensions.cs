using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SwizzleExtensions
{
    //====================
    //Vector2 to Vector2:
    //====================

    public static Vector2 XO(this Vector2 self) => new Vector2(self.x, 0);
    public static Vector2 OY(this Vector2 self) => new Vector2(0, self.y);
    public static Vector2 XnY(this Vector2 self) => new Vector2(self.x, -self.y);
    public static Vector2 nXY(this Vector2 self) => new Vector2(-self.x, self.y);
    public static Vector2 YX(this Vector2 self) => new Vector2(self.y, self.x);

    // rotates a vector2 90 degrees anticlockwise
    public static Vector2 nYX(this Vector2 self) => new Vector2(-self.y, self.x);

    // rotates a vector2 90 degrees clockwise
    public static Vector2 YnX(this Vector2 self) => new Vector2(self.y, -self.x);

    //====================
    //Vector2 to Vector3:
    //====================

    // different ways to extend a vector2 into three dimensions (unity does the first one implicitly, but explicit is always better)
    public static Vector3 XYO(this Vector2 self) => new Vector3(self.x, self.y, 0);
    public static Vector3 XOY(this Vector2 self) => new Vector3(self.x, 0, self.y);
    public static Vector3 OXY(this Vector2 self) => new Vector3(0, self.x, self.y);

    public static Vector3 YXO(this Vector2 self) => new Vector3(self.y, self.x, 0);
    public static Vector3 YOX(this Vector2 self) => new Vector3(self.x, 0, self.y);
    public static Vector3 OYX(this Vector2 self) => new Vector3(0, self.y, self.x);

    public static Vector3 nXYO(this Vector2 self) => new Vector3(-self.x, self.y, 0);
    public static Vector3 nXOY(this Vector2 self) => new Vector3(-self.x, 0, self.y);
    public static Vector3 OnXY(this Vector2 self) => new Vector3(0, -self.x, self.y);


    public static Vector3 YOnX(this Vector2 self) => new Vector3(-self.x, 0, self.y);
    public static Vector3 OYnX(this Vector2 self) => new Vector3(0, self.y, -self.x);

    //====================
    //Vector3 to Vector2:
    //====================

    public static Vector2 XY(this Vector3 self) => new Vector2(self.x, self.y);
    public static Vector2 XZ(this Vector3 self) => new Vector2(self.x, self.z);
    public static Vector2 YX(this Vector3 self) => new Vector2(self.y, self.x);
    public static Vector2 YZ(this Vector3 self) => new Vector2(self.y, self.z);
    public static Vector2 ZX(this Vector3 self) => new Vector2(self.z, self.x);
    public static Vector2 ZY(this Vector3 self) => new Vector2(self.z, self.y);

    //====================
    //Vector3 to Vector3:
    //====================
    // Each field can be either X,Y,Z, negative X,Y,Z, or 0, so there are 7*7*7 = 343 possible swizzle functions.
    // They'd be confusing to read and to use, so I'm not trying to be exhaustive here. But here are enough pieces to let you build whatever swizzle you want.

    // zero out components
    public static Vector3 OYZ(this Vector3 self) => new Vector3(0, self.y, self.z);
    public static Vector3 XOZ(this Vector3 self) => new Vector3(self.x, 0, self.z);
    public static Vector3 XYO(this Vector3 self) => new Vector3(self.x, self.y, 0);
    public static Vector3 XOO(this Vector3 self) => new Vector3(self.x, 0, 0);
    public static Vector3 OYO(this Vector3 self) => new Vector3(0, self.y, 0);
    public static Vector3 OOZ(this Vector3 self) => new Vector3(0, 0, self.z);

    // negate components
    public static Vector3 nXYZ(this Vector3 self) => new Vector3(-self.x, self.y, self.z);
    public static Vector3 XnYZ(this Vector3 self) => new Vector3(self.x, -self.y, self.z);
    public static Vector3 XYnZ(this Vector3 self) => new Vector3(self.x, self.y, -self.z);
    public static Vector3 XnYnZ(this Vector3 self) => new Vector3(self.x, -self.y, -self.z);
    public static Vector3 nXYnZ(this Vector3 self) => new Vector3(-self.x, self.y, -self.z);
    public static Vector3 nXnYZ(this Vector3 self) => new Vector3(-self.x, -self.y, self.z);

    // all reorderings
    public static Vector3 XZY(this Vector3 self) => new Vector3(self.x, self.z, self.y);
    public static Vector3 YXZ(this Vector3 self) => new Vector3(self.y, self.x, self.z);
    public static Vector3 YZX(this Vector3 self) => new Vector3(self.y, self.z, self.x);
    public static Vector3 ZYX(this Vector3 self) => new Vector3(self.z, self.y, self.x);
    public static Vector3 ZXY(this Vector3 self) => new Vector3(self.z, self.x, self.y);

    //====================
    //Vector2Int to Vector3Int:
    //====================

    public static Vector3Int XYO(this Vector2Int self) => new Vector3Int(self.x, self.y, 0);
    public static Vector3Int XOY(this Vector2Int self) => new Vector3Int(self.x, 0, self.y);
    public static Vector3Int OXY(this Vector2Int self) => new Vector3Int(0, self.x, self.y);
}