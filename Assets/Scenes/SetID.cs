using System;
using UnityEngine;

public class SetID : MonoBehaviour
{
    void Start()
    {
        // 現在設定されているIDを調べる
        string value = Environment.GetEnvironmentVariable("ROS_DOMAIN_ID");
        Debug.Log("current ROS_DOMAIN_ID:" + value + "\n");
        // ROS_DOMAIN_IDに123を設定する
        Environment.SetEnvironmentVariable("ROS_DOMAIN_ID", "30");
        Debug.Log("DOMAIN_ID set OK\n");
        // 上手く設定できているか確認する
        value = Environment.GetEnvironmentVariable("ROS_DOMAIN_ID");
        Debug.Log("current ROS_DOMAIN_ID:" + value + "\n");
    }
}