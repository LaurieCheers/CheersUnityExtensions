using UnityEngine;

public static class CheersMath
{
    //=========================================
    // Misc non-extension utility functions
    //=========================================

    // Given a "forward" vector, chooses some arbitrary "right" and "up" vectors and returns them.
    // There's no way for such a function to be smooth across all directions, so it's stabilized by passing in the previously generated right vector.
    // This can be useful for getting basis vectors e.g. to generate a mesh
    public static void GetStablePerpendiculars(Vector3 forward, ref Vector3 right, out Vector3 up)
    {
        up = Vector3.Cross(forward, right).normalized;
        right = Vector3.Cross(forward, up).normalized;
    }

    /*public static Vector2? GetIntersection(Vector2 line1Start, Vector2 line1End, Vector2 line2Start, Vector2 line2End)
    {
    }*/

    // convert to/from FLU (X=Forward, Y=Left, Z=Up) coordinate space (Source Engine, ROS)
    public static Vector3 ToFLU(Vector3 self) => new Vector3(self.z, -self.x, self.y);
    public static Vector3 FromFLU(Vector3 self) => new Vector3(-self.y, self.z, self.x);

    // convert to/from RFU (X=Right, Y=Forward, Z=Up) coordinate space (Blender, Max)
    public static Vector3 ToRFU(Vector3 self) => new Vector3(self.x, self.z, self.y);
    public static Vector3 FromRFU(Vector3 self) => new Vector3(self.x, self.z, self.y);

    // convert to/from RUB (X=Right, Y=Up, Z=Back) coordinate space (OpenGL, Maya)
    public static Vector3 ToRUB(Vector3 self) => new Vector3(self.x, self.y, -self.z);
    public static Vector3 FromRUB(Vector3 self) => new Vector3(self.x, self.y, -self.z);

    // A cosine curve rescaled to 0 when t=0, and peaking at 1 when t=1
    public static float CosBlend(float t) => 0.5f - Mathf.Cos(t * Mathf.PI * 0.5f) * 0.5f;

    // Interpolate from "a" when t = 0 to "b" when t = 1, with a smooth ease-in and out via a cosine curve
    public static float CosLerp(float a, float b, float t) => Mathf.Lerp(a, b, CosBlend(t));
    public static Vector3 CosLerp(Vector3 a, Vector3 b, float t) => Vector3.Lerp(a, b, CosBlend(t));
    public static Quaternion CosLerp(Quaternion a, Quaternion b, float t) => Quaternion.Lerp(a, b, CosBlend(t));
    public static Quaternion CosSlerp(Quaternion a, Quaternion b, float t) => Quaternion.Slerp(a, b, CosBlend(t));

    public static Vector2 ToVector2(this Vector2Int self) => new Vector2(self.x, self.y);
    public static Vector3 ToVector3(this Vector3Int self) => new Vector3(self.x, self.y, self.z);

    //=========================================
    // Vector3 extension methods
    //=========================================

    public static Vector3 GetClosestPointOnLine(this Vector3 point, Vector3 lineStart, Vector3 lineEnd)
    {
        if (lineStart == lineEnd)
            return lineStart;

        Vector3 lineOffset = lineEnd - lineStart;
        float lineLength = lineOffset.magnitude;
        Vector3 lineDirection = lineOffset / lineLength;
        float distanceAlongLine = Vector3.Dot(point - lineStart, lineDirection).Clamp(0, lineLength);
        return lineStart + distanceAlongLine * lineDirection;
    }

    // The "Plane Origin" can just be any point on the plane
    //NB this won't work unless the plane normal vector is of length 1
    public static Vector3 GetClosestPointOnPlane(this Vector3 point, Vector3 planeOrigin, Vector3 planeNormalMustBeLength1)
    {
        float distFromPlane = Vector3.Dot(planeNormalMustBeLength1, point - planeOrigin);
        return point - planeNormalMustBeLength1 * distFromPlane;
    }

    public static bool IsAbovePlane(this Vector3 point, Vector3 planeOrigin, Vector3 planeUp) => Vector3.Dot(point - planeOrigin, planeUp) > 0;

