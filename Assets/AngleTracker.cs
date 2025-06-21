using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleTracker : MonoBehaviour
{
    public enum Axis { X, Y, Z }

    [Header("�������")]
    public Axis detectionAxis = Axis.Y;  // ������ת��
    public float threshold = 90f;        // ��ת��ֵ���ȣ�
    public float hysteresis = 5f;        // �ͻ�ֵ���ȣ�
    public int targetCount = 3;          // Ŀ�괥������

    [Header("״̬���")]
    [SerializeField] private int currentCount; // ��ǰ����
    [SerializeField] private bool isAbove;     // �Ƿ�����ֵ�Ϸ�

    private void Update()
    {
        if (Level1Manager.instance.TaskIndex!=3)
            return;
        // ��ȡ��ǰ��ת�Ƕ�
        float currentAngle = GetCurrentAngle();

        // ״̬ת���߼�
        if (!isAbove)
        {
            // �ӵ���״̬�������״̬
            if (currentAngle >= threshold)
            {
                currentCount++;
                isAbove = true;
                Debug.Log($"�ﵽ��ֵ! ����: {currentCount}");
            }
        }
        else
        {
            // �����ͻر߽磨����360�Ȼ��ƣ�
            float lowerBound = threshold - hysteresis;

            // ����ǶȻ��Ƶ��������
            if (lowerBound < 0)
            {
                // ���ͻر߽�С��0ʱ����Ҫ�����������
                if (currentAngle < lowerBound + 360 && currentAngle > threshold)
                {
                    isAbove = false;
                    Debug.Log("����״̬");
                }
            }
            else
            {
                // ��׼���������Ƿ�����ͻر߽�
                if (currentAngle < lowerBound)
                {
                    isAbove = false;
                    Debug.Log("����״̬");
                }
            }
        }
    }

    private float GetCurrentAngle()
    {
        Vector3 angles = transform.eulerAngles;

        switch (detectionAxis)
        {
            case Axis.X: return NormalizeAngle(angles.x);
            case Axis.Y: return NormalizeAngle(angles.y);
            case Axis.Z: return NormalizeAngle(angles.z);
            default: return NormalizeAngle(angles.y);
        }
    }

    private float NormalizeAngle(float angle)
    {
        angle %= 360;
        return angle < 0 ? angle + 360 : angle;
    }

    public bool IsCountMatched()
    {
        return currentCount == targetCount;
    }


    public void ResetCounter()
    {
        currentCount = 0;
        isAbove = false;
    }

    //private void OnGUI()
    //{
    //    GUIStyle style = new GUIStyle(GUI.skin.label)
    //    {
    //        fontSize = 24,
    //        alignment = TextAnchor.MiddleCenter
    //    };

    //    GUI.Label(new Rect(10, 10, 300, 40),
    //             $"����: {currentCount}/{targetCount}", style);
    //    GUI.Label(new Rect(10, 50, 300, 40),
    //             $"״̬: {(isAbove ? "������ֵ" : "������ֵ")}", style);
    //    GUI.Label(new Rect(10, 90, 300, 40),
    //             $"ƥ��: {IsCountMatched()}", style);
    //}
}
