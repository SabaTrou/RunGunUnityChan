using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// �ꊇ�ŊǗ����邽��
/// </summary>
public interface IBaseCollisionData2D
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="other"></param>
    /// <param name="hitPointA">���\�b�h�g���Ă�ق��̐ڐG�_</param>
    /// <param name="hitPointB">�Ώۂ̐ڐG�_</param>
    /// <returns></returns>
    public bool CheckCollision(IBaseCollisionData2D other,out Vector2 hitPointA,out Vector2 hitPointB);
    public bool CheckCollisionWithCapsule(CapsuleData2D capsule, out Vector2 hitPointA, out Vector2 hitPointB);
    public bool CheckCollisionWithCircle(CircleData2D circle, out Vector2 hitPointA, out Vector2 hitPointB);
    public bool CheckCollisionWithLine(LineData2D line, out Vector2 hitPointA, out Vector2 hitPointB);
    public bool CheckCollisionWithBox(BoxData2D box, out Vector2 hitPointA, out Vector2 hitPointB);
}

public class CapsuleData2D:IBaseCollisionData2D
{
    public Vector2 endPoint;
    public Vector2 originPoint;
    public float radius;
    private LineData2D lineData;
    public CapsuleData2D()
    {
        lineData = new LineData2D(originPoint,endPoint);
    }
    public CapsuleData2D(Vector2 origin, Vector2 end, float radius)
    {
        this.endPoint = end;
        this.originPoint = origin;
        this.radius = radius;
    }
    public void SetData(Vector2 origin, Vector2 end, float radius)
    {
        this.endPoint = end;
        this.originPoint = origin;
        this.radius = radius;
    }
    public LineData2D ToLine()
    {
        lineData.SetData(originPoint, endPoint);
        return lineData;
    }
    public bool CheckCollision(IBaseCollisionData2D other,out Vector2 hitPointA,out Vector2 hitPointB)
    {
        return other.CheckCollisionWithCapsule(this, out hitPointA,out hitPointB);
    }
    public bool CheckCollisionWithCapsule(CapsuleData2D capsule, out Vector2 hitPointA, out Vector2 hitPointB)
    {
        return capsule.CheckCollision(this, out hitPointA, out hitPointB);
    }
    public bool CheckCollisionWithCircle(CircleData2D circle, out Vector2 hitPointA, out Vector2 hitPointB)
    {
        return this.CheckCollision2D(circle, out hitPointA, out hitPointB);
    }
    public bool CheckCollisionWithLine(LineData2D line, out Vector2 hitPointA, out Vector2 hitPointB)
    {
        return this.CheckCollision2D(line, out hitPointA, out hitPointB);
    }
    public bool CheckCollisionWithBox(BoxData2D box,out Vector2 hitPointA, out Vector2 hitPointB)
    {
        return this.CheckCollision2D(box, out hitPointA, out hitPointB);
    }
}
public class CircleData2D:IBaseCollisionData2D
{
    public Vector2 position;
    public float radius;
    public CircleData2D(Vector2 position, float radius)
    {
        this.position = position;
        this.radius = radius;
    }
    public void SetData(Vector2 position,float radius)
    {
        this.position = position;
        this.radius = radius;
    }
    public bool CheckCollision(IBaseCollisionData2D other, out Vector2 hitPointA, out Vector2 hitPointB)
    {
        return other.CheckCollisionWithCircle(this, out hitPointA, out hitPointB);
    }
    public bool CheckCollisionWithCapsule(CapsuleData2D capsule, out Vector2 hitPointA, out Vector2 hitPointB)
    {
        return capsule.CheckCollision(this, out hitPointA, out hitPointB);
    }
    public bool CheckCollisionWithCircle(CircleData2D circle, out Vector2 hitPointA, out Vector2 hitPointB)
    {
        return this.CheckCollision2D(circle, out hitPointA, out hitPointB);
    }
    public bool CheckCollisionWithLine(LineData2D line, out Vector2 hitPointA, out Vector2 hitPointB)
    {
        return this.CheckCollision2D(line, out hitPointA, out hitPointB);
    }
    public bool CheckCollisionWithBox(BoxData2D box, out Vector2 hitPointA, out Vector2 hitPointB)
    {
        return this.CheckCollision2D(box, out hitPointA, out hitPointB);
    }
}
public class LineData2D:IBaseCollisionData2D
{
    public Vector2 endPoint;
    public Vector2 originPoint;
    public LineData2D(Vector2 origin, Vector2 end)
    {
        this.endPoint = end;
        this.originPoint = origin;
    }
    public void SetData(Vector2 origin, Vector2 end)
    {
        this.endPoint = end;
        this.originPoint = origin;
    }
    public bool CheckCollision(IBaseCollisionData2D other, out Vector2 hitPointA, out Vector2 hitPointB)
    {
        return other.CheckCollisionWithLine(this, out hitPointA, out hitPointB);
    }
    public bool CheckCollisionWithCapsule(CapsuleData2D capsule, out Vector2 hitPointA, out Vector2 hitPointB)
    {
        return capsule.CheckCollision(this, out hitPointA, out hitPointB);
    }
    public bool CheckCollisionWithCircle(CircleData2D circle, out Vector2 hitPointA, out Vector2 hitPointB)
    {
        return this.CheckCollision2D(circle, out hitPointA, out hitPointB);
    }
    public bool CheckCollisionWithLine(LineData2D line, out Vector2 hitPointA, out Vector2 hitPointB)
    {
        return this.CheckCollision2D(line, out hitPointA, out hitPointB);
    }
    public bool CheckCollisionWithBox(BoxData2D box, out Vector2 hitPointA, out Vector2 hitPointB)
    {
        return this.CheckCollision2D(box, out hitPointA, out hitPointB);
    }

}
public class BoxData2D : IBaseCollisionData2D
{
    public Vector2 originPoint; // �{�b�N�X�̊J�n�_
    public Vector2 endPoint;    // �{�b�N�X�̏I���_
    public Vector2 boxCenter;   // �{�b�N�X�̒��S�_
    public Vector2 forward;     // ���S���̕����x�N�g��
    public float boxWidth;      // �{�b�N�X�̕�

