using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class Turret : MonoBehaviour
{

    public Transform moveSegment;
    public float Speed = 30f;
    public float Limit = 90f;
    public Vector3 target;

    private Quaternion yawSegmentStartRotation;

    public virtual void Start()
    {
        this.yawSegmentStartRotation = this.moveSegment.localRotation;
    }

    public virtual void Update()
    {
        float angle = 0.0f;
        Vector3 targetRelative = default(Vector3);
        Quaternion targetRotation = default(Quaternion);

        if (this.moveSegment && (this.Limit != 0f))
        {
            targetRelative = this.moveSegment.InverseTransformPoint(this.target);
            angle = Mathf.Atan2(targetRelative.x, targetRelative.z) * Mathf.Rad2Deg;
            if (angle >= 180f) angle = 180f - angle;

            if (angle <= -180f) angle = -180f + angle;
            targetRotation = this.moveSegment.rotation * Quaternion.Euler(0f, Mathf.Clamp(angle, -this.Speed * Time.deltaTime, this.Speed * Time.deltaTime), 0f);

            if ((this.Limit < 360f) && (this.Limit > 0f)) this.moveSegment.rotation = Quaternion.RotateTowards(this.moveSegment.parent.rotation * this.yawSegmentStartRotation, targetRotation, this.Limit);
            else this.moveSegment.rotation = targetRotation;
        }
    }

    public virtual void Target(Vector3 target)
    {
        this.target = target;
    }

}