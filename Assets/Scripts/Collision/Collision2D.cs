using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;


public static class CapsuleCollision2D
{
    #region �v�Z
    private static Vector2 GetNearestPoint2D(Vector2 origin, Vector2 end, Vector2 position)
    {
        //�J�v�Z���̎n�_����I�_�̃x�N�g���𐳋K����������
        Vector2 vector1 = (end - origin).normalized;
        //�n�_����_�ւ̃x�N�g��
        Vector2 originToPoint = position - origin;
        //�I�_����_�ւ̃x�N�g��
        Vector2 endToPoint = position - end;

        if (0 > Vector2.Dot(vector1, originToPoint))
        {

            return origin;
        }
        else if (0 < Vector2.Dot(vector1, endToPoint))
        {
            return end;
        }

        Vector2 point = origin + vector1 * Vector2.Dot(vector1, originToPoint);

        return point;
    }
    private static Vector2 GetNearestPoint2D(LineData2D lineData1, LineData2D lineData2)
    {
        Vector2 originNearPoint = GetNearestPoint2D(lineData1.originPoint, lineData1.endPoint, lineData2.originPoint);
        float originDistance = GetDistance2D(originNearPoint, lineData2.originPoint);
        Vector2 endNearPoint = GetNearestPoint2D(lineData1.originPoint, lineData1.endPoint, lineData2.endPoint);
        float endDistance = GetDistance2D(endNearPoint, lineData2.endPoint);
        if (originDistance <= endDistance)
        {
            return originNearPoint;
        }
        return endNearPoint;
    }