    private Vector2[] vertices = new Vector2[4]; // �{�b�N�X�̒��_
    private Vector2[][] edges = new Vector2[4][]; // �{�b�N�X�̃G�b�W

    private LineData2D lineData;

    // �R���X�g���N�^
    public BoxData2D(Vector2 originPoint, Vector2 endPoint, float boxWidth)
    {
        this.originPoint = originPoint;
        this.endPoint = endPoint;
        this.boxWidth = boxWidth;

        // �K�v�ȃf�[�^���X�V
        this.boxCenter = (originPoint + endPoint) / 2;
        this.forward = (endPoint - originPoint).normalized;
        // ���_�ƃG�b�W��������
        CalculateVerticesAndEdges();
        lineData = new LineData2D(originPoint, endPoint);
    }

    // �f�[�^���Z�b�g���郁�\�b�h
    public void SetData(Vector2 originPoint, Vector2 endPoint, float boxWidth)
    {
        this.originPoint = originPoint;
        this.endPoint = endPoint;
        this.boxWidth = boxWidth;

        // �K�v�ȃf�[�^���X�V
        this.boxCenter = (originPoint + endPoint) / 2;
        this.forward = (endPoint - originPoint).normalized;

        // ���_�ƃG�b�W���Čv�Z
        CalculateVerticesAndEdges();
        
    }
    // �{�b�N�X�̒��_�ƃG�b�W���v�Z
    private void CalculateVerticesAndEdges()
    {
        // ���S���̐��������̃x�N�g�����v�Z
        Vector2 perpendicular = new Vector2(-forward.y, forward.x) * (boxWidth / 2);

        // �{�b�N�X��4�̒��_���v�Z
        vertices[0] = originPoint - perpendicular; // ����
        vertices[1] = endPoint - perpendicular;   // �E��
        vertices[2] = endPoint + perpendicular;   // �E��
        vertices[3] = originPoint + perpendicular; // ����

        // �{�b�N�X�̃G�b�W���\��
        edges[0] = new Vector2[] { vertices[0], vertices[1] }; // ����
        edges[1] = new Vector2[] { vertices[1], vertices[2] }; // �E��
        edges[2] = new Vector2[] { vertices[2], vertices[3] }; // ���
        edges[3] = new Vector2[] { vertices[3], vertices[0] }; // ����
    }
    // LineData2D �ɕϊ�
    public LineData2D ToLine()
    {
        lineData.SetData(originPoint, endPoint);
        return lineData;
    }

    public bool CheckCollision(IBaseCollisionData2D other, out Vector2 hitPointA, out Vector2 hitPointB)
    {
        return other.CheckCollisionWithBox(this, out hitPointA, out hitPointB);
    }
    public bool CheckCollisionWithCapsule(CapsuleData2D capsule, out Vector2 hitPointA, out Vector2 hitPointB)
    {
        return capsule.CheckCollision(this, out hitPointA, out hitPointB);
    }
    public bool CheckCollisionWithCircle(CircleData2D circle, out Vector2 hitPointA, out Vector2 hitPointB)
    {
        return this.CheckCollision2D(circle, out hitPointA, out hitPointB);
    }
    public bool CheckCollisionWithLine(LineData2D line, out Vector2 hitPointA, out Vector2 hitPointB)
    {
        return this.CheckCollision2D(line, out hitPointA, out hitPointB);
    }
    public bool CheckCollisionWithBox(BoxData2D box, out Vector2 hitPointA, out Vector2 hitPointB)
    {
        return this.CheckCollision2D(box, out hitPointA, out hitPointB);
    }



    // �{�b�N�X�̑S���_���擾
    public Vector2[] GetVertices()
    {
        return vertices;
    }

    // �{�b�N�X�̑S�G�b�W���擾
    public Vector2[][] GetEdges()
    {
        return edges;
    }
}
public struct CollisionData2D
{
    public readonly Vector2 hitPoint;
    public readonly ICollisionable2D collisionable;
    public readonly Vector2 depth;
    public CollisionData2D(Vector2 hitpoint,ICollisionable2D collisionable,Vector2 depth)
    {
        this.hitPoint = hitpoint;
        this.collisionable = collisionable;
        this.depth = depth;
    }


}



