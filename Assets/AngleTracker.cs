using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleTracker : MonoBehaviour
{
    public enum Axis { X, Y, Z }

    [Header("检测设置")]
    public Axis detectionAxis = Axis.Y;  // 检测的旋转轴
    public float threshold = 90f;        // 旋转阈值（度）
    public float hysteresis = 5f;        // 滞回值（度）
    public int targetCount = 3;          // 目标触发次数

    [Header("状态监控")]
    [SerializeField] private int currentCount; // 当前计数
    [SerializeField] private bool isAbove;     // 是否在阈值上方

    private void Update()
    {
        if (Level1Manager.instance.TaskIndex!=3)
            return;
        // 获取当前旋转角度
        float currentAngle = GetCurrentAngle();

        // 状态转换逻辑
        if (!isAbove)
        {
            // 从低于状态进入高于状态
            if (currentAngle >= threshold)
            {
                currentCount++;
                isAbove = true;
                Debug.Log($"达到阈值! 计数: {currentCount}");
            }
        }
        else
        {
            // 计算滞回边界（处理360度环绕）
            float lowerBound = threshold - hysteresis;

            // 处理角度环绕的特殊情况
            if (lowerBound < 0)
            {
                // 当滞回边界小于0时，需要检查两个区间
                if (currentAngle < lowerBound + 360 && currentAngle > threshold)
                {
                    isAbove = false;
                    Debug.Log("重置状态");
                }
            }
            else
            {
                // 标准情况：检查是否低于滞回边界
                if (currentAngle < lowerBound)
                {
                    isAbove = false;
                    Debug.Log("重置状态");
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
    //             $"计数: {currentCount}/{targetCount}", style);
    //    GUI.Label(new Rect(10, 50, 300, 40),
    //             $"状态: {(isAbove ? "高于阈值" : "低于阈值")}", style);
    //    GUI.Label(new Rect(10, 90, 300, 40),
    //             $"匹配: {IsCountMatched()}", style);
    //}
}