    public static float InverseLerp(this Vector3 point, Vector3 from, Vector3 to)
    {
        Vector3 offset = (to - from);
        float distance = offset.magnitude;
        Vector3 dir = offset / distance;
        return Vector3.Dot(point - from, dir) / distance;
    }

    // LookFixAB is a rotation that turns the A axis to face old Z, and the B axis to face old Y. (nX is my notation for negative X).
    public static Quaternion LookFix_ZY = Quaternion.identity; // for completeness
    public static Quaternion LookFix_XY = Quaternion.Euler(0, -90, 0);
    public static Quaternion LookFix_XZ = Quaternion.AngleAxis(-120, Vector3.one);
    public static Quaternion LookFix_YX = Quaternion.AngleAxis(120, Vector3.one);
    public static Quaternion LookFix_YZ = Quaternion.AngleAxis(180, new Vector3(0, 1, 1));
    public static Quaternion LookFix_ZX = Quaternion.Euler(0, 0, 90);
    public static Quaternion LookFix_nXY = Quaternion.Euler(0, 90, 0);
    public static Quaternion LookFix_nXZ = Quaternion.AngleAxis(120, new Vector3(-1, 1, 1));
    public static Quaternion LookFix_nYX = Quaternion.AngleAxis(120, new Vector3(-1, -1, 1));
    public static Quaternion LookFix_nYZ = Quaternion.Euler(-90, 0, 0);
    public static Quaternion LookFix_nZX = Quaternion.AngleAxis(180, new Vector3(1, 1, 0));
    public static Quaternion LookFix_nZY = Quaternion.Euler(0, 180, 0);
    public static Quaternion LookFix_XnY = Quaternion.AngleAxis(180, new Vector3(1, 0, 1));
    public static Quaternion LookFix_XnZ = Quaternion.AngleAxis(120, new Vector3(1, -1, 1));
    public static Quaternion LookFix_YnX = Quaternion.AngleAxis(120, new Vector3(1, -1, -1));
    public static Quaternion LookFix_YnZ = Quaternion.Euler(90, 0, 0);
    public static Quaternion LookFix_ZnX = Quaternion.Euler(0, 0, -90);
    public static Quaternion LookFix_ZnY = Quaternion.Euler(0, 0, 180);
    public static Quaternion LookFix_nXnY = Quaternion.AngleAxis(180, new Vector3(1, 0, -1));
    public static Quaternion LookFix_nXnZ = Quaternion.AngleAxis(120, new Vector3(1, 1, -1));
    public static Quaternion LookFix_nYnX = Quaternion.AngleAxis(120, new Vector3(-1, 1, -1));
    public static Quaternion LookFix_nYnZ = Quaternion.AngleAxis(180, new Vector3(0, 1, -1));
    public static Quaternion LookFix_nZnX = Quaternion.AngleAxis(180, new Vector3(-1, 1, 0));
    public static Quaternion LookFix_nZnY = Quaternion.Euler(180, 0, 0);

    public static Quaternion ToLookRotation(this Vector3 direction) => Quaternion.LookRotation(direction);
    public static Quaternion ToLookRotation(this Vector3 direction, Vector3 up) => Quaternion.LookRotation(direction, up);
    
