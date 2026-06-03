using System.Text;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;
using TMPro;

namespace Practice.Chat
{
    public class ChatClient : MonoBehaviour    //  サーバー接続を確認するためのクライアント
    {
        public TMP_InputField inputField;
        public TextMeshProUGUI chatText;

        private TcpClient client;
        private NetworkStream stream;
        private Thread receiveThread; // 受信専用の「別動隊」

        // サーバーから届いた最新のメッセージを格納する変数
        private string latestMessage = "";

        void Start()
        {
            // 1. 起動時にサーバーに接続しておく
            client = new TcpClient("127.0.0.1", 12345);
            stream = client.GetStream();

            // 2. 「受信専用スレッド」を開始する（メインの動きを止めないため）
            receiveThread = new Thread(new ThreadStart(ReceiveMessages));
            receiveThread.IsBackground = true; // アプリ終了時に一緒に終わるように
            receiveThread.Start();
        }

        // サーバーからの声を聴き続けるループ（別動隊の仕事）
        private void ReceiveMessages()
        {
            byte[] buffer = new byte[1024];
            try
            {
                while (true)
                {
                    // 3. サーバーからデータが届くまでここで待機
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    if (bytesRead <= 0) break;

                    // 4. バイト列をStringに変換して変数に格納！
                    latestMessage = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                    Debug.Log("サーバーから受信: " + latestMessage);
                }
            }
            catch (System.Exception e)
            {
                Debug.Log("受信停止: " + e.Message);
            }
        }

        void Update()
        {
            // 5. 受信したString変数に中身があれば、画面に表示したりする
            if (!string.IsNullOrEmpty(latestMessage))
            {
                Debug.Log("Updateで確認: " + latestMessage);
                chatText.text = latestMessage;
                // ここでUIのテキストを更新したりする
                latestMessage = ""; // 処理したら空にする
            }
        }

        public void SendMessageToServer()
        {
            if (string.IsNullOrEmpty(inputField.text)) return;
            byte[] data = Encoding.UTF8.GetBytes(inputField.text);
            stream.Write(data, 0, data.Length);
            inputField.text = "";
        }
    }
}