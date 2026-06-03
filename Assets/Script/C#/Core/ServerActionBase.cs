using System;
using System.Text;
using System.Net.Sockets;
using System.Threading.Tasks;
using UnityEngine;

namespace Practice.Server
{
    public abstract class ServerActionBase    //  サーバー系関数のBaseClass
    {
        public TcpClient Client { get; private set; }
        public NetworkStream Stream { get; private set; }
        //public Thread ReceiveThread { get; private set; }

        string serverIPAddress;    //  接続先のIPアドレス
        int serverPort;    //  接続先のポート番号

        public ServerActionBase(string serverIP, int port)    //  接続先のIPとポートをもらうコンストラクタ
        {
            serverIPAddress = serverIP;
            serverPort = port;

            //ReceiveThread = new Thread(ReceiveLoop); 
            //ReceiveThread.IsBackground = true;
            //ReceiveThread.Start();
        }

        public virtual async Task<bool> ConnectServer()    //  サーバーに接続する
        {
            try
            {
                if (Client == null)
                {
                    Client = new TcpClient();
                }

                if (Client.Connected && Stream != null && Stream.CanWrite)    //  再接続等の必要性がなくかったら関数終了
                { return true; }

                await Client.ConnectAsync(serverIPAddress, serverPort);

                Stream = Client.GetStream();

                Debug.Log("Wondefull you connected server");
                return true;
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Amaizing you cound't connect:{e.Message}");
                return false;
            }
        }

        public virtual void GetBoardData()    //  盤面の定義データをサーバーから取得
        {
            bool happeSomeError = false;

            Task.Run(async () =>
            {
                try
                {
                    while (!happeSomeError && Client.Connected)
                    {
                        byte[] reedBuffer = new byte[4];
                        int read = await Stream.ReadAsync(reedBuffer, 0, 4);    //  4byteの読み取り処理

                        if (read == 0) break;    //  切断Break

                        int datasize = BitConverter.ToInt32(reedBuffer, 0);
                        byte[] dataBuffer = new byte[datasize];

                        int totalRead = 0;
                        while (totalRead < datasize)
                        {
                            int bytes = await Stream.ReadAsync(dataBuffer, totalRead, datasize - totalRead);
                            if (bytes == 0) break;
                            totalRead += bytes;
                        }

                        string json = Encoding.UTF8.GetString(dataBuffer);


                    }
                }
                catch
                {

                }
            });
        }
    }
}