    // In these look rotations, the first named axis (e.g. Z in LookRotationZY) will point exactly along the "this" vector.
    // The second axis (e.g. Y in LookRotationZY) points towards the second vector as closely as possible.
    public static Quaternion ToLookRotation_XY(this Vector3 direction, Vector3 up) => Quaternion.LookRotation(direction, up) * LookFix_XY;
    public static Quaternion ToLookRotation_XZ(this Vector3 direction, Vector3 forward) => Quaternion.LookRotation(direction, forward) * LookFix_XZ;
    public static Quaternion ToLookRotation_YX(this Vector3 direction, Vector3 right) => Quaternion.LookRotation(direction, right) * LookFix_YX;
    public static Quaternion ToLookRotation_YZ(this Vector3 direction, Vector3 forward) => Quaternion.LookRotation(direction, forward) * LookFix_YZ;
    public static Quaternion ToLookRotation_ZX(this Vector3 direction, Vector3 right) => Quaternion.LookRotation(direction, right) * LookFix_ZX;
    public static Quaternion ToLookRotation_ZY(this Vector3 direction, Vector3 up) => Quaternion.LookRotation(direction, up); // it's just a regular LookRotation
    public static Quaternion ToLookRotation_nXY(this Vector3 direction, Vector3 up) => Quaternion.LookRotation(direction, up) * LookFix_nXY;
    public static Quaternion ToLookRotation_nXZ(this Vector3 direction, Vector3 forward) => Quaternion.LookRotation(direction, forward) * LookFix_nXZ;
    public static Quaternion ToLookRotation_nYX(this Vector3 direction, Vector3 right) => Quaternion.LookRotation(direction, right) * LookFix_nYX;
    public static Quaternion ToLookRotation_nYZ(this Vector3 direction, Vector3 forward) => Quaternion.LookRotation(direction, forward) * LookFix_nYZ;
    public static Quaternion ToLookRotation_nZX(this Vector3 direction, Vector3 right) => Quaternion.LookRotation(direction, right) * LookFix_nZX;
    public static Quaternion ToLookRotation_nZY(this Vector3 direction, Vector3 up) => Quaternion.LookRotation(direction, up) * LookFix_nZY;

    // hey look, all the "negative axis" direction words are 4 letters. And not one of the "positive axis" directions are.
    public static Quaternion ToLookRotation_XnY(this Vector3 direction, Vector3 down) => Quaternion.LookRotation(direction, down) * LookFix_XnY;
    public static Quaternion ToLookRotation_XnZ(this Vector3 direction, Vector3 back) => Quaternion.LookRotation(direction, back) * LookFix_XnZ;
    public static Quaternion ToLookRotation_YnX(this Vector3 direction, Vector3 left) => Quaternion.LookRotation(direction, left) * LookFix_YnX;
    public static Quaternion ToLookRotation_YnZ(this Vector3 direction, Vector3 back) => Quaternion.LookRotation(direction, back) * LookFix_YnZ;
    public static Quaternion ToLookRotation_ZnX(this Vector3 direction, Vector3 left) => Quaternion.LookRotation(direction, left) * LookFix_ZnX;
    public static Quaternion ToLookRotation_ZnY(this Vector3 direction, Vector3 down) => Quaternion.LookRotation(direction, down) * LookFix_ZnY;
    public static Quaternion ToLookRotation_nXnY(this Vector3 direction, Vector3 down) => Quaternion.LookRotation(direction, down) * LookFix_nXnY;
    public static Quaternion ToLookRotation_nXnZ(this Vector3 direction, Vector3 back) => Quaternion.LookRotation(direction, back) * LookFix_nXnZ;
    public static Quaternion ToLookRotation_nYnX(this Vector3 direction, Vector3 left) => Quaternion.LookRotation(direction, left) * LookFix_nYnX;
    public static Quaternion ToLookRotation_nYnZ(this Vector3 direction, Vector3 back) => Quaternion.LookRotation(direction, back) * LookFix_nYnZ;
    public static Quaternion ToLookRotation_nZnX(this Vector3 direction, Vector3 left) => Quaternion.LookRotation(direction, left) * LookFix_nZnX;
    public static Quaternion ToLookRotation_nZnY(this Vector3 direction, Vector3 down) => Quaternion.LookRotation(direction, down) * LookFix_nZnY;

    //=========================================
    // Vector2 extension methods
    //=========================================

    public static bool IsMainlyHorizontal(this Vector2 self) => Mathf.Abs(self.x) > Mathf.Abs(self.y);

    // cheap rotate 90 degrees
    public static Vector2 Rot90(this Vector2 self) => new Vector3(self.y, -self.x);
    public static Vector2 RotNeg90(this Vector2 self) => new Vector3(-self.y, self.x);