    private static float GetDistance2D(Vector2 point1, Vector2 point2)
    {

        Vector2 distanceVec = point1 - point2;
        float distance = Mathf.Sqrt(
            Mathf.Pow(Mathf.Abs(distanceVec.x), 2) +
            Mathf.Pow(Mathf.Abs(distanceVec.y), 2)
            );
        return distance;
    }
    private static float GetLinesDistance2D(LineData2D lineData1, LineData2D lineData2)
    {
        Vector2 originNearPoint = GetNearestPoint2D(lineData1.originPoint, lineData1.endPoint, lineData2.originPoint);
        float originDistance = GetDistance2D(originNearPoint, lineData2.originPoint);
        Vector2 endNearPoint = GetNearestPoint2D(lineData1.originPoint, lineData1.endPoint, lineData2.endPoint);
        float endDistance = GetDistance2D(endNearPoint, lineData2.endPoint);
        if (originDistance <= endDistance)
        {
            return originDistance;
        }
        return endDistance;
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static Vector2 GetVector(Vector2 point, Vector2 target)
    {
        return target - point;
    }
    #endregion
    #region capsule
    public static bool CheckCollision2D(this CapsuleData2D capsuleData, IBaseCollisionData2D collisionData, out Vector2 hitPointA, out Vector2 hitPointB)
    {
        switch (collisionData)
        {
            case CircleData2D circle:
                {
                    return capsuleData.CheckCollision2D(circle, out hitPointA, out hitPointB);
                }
            case CapsuleData2D capsule:
                {
                    return capsuleData.CheckCollision2D(capsule, out hitPointA, out hitPointB);
                }
            case LineData2D line:
                {
                    return capsuleData.CheckCollision2D(line, out hitPointA, out hitPointB);
                }
            case BoxData2D box:
                {
                    return capsuleData.CheckCollision2D(box, out hitPointA, out hitPointB);
                }
        }
        hitPointA = default(Vector2);
        hitPointB = default(Vector2);
        return false;
    }
    /// <summary>
    /// point
    /// </summary>
    /// <param name="capsuleData"></param>
    /// <param name="position"></param>
    /// <returns></returns>
    public static bool CheckCollision2D(this CapsuleData2D capsuleData, Vector2 position, out Vector2 hitPoint)
    {
        Vector2 point = GetNearestPoint2D(capsuleData.originPoint, capsuleData.endPoint, position);
        //Debug.Log(point);
        float distance = GetDistance2D(position, point);

        if (distance - capsuleData.radius <= 0)
        {
            hitPoint = point;
            return true;
        }
        hitPoint = default;
        return false;
    }
    /// <summary>
    /// �J�v�Z���Ɖ~��2D�Փ˔�����s���A�����̐ڐG�_���v�Z���܂��B
    /// </summary>
    /// <param name="capsuleData">�J�v�Z���f�[�^</param>
    /// <param name="circleData">�~�f�[�^</param>
    /// <param name="hitPointA">�J�v�Z����̐ڐG�_</param>
    /// <param name="hitPointB">�~��̐ڐG�_</param>
    /// <returns>�Փ˂��Ă���ꍇ��true</returns>
    public static bool CheckCollision2D(this CapsuleData2D capsuleData, CircleData2D circleData, out Vector2 hitPointA, out Vector2 hitPointB)
    {
        // �J�v�Z��������̍ŋߓ_���擾
        Vector2 nearestPoint = GetNearestPoint2D(capsuleData.originPoint, capsuleData.endPoint, circleData.position);

        // �~�̒��S�ƍŋߓ_�̃x�N�g������ы������v�Z
        Vector2 direction = circleData.position - nearestPoint;
        float distance = direction.magnitude;

        // �Փ˔���: ���������I�u�W�F�N�g�̔��a�̍��v�ȉ���
        if (distance < capsuleData.radius + circleData.radius)
        {
            // �J�v�Z����̐ڐG�_
            hitPointA = nearestPoint + direction.normalized * capsuleData.radius;
            // �~��̐ڐG�_
            hitPointB = circleData.position - direction.normalized * circleData.radius;
            return true;
        }

        // �Փ˂��Ă��Ȃ��ꍇ�̓f�t�H���g�l��Ԃ�
        hitPointA = hitPointB = default;
        return false;
    }
    /// <summary>
    /// �J�v�Z���Ɛ�����2D�Փ˔�����s���A�����̐ڐG�_���v�Z���܂��B
    /// </summary>
    /// <param name="capsuleData">�J�v�Z���f�[�^</param>
    /// <param name="lineData">�����f�[�^</param>
    /// <param name="hitPointA">�J�v�Z����̐ڐG�_</param>
    /// <param name="hitPointB">������̐ڐG�_</param>
    /// <returns>�Փ˂��Ă���ꍇ��true</returns>
    public static bool CheckCollision2D(this CapsuleData2D capsuleData, LineData2D lineData, out Vector2 hitPointA, out Vector2 hitPointB)
    {
        // ������̍ŋߓ_���v�Z
        Vector2 lineNearestPoint = GetNearestPoint2D(lineData.originPoint, lineData.endPoint, capsuleData.originPoint);

        // �J�v�Z���̒��S����̍ŋߓ_���v�Z
        Vector2 capsuleNearestPoint = GetNearestPoint2D(capsuleData.originPoint, capsuleData.endPoint, lineData.originPoint);

        // �ŋߓ_�Ԃ̋������v�Z
        float distance = Vector2.Distance(lineNearestPoint, capsuleNearestPoint);

        // �Փ˔���: �������J�v�Z���̔��a�ȉ��Ȃ�Փ�
        if (distance <= capsuleData.radius)
        {
            // �ڐG�_���v�Z
            hitPointA = capsuleNearestPoint;  // �J�v�Z����̐ڐG�_
            hitPointB = lineNearestPoint;    // ������̐ڐG�_
            return true;
        }

        // �Փ˂��Ă��Ȃ��ꍇ�̓f�t�H���g�l��Ԃ�
        hitPointA = hitPointB = default;
        return false;
    }

    /// <summary>
    /// 2�̃J�v�Z����2D�Փ˔�����s���A�����̐ڐG�_���v�Z���܂��B
    /// </summary>
    /// <param name="capsuleData1">1�ڂ̃J�v�Z���f�[�^</param>
    /// <param name="capsuleData2">2�ڂ̃J�v�Z���f�[�^</param>
    /// <param name="hitPointA">1�ڂ̃J�v�Z����̐ڐG�_</param>
    /// <param name="hitPointB">2�ڂ̃J�v�Z����̐ڐG�_</param>
    /// <returns>�Փ˂��Ă���ꍇ��true</returns>
    public static bool CheckCollision2D(this CapsuleData2D capsuleData1, CapsuleData2D capsuleData2, out Vector2 hitPointA, out Vector2 hitPointB)
    {
        // ���J�v�Z���̐�����̍ŋߓ_���擾
        Vector2 nearestPoint1 = GetNearestPoint2D(capsuleData1.originPoint, capsuleData1.endPoint, capsuleData2.originPoint);
        Vector2 nearestPoint2 = GetNearestPoint2D(capsuleData2.originPoint, capsuleData2.endPoint, capsuleData1.originPoint);

        // �ŋߓ_�Ԃ̋������v�Z
        Vector2 direction = nearestPoint2 - nearestPoint1;
        float distance = direction.magnitude;

        // �Փ˔���: ���������a�̍��v�ȉ���
        if (distance < (capsuleData1.radius + capsuleData2.radius))
        {
            // 1�ڂ̃J�v�Z����̐ڐG�_
            hitPointA = nearestPoint1 + direction.normalized * capsuleData1.radius;
            // 2�ڂ̃J�v�Z����̐ڐG�_
            hitPointB = nearestPoint2 - direction.normalized * capsuleData2.radius;
            return true;
        }

        // �Փ˂��Ă��Ȃ��ꍇ�̓f�t�H���g�l��Ԃ�
        hitPointA = hitPointB = default;
        return false;
    }
    /// <summary>
    /// �J�v�Z���ƃ{�b�N�X��2D�Փ˔�����s���A�����̐ڐG�_���v�Z���܂��B
    /// </summary>
    /// <param name="capsule">�J�v�Z���f�[�^</param>
    /// <param name="box">�{�b�N�X�f�[�^</param>
    /// <param name="hitPointA">�J�v�Z����̐ڐG�_</param>
    /// <param name="hitPointB">�{�b�N�X��̐ڐG�_</param>
    /// <returns>�Փ˂��Ă���ꍇ��true</returns>
    public static bool CheckCollision2D(this CapsuleData2D capsule, BoxData2D box, out Vector2 hitPointA, out Vector2 hitPointB)
    {
        hitPointA = hitPointB = default;

        // �{�b�N�X�̃G�b�W���X�g���擾
        Vector2[][] edges = box.GetEdges();

        float minDistance = float.MaxValue;
        Vector2 closestCapsulePoint = default;
        Vector2 closestBoxPoint = default;

        // �{�b�N�X�̊e�G�b�W�ɂ��Ĕ���
        foreach (Vector2[] edge in edges)
        {
            Vector2 edgeStart = edge[0];
            Vector2 edgeEnd = edge[1];

            // �J�v�Z���̐����ƃ{�b�N�X�̃G�b�W�Ԃ̍ŋߓ_���v�Z
            Vector2 capsuleNearestPoint = GetNearestPoint2D(capsule.originPoint, capsule.endPoint, edgeStart);
            Vector2 edgeNearestPoint = GetNearestPoint2D(edgeStart, edgeEnd, capsuleNearestPoint);

            // �ŋߓ_�Ԃ̋������v�Z
            float distance = GetDistance2D(capsuleNearestPoint, edgeNearestPoint);

            // �ŒZ�����̍X�V
            if (distance < minDistance)
            {
                minDistance = distance;
                closestCapsulePoint = capsuleNearestPoint;
                closestBoxPoint = edgeNearestPoint;
            }
        }

        // �Փ˔���: �ŒZ�������J�v�Z���̔��a�ȉ���
        if (minDistance <= capsule.radius)
        {
            // �ڐG�_��ݒ�
            Vector2 direction = (closestBoxPoint - closestCapsulePoint).normalized;
            hitPointA = closestCapsulePoint + direction * capsule.radius; // �J�v�Z����̐ڐG�_
            hitPointB = closestBoxPoint; // �{�b�N�X��̐ڐG�_
            return true;
        }

        return false;
    }
    #endregion
    #region circle

    /// <summary>
    /// CircleData2D���m�̏Փ˔���B�����̃I�u�W�F�N�g��ł̐ڐG�_���v�Z���܂��B
    /// </summary>
    public static bool CheckCollision2D(this CircleData2D circleData, CircleData2D circleData1, out Vector2 hitPointA, out Vector2 hitPointB)
    {
        // 2�̉~�̒��S�Ԃ̋������v�Z
        float distance = GetDistance2D(circleData.position, circleData1.position);

        // �Փ˔���: ���������a�̍��v�ȉ���
        if (distance <= circleData.radius + circleData1.radius)
        {
            // ���ꂼ��̐ڐG�_���v�Z
            Vector2 direction = (circleData1.position - circleData.position).normalized;
            hitPointA = circleData.position + direction * circleData.radius; // circleData�
            hitPointB = circleData1.position - direction * circleData1.radius; // circleData1�
            return true;
        }

        // �Փ˂��Ȃ��ꍇ�̓f�t�H���g�l
        hitPointA = hitPointB = default;
        return false;
    }

    /// <summary>
    /// CircleData2D�ƔC�ӂ̃I�u�W�F�N�g�̏Փ˔���B
    /// </summary>
    public static bool CheckCollision2D(this CircleData2D circleData, IBaseCollisionData2D collisionData, out Vector2 hitPointA, out Vector2 hitPointB)
    {
        switch (collisionData)
        {
            case CircleData2D circle:
                return circleData.CheckCollision2D(circle, out hitPointA, out hitPointB);
            case CapsuleData2D capsule:
                return circleData.CheckCollision2D(capsule, out hitPointA, out hitPointB);
            case LineData2D line:
                return circleData.CheckCollision2D(line, out hitPointA, out hitPointB);
            case BoxData2D box:
                return circleData.CheckCollision2D(box, out hitPointA, out hitPointB);
        }

        // �Ή����Ă��Ȃ��^�̏ꍇ�͏Փ˂Ȃ��Ƃ݂Ȃ�
        hitPointA = hitPointB = default;
        return false;
    }

    /// <summary>
    /// CircleData2D��LineData2D�̏Փ˔���B�ڐG�_���v�Z���܂��B
    /// </summary>
    public static bool CheckCollision2D(this CircleData2D circleData, LineData2D lineData, out Vector2 hitPointA, out Vector2 hitPointB)
    {
        // ������̉~�ɍł��߂��_���v�Z
        Vector2 nearestPoint = GetNearestPoint2D(lineData.originPoint, lineData.endPoint, circleData.position);
        float distance = GetDistance2D(circleData.position, nearestPoint);

        // �Փ˔���: �ŋߓ_���~�̔��a�ȓ��ɂ��邩
        if (distance <= circleData.radius)
        {
            Vector2 direction = (nearestPoint - circleData.position).normalized;
            hitPointA = circleData.position + direction * circleData.radius; // �~�̐ڐG�_
            hitPointB = nearestPoint; // �����̐ڐG�_
            return true;
        }

        // �Փ˂��Ȃ��ꍇ�̓f�t�H���g�l
        hitPointA = hitPointB = default;
        return false;
    }

    /// <summary>
    /// CircleData2D��CapsuleData2D�̏Փ˔���B�ڐG�_���v�Z���܂��B
    /// </summary>
    public static bool CheckCollision2D(this CircleData2D circleData, CapsuleData2D capsuleData, out Vector2 hitPointA, out Vector2 hitPointB)
    {
        // �J�v�Z���̐�����̍ŋߓ_���擾
        Vector2 nearestPoint = GetNearestPoint2D(capsuleData.originPoint, capsuleData.endPoint, circleData.position);
        float distance = GetDistance2D(circleData.position, nearestPoint);

        // �Փ˔���: �ŋߓ_���~�ƃJ�v�Z���̔��a�̍��v�ȓ���
        if (distance <= circleData.radius + capsuleData.radius)
        {
            Vector2 direction = (nearestPoint - circleData.position).normalized;
            hitPointA = circleData.position + direction * circleData.radius; // �~�̐ڐG�_
            hitPointB = nearestPoint - direction * capsuleData.radius; // �J�v�Z���̐ڐG�_
            return true;
        }

        // �Փ˂��Ȃ��ꍇ�̓f�t�H���g�l
        hitPointA = hitPointB = default;
        return false;
    }

    /// <summary>
    /// CircleData2D��BoxData2D�̏Փ˔���B�ڐG�_���v�Z���܂��B
    /// </summary>
    public static bool CheckCollision2D(this CircleData2D circle, BoxData2D box, out Vector2 hitPointA, out Vector2 hitPointB)
    {
        hitPointA = hitPointB = default;

        float minDistance = float.MaxValue;
        Vector2 closestPoint = default;

        // �{�b�N�X�̊e�G�b�W�Ɖ~�̍ŋߖT�_���v�Z
        foreach (Vector2[] edge in box.GetEdges())
        {
            if (CheckLineOut(edge[0], edge[1], circle.position, out Vector2 nearestPoint))
            {
                float distance = GetDistance2D(nearestPoint, circle.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestPoint = nearestPoint;
                }
            }
        }

        // �ŋߖT�_���~�̔��a�ȓ��łȂ���ΐڐG�Ȃ�
        if (minDistance > circle.radius)
            return false;

        hitPointB = closestPoint; // �{�b�N�X��̐ڐG�_

        // �~����̐ڐG�_���v�Z
        Vector2 direction = (closestPoint - circle.position).normalized;
        hitPointA = circle.position + direction * circle.radius;
       
        return true;
    }

    #endregion
    #region line
    /// <summary>
    /// �����ƔC�ӂ̏ՓˑΏۂƂ̏Փ˔�����s���A�o�����̐ڐG�_��Ԃ��܂��B
    /// </summary>
    /// <param name="lineData">�����f�[�^</param>
    /// <param name="collisionData">�ՓˑΏۃf�[�^</param>
    /// <param name="hitPointA">������̐ڐG�_</param>
    /// <param name="hitPointB">�ՓˑΏۊ�̐ڐG�_</param>
    /// <returns>�Փ˂��Ă��邩�ǂ���</returns>
    public static bool CheckCollision2D(this LineData2D lineData, IBaseCollisionData2D collisionData, out Vector2 hitPointA, out Vector2 hitPointB)
    {
        switch (collisionData)
        {
            case CircleData2D circle:
                {
                    return lineData.CheckCollision2D(circle, out hitPointA, out hitPointB);
                }
            case CapsuleData2D capsule:
                {
                    return lineData.CheckCollision2D(capsule, out hitPointA, out hitPointB);
                }
            case LineData2D line:
                { 
                    return lineData.CheckCollision2D(line, out hitPointA, out hitPointB); 
                }
            case BoxData2D box:
                {
                    return lineData.CheckCollision2D(box, out hitPointA, out hitPointB);
                }
        }

        hitPointA = default;
        hitPointB = default;
        return false;
    }

    /// <summary>
    /// �����Ɠ_�̏Փ˔�����s���A�o�����̐ڐG�_��Ԃ��܂��B
    /// </summary>
    /// <param name="lineData">�����f�[�^</param>
    /// <param name="position">�_�̍��W</param>
    /// <param name="hitPointA">������̐ڐG�_</param>
    /// <param name="hitPointB">�_��̐ڐG�_</param>
    /// <returns>�Փ˂��Ă��邩�ǂ���</returns>
    public static bool CheckCollision2D(this LineData2D lineData, Vector2 position, out Vector2 hitPointA, out Vector2 hitPointB)
    {
        Vector2 nearestPoint = GetNearestPoint2D(lineData.originPoint, lineData.endPoint, position);
        float distance = GetDistance2D(position, nearestPoint);

        if (distance <= 0)
        {
            hitPointA = nearestPoint;
            hitPointB = position;
            return true;
        }

        hitPointA = default;
        hitPointB = default;
        return false;
    }

    /// <summary>
    /// �����Ɖ~�̏Փ˔�����s���A�o�����̐ڐG�_��Ԃ��܂��B
    /// </summary>
    /// <param name="lineData">�����f�[�^</param>
    /// <param name="circleData">�~�f�[�^</param>
    /// <param name="hitPointA">������̐ڐG�_</param>
    /// <param name="hitPointB">�~��̐ڐG�_</param>
    /// <returns>�Փ˂��Ă��邩�ǂ���</returns>
    public static bool CheckCollision2D(this LineData2D lineData, CircleData2D circleData, out Vector2 hitPointA, out Vector2 hitPointB)
    {
        hitPointA = GetNearestPoint2D(lineData.originPoint, lineData.endPoint, circleData.position);
        float distance = GetDistance2D(circleData.position, hitPointA);

        if (distance <= circleData.radius)
        {
            Vector2 direction = (hitPointA - circleData.position).normalized;
            hitPointB = circleData.position + direction * circleData.radius;
            return true;
        }

        hitPointA = default;
        hitPointB = default;
        return false;
    }

    /// <summary>
    /// �����ƃJ�v�Z���̏Փ˔�����s���A�o�����̐ڐG�_��Ԃ��܂��B
    /// </summary>
    /// <param name="lineData">�����f�[�^</param>
    /// <param name="capsuleData">�J�v�Z���f�[�^</param>
    /// <param name="hitPointA">������̐ڐG�_</param>
    /// <param name="hitPointB">�J�v�Z����̐ڐG�_</param>
    /// <returns>�Փ˂��Ă��邩�ǂ���</returns>
    public static bool CheckCollision2D(this LineData2D lineData, CapsuleData2D capsuleData, out Vector2 hitPointA, out Vector2 hitPointB)
    {
        float distance = GetLinesDistance2D(lineData, capsuleData.ToLine());

        if (distance <= capsuleData.radius)
        {
            hitPointA = GetNearestPoint2D(lineData.originPoint, lineData.endPoint, capsuleData.ToLine().originPoint);
            hitPointB = capsuleData.ToLine().originPoint;
            return true;
        }

        hitPointA = default;
        hitPointB = default;
        return false;
    }

    /// <summary>
    /// 2�̐����̏Փ˔�����s���A�o�����̐ڐG�_��Ԃ��܂��B
    /// </summary>
    /// <param name="lineData1">1�ڂ̐���</param>
    /// <param name="lineData2">2�ڂ̐���</param>
    /// <param name="hitPointA">����1��̐ڐG�_</param>
    /// <param name="hitPointB">����2��̐ڐG�_</param>
    /// <returns>�Փ˂��Ă��邩�ǂ���</returns>
    public static bool CheckCollision2D(this LineData2D lineData1, LineData2D lineData2, out Vector2 hitPointA, out Vector2 hitPointB)
    {
        float distance = GetLinesDistance2D(lineData1, lineData2);

        if (distance <= 0)
        {
            hitPointA = lineData1.originPoint; // ���̒l
            hitPointB = lineData2.originPoint; // ���̒l
            return true;
        }

        hitPointA = default;
        hitPointB = default;
        return false;
    }

    /// <summary>
    /// �����ƃ{�b�N�X�̏Փ˔�����s���A�o�����̐ڐG�_��Ԃ��܂��B
    /// </summary>
    /// <param name="lineData">�����f�[�^</param>
    /// <param name="box">�{�b�N�X�f�[�^</param>
    /// <param name="hitPointA">������̐ڐG�_</param>
    /// <param name="hitPointB">�{�b�N�X��̐ڐG�_</param>
    /// <returns>�Փ˂��Ă��邩�ǂ���</returns>
    public static bool CheckCollision2D(this LineData2D lineData, BoxData2D box, out Vector2 hitPointA, out Vector2 hitPointB)
    {
        Vector2 nearestPoint = GetNearestPoint2D(box.ToLine(), lineData);

        if (GetNearestPointOnLine(box, nearestPoint, out Vector2 point))
        {
            hitPointA = default;
            hitPointB = default;
            return false;
        }

        if (GetDistance2D(point, nearestPoint) <= box.boxWidth)
        {
            hitPointA = nearestPoint;
            hitPointB = point;
            return true;
        }

        hitPointA = default;
        hitPointB = default;
        return false;
    }
    #endregion
    #region box
    /// <summary>
    /// �{�b�N�X�ƔC�ӂ̏ՓˑΏۂƂ̏Փ˔�����s���A�ڐG�_��Ԃ��܂��B
    /// </summary>
    /// <param name="boxData">�{�b�N�X�f�[�^</param>
    /// <param name="collisionData">�ՓˑΏۃf�[�^</param>
    /// <param name="hitPointA">�{�b�N�X��̐ڐG�_</param>
    /// <param name="hitPointB">�ՓˑΏۊ�̐ڐG�_</param>
    /// <returns>�Փ˂��Ă��邩�ǂ���</returns>
    public static bool CheckCollision2D(this BoxData2D boxData, IBaseCollisionData2D collisionData, out Vector2 hitPointA, out Vector2 hitPointB)
    {
        switch (collisionData)
        {
            case CircleData2D circle:
                return boxData.CheckCollision2D(circle, out hitPointA, out hitPointB);
            case CapsuleData2D capsule:
                return boxData.CheckCollision2D(capsule, out hitPointA, out hitPointB);
            case LineData2D line:
                return boxData.CheckCollision2D(line, out hitPointA, out hitPointB);
            case BoxData2D box:
                return boxData.CheckCollision2D(box, out hitPointA, out hitPointB);
        }
        hitPointA = hitPointB = default;
        return false; // ���Ή��̌^�̏ꍇ
    }

    /// <summary>
    /// �{�b�N�X�Ɠ_�̏Փ˔�����s���A�ڐG�_��Ԃ��܂��B
    /// </summary>
    public static bool CheckCollision2D(this BoxData2D box, Vector2 position, out Vector2 hitPointA, out Vector2 hitPointB)
    {
        hitPointA = hitPointB = default;

        // �ŋߖT�_���{�b�N�X�̐�����Ŏ擾
        if (!box.GetNearestPointOnLine(position, out Vector2 nearestPoint))
        {
            return false;
        }
            

        // �ŋߖT�_�Ɠ_�̋������{�b�N�X�̕��𒴂���ꍇ�͏Փ˂Ȃ�
        if (GetDistance2D(nearestPoint, position) > box.boxWidth / 2)
        {
            return false;
        }
            

        hitPointA = nearestPoint;  // �{�b�N�X��̐ڐG�_
        hitPointB = position;      // �_��̐ڐG�_
        return true;
    }

    /// <summary>
    /// �{�b�N�X�Ɛ����̏Փ˔�����s���A�ڐG�_��Ԃ��܂��B
    /// ������
    /// </summary>
    public static bool CheckCollision2D(this BoxData2D box, LineData2D line, out Vector2 hitPointA, out Vector2 hitPointB)
    {
        hitPointA = hitPointB = default;

        // �{�b�N�X�Ɛ����̍ŋߖT�_���擾
        Vector2 nearestPoint = GetNearestPoint2D(box.ToLine(), line);

        if (!box.GetNearestPointOnLine(nearestPoint, out Vector2 point))
        {
            return false;
        }
            

        // �ŋߖT�_�Ɛ����̋������{�b�N�X�̕��𒴂���ꍇ�͏Փ˂Ȃ�
        if (GetDistance2D(point, nearestPoint) > box.boxWidth / 2)
        {
            return false;
        }
            

        hitPointA = point;        // �{�b�N�X��̐ڐG�_
        hitPointB = nearestPoint; // ������̐ڐG�_
        return true;
    }

    /// <summary>
    /// �{�b�N�X�Ɖ~�̏Փ˔�����s���A�ڐG�_��Ԃ��܂��B
    /// ���؍ς�
    /// </summary>
    public static bool CheckCollision2D(this BoxData2D box, CircleData2D circle, out Vector2 hitPointA, out Vector2 hitPointB)
    {
        hitPointA = hitPointB = default;

        float minDistance = float.MaxValue;
        Vector2 closestPoint = default;

        // �{�b�N�X�̊e�G�b�W�Ɖ~�̍ŋߖT�_���v�Z
        foreach (Vector2[] edge in box.GetEdges())
        {
            if (CheckLineOut(edge[0], edge[1], circle.position, out Vector2 nearestPoint))
            {
                float distance = GetDistance2D(nearestPoint, circle.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestPoint = nearestPoint;
                }
            }
        }

        // �ŋߖT�_���~�̔��a�ȓ��łȂ���ΐڐG�Ȃ�
        if (minDistance > circle.radius)
            return false;

        hitPointA = closestPoint; // �{�b�N�X��̐ڐG�_
       
        // �~����̐ڐG�_���v�Z
        Vector2 direction = (closestPoint - circle.position).normalized;
        hitPointB = circle.position + direction * circle.radius;
        Debug.Log(hitPointA + " " + hitPointB);
        return true;
    }

    /// <summary>
    /// �J�v�Z���ƃ{�b�N�X��2D�Փ˔�����s���A�����̐ڐG�_���v�Z���܂��B
    /// ������
    /// </summary>
    /// <param name="capsule">�J�v�Z���f�[�^</param>
    /// <param name="box">�{�b�N�X�f�[�^</param>
    /// <param name="hitPointA">�J�v�Z����̐ڐG�_</param>
    /// <param name="hitPointB">�{�b�N�X��̐ڐG�_</param>
    ///
    /// <returns>�Փ˂��Ă���ꍇ��true</returns>
    public static bool CheckCollision2D(this BoxData2D box, CapsuleData2D capsule, out Vector2 hitPointA, out Vector2 hitPointB)
    {
        hitPointA = hitPointB = default;

        // �{�b�N�X�̃G�b�W���X�g���擾
        Vector2[][] edges = box.GetEdges();

        float minDistance = float.MaxValue;
        Vector2 closestCapsulePoint = default;
        Vector2 closestBoxPoint = default;

        // �{�b�N�X�̊e�G�b�W�ɂ��Ĕ���
        foreach (Vector2[] edge in edges)
        {
            Vector2 edgeStart = edge[0];
            Vector2 edgeEnd = edge[1];

            // �J�v�Z���̐����ƃ{�b�N�X�̃G�b�W�Ԃ̍ŋߓ_���v�Z
            Vector2 capsuleNearestPoint = GetNearestPoint2D(capsule.originPoint, capsule.endPoint, edgeStart);
            Vector2 edgeNearestPoint = GetNearestPoint2D(edgeStart, edgeEnd, capsuleNearestPoint);

            // �ŋߓ_�Ԃ̋������v�Z
            float distance = GetDistance2D(capsuleNearestPoint, edgeNearestPoint);

            // �ŒZ�����̍X�V
            if (distance < minDistance)
            {
                minDistance = distance;
                closestCapsulePoint = capsuleNearestPoint;
                closestBoxPoint = edgeNearestPoint;
            }
        }

        // �Փ˔���: �ŒZ�������J�v�Z���̔��a�ȉ���
        if (minDistance > capsule.radius)
        {
            return false;
        }
        // �ڐG�_��ݒ�
        Vector2 direction = (closestBoxPoint - closestCapsulePoint).normalized;
        hitPointB = closestCapsulePoint + direction * capsule.radius; // �J�v�Z����̐ڐG�_
        hitPointA = closestBoxPoint; // �{�b�N�X��̐ڐG�_
        return true;
        
    }

    /// <summary>
    /// �{�b�N�X���m�̏Փ˔�����s���A�ڐG�_��Ԃ��܂��B
    /// ������
    /// </summary>
    public static bool CheckCollision2D(this BoxData2D box1, BoxData2D box2, out Vector2 hitPointA, out Vector2 hitPointB)
    {
        hitPointA = hitPointB = default;

        // �ŋߖT�_���擾
        Vector2 nearestPoint = GetNearestPoint2D(box1.ToLine(), box2.ToLine());

        if (!box1.GetNearestPointOnLine(nearestPoint, out Vector2 point))
            return false;

        // �ŋߖT�_�ƃ{�b�N�X�̋��������ꂼ��̕��̍��v�𒴂���ꍇ�͏Փ˂Ȃ�
        if (GetDistance2D(point, nearestPoint) > (box1.boxWidth + box2.boxWidth) / 2)
            return false;

        hitPointA = point;        // �{�b�N�X1��̐ڐG�_
        hitPointB = nearestPoint; // �{�b�N�X2��̐ڐG�_
        return true;
    }

    /// <summary>
    /// �_���{�b�N�X�̐����̊O�ɂ��邩���ʂ��A�ŋߖT�_���v�Z���܂��B
    /// </summary>
    private static bool GetNearestPointOnLine(this BoxData2D box, Vector2 position, out Vector2 nearestPoint)
    {
        Vector2 direction = (box.endPoint - box.originPoint).normalized;
        Vector2 originToPoint = position - box.originPoint;
        Vector2 endToPoint = position - box.endPoint;

        if (Vector2.Dot(direction, originToPoint) < 0 || Vector2.Dot(direction, endToPoint) > 0)
        {
            nearestPoint = default;
            return false;
        }

        nearestPoint = box.originPoint + direction * Vector2.Dot(direction, originToPoint);
        return true;
    }

   
    private static bool CheckLineOut(Vector2 origin,Vector2 end, Vector2 position, out Vector2 nearestPoint)
    {
        // �����̕����x�N�g��
        Vector2 direction = (end - origin);
        float lineLength = direction.magnitude;   // �����̒���
        direction.Normalize();                   // �P�ʃx�N�g����

        // �����̊J�n�_����Ώۓ_�ւ̃x�N�g��
        Vector2 originToPoint = position - origin;

        // ������̍ŋߖT�_�̈ʒu���X�J���[�l�Ōv�Z
        float projection = Vector2.Dot(originToPoint, direction);

        // �����͈̔͊O���`�F�b�N
        if (projection < 0)
        {
            // �ŋߖT�_�� origin ���̒[�_
            nearestPoint = origin;
            return false; // �͈͊O
        }
        else if (projection > lineLength)
        {
            // �ŋߖT�_�� end ���̒[�_
            nearestPoint = end;
            return false; // �͈͊O
        }

        // ������̍ŋߖT�_���v�Z
        nearestPoint = origin + direction * projection;

        return true; // ������ɍŋߖT�_�����݂���
    }
    #endregion


    //vector3��xz��vector2�ɕϊ�����
    public static Vector2 ToVector2XZ(this Vector3 vector3)
    {

        return new Vector2(vector3.x, vector3.z);

    }


}
