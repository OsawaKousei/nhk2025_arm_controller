using UnityEngine;
using ROS2;
using UnityEngine.UI; // UIコンポーネントを使用するために追加
using TMPro; // TextMeshProを使用する場合（推奨）

public class Controller : MonoBehaviour
{
    private ROS2UnityComponent ros2Unity;
    private ROS2Node ros2Node;
    private IPublisher<std_msgs.msg.String> chatter_pub;
    private IPublisher<std_msgs.msg.Float32> target_value_pub;

    // UIコンポーネント参照
    [SerializeField] private Button increaseButton;
    [SerializeField] private Button decreaseButton;
    [SerializeField] private Button resetButton;
    [SerializeField] private Button sendButton; // 送信ボタンへの参照を追加
    [SerializeField] private TMP_Text valueDisplayText; // TextMeshProを使用（または Text を使用）

    // 値の設定
    [SerializeField] private float defaultValue = 0.0f;
    [SerializeField] private float incrementValue = 0.1f;

    // 現在の値
    private float currentValue;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ros2Unity = GetComponent<ROS2UnityComponent>();
        if (ros2Unity.Ok())
        {
            if (ros2Node == null)
            {
                ros2Node = ros2Unity.CreateNode("ROS2UnityTalkerNode");
                chatter_pub = ros2Node.CreatePublisher<std_msgs.msg.String>("chatter");
                target_value_pub = ros2Node.CreatePublisher<std_msgs.msg.Float32>("target_value");
            }
        }

        // 値を初期化
        currentValue = defaultValue;
        UpdateDisplayText();

        // ボタンにリスナーを設定
        increaseButton.onClick.AddListener(IncreaseValue);
        decreaseButton.onClick.AddListener(DecreaseValue);
        resetButton.onClick.AddListener(ResetValue);
        sendButton.onClick.AddListener(SendValue); // 送信ボタンにリスナーを設定

        Debug.Log("ROS2 Unity Controller started.");
        chatter_pub.Publish(new std_msgs.msg.String { Data = "Hello ROS2 from Unity!" });
    }

    // Update is called once per frame
    void Update()
    {

    }

    // 値を増加させるメソッド
    public void IncreaseValue()
    {
        currentValue += incrementValue;
        UpdateDisplayText();
    }

    // 値を減少させるメソッド
    public void DecreaseValue()
    {
        currentValue -= incrementValue;
        UpdateDisplayText();
    }

    // 値をリセットするメソッド
    public void ResetValue()
    {
        currentValue = defaultValue;
        UpdateDisplayText();
    }

    // 現在の値を送信するメソッド
    public void SendValue()
    {
        if (target_value_pub != null)
        {
            var msg = new std_msgs.msg.Float32 { Data = currentValue };
            target_value_pub.Publish(msg);
            Debug.Log($"Published value: {currentValue}");

            chatter_pub.Publish(new std_msgs.msg.String { Data = $"Published value: {currentValue}" });
        }
    }

    // 表示テキストを更新するメソッド
    private void UpdateDisplayText()
    {
        if (valueDisplayText != null)
        {
            valueDisplayText.text = currentValue.ToString("F2"); // 小数点以下2桁で表示
        }
    }
}