    // cheap rotate 90 degrees around x/y/z
    public static Vector3 Pitch90(this Vector3 self) => new Vector3(self.x, -self.z, self.y);
    public static Vector3 PitchNeg90(this Vector3 self) => new Vector3(self.x, self.z, -self.y);
    public static Vector3 Yaw90(this Vector3 self) => new Vector3(-self.z, self.y, self.x);
    public static Vector3 YawNeg90(this Vector3 self) => new Vector3(self.z, self.y, -self.x);
    public static Vector3 Roll90(this Vector3 self) => new Vector3(-self.y, self.x, self.z);
    public static Vector3 RollNeg90(this Vector3 self) => new Vector3(self.y, -self.x, self.z);

    // These are mainly convenience functions to avoid the cost of the square root in Vector3.magnitude
    public static bool IsShorterThan(this Vector3 self, Vector3 other) => self.sqrMagnitude < other.sqrMagnitude;
    public static bool IsShorterThan(this Vector3 self, float distance) => self.sqrMagnitude < distance * distance;
    public static bool IsShorterThan(this Vector2 self, Vector2 other) => self.sqrMagnitude < other.sqrMagnitude;
    public static bool IsShorterThan(this Vector2 self, float distance) => self.sqrMagnitude < distance * distance;
    public static bool IsLongerThan(this Vector3 self, Vector3 other) => self.sqrMagnitude > other.sqrMagnitude;
    public static bool IsLongerThan(this Vector3 self, float distance) => self.sqrMagnitude > distance * distance;
    public static bool IsLongerThan(this Vector2 self, Vector2 other) => self.sqrMagnitude > other.sqrMagnitude;
    public static bool IsLongerThan(this Vector2 self, float distance) => self.sqrMagnitude > distance * distance;

    public static bool IsInRange(this Vector3 self, float minRange, float maxRange)
    {
        float sqrMagnitude = self.sqrMagnitude;
        return sqrMagnitude > minRange * minRange && sqrMagnitude < maxRange * maxRange;
    }

    //=========================================
    // Ray extension methods
    //=========================================

    public static Vector3 GetClosestPoint(this Ray ray, Vector3 target)
    {
        float distance = Vector3.Dot(target - ray.origin, ray.direction);
        return ray.origin + ray.direction * distance;
    }

    public static float GetClosestDistSqr(this Ray ray, Vector3 target) => (GetClosestPoint(ray, target) - target).sqrMagnitude;
    public static float GetClosestDistance(this Ray ray, Vector3 target) => (GetClosestPoint(ray, target) - target).magnitude;

    // The "Plane Origin" can just be any point on the plane
    //NB this assumes the plane normal is length 1
    public static Vector3 ProjectToPlane(this Ray ray, Vector3 planeOrigin, Vector3 planeNormalMustBeLength1)
    {
        Vector3 offset = planeOrigin - ray.origin;
        float distFromPlane = Vector3.Dot(planeNormalMustBeLength1, offset);
        float directionScaler = Vector3.Dot(planeNormalMustBeLength1, ray.direction);
        return ray.origin + ray.direction * distFromPlane / directionScaler;
    }

    public static Vector3 ProjectToLine(this Ray ray, Vector3 lineStart, Vector3 lineEnd)
    {
        Vector3 lineDirection = (lineEnd - lineStart).normalized;
        Vector3 lineCross = Vector3.Cross(ray.direction, lineDirection);
        Vector3 planeNormal = Vector3.Cross(lineCross, lineDirection).normalized;
        return ray.ProjectToPlane(lineStart, planeNormal);
    }

    //=========================================
    // Quaternion extension methods
    //=========================================

    public static Quaternion ToLocal(this Quaternion worldRotation, Quaternion parentWorldRotation) => Quaternion.Inverse(parentWorldRotation) * worldRotation;
    public static Quaternion ToWorld(this Quaternion localRotation, Quaternion parentWorldRotation) => parentWorldRotation * localRotation;

    //=========================================
    // float extension methods
    //=========================================

    public static float Clamp(this float f, float min, float max) => Mathf.Clamp(f, min, max);
    public static bool IsInRange(this float self, float min, float max) => self >= min && self <= max;
    public static float Deg2Rad(this float f) => Mathf.Deg2Rad * f;
    public static float Rad2Deg(this float f) => Mathf.Rad2Deg * f;
}
