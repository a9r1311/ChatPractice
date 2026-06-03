using UnityEngine;
using Practice.Core;
using Practice.Server;
using System.Threading.Tasks;

namespace Practice.Gomoku
{
    public class BoradBootstrap : MonoBehaviour    //  ボードの初期化クラス
    {
        ServerAction action;

        const string serverIPAdress = "127.0.0.1";    //  改善予定
        const int serverPort = 12345;    //  改善予定

        private void Awake()
        {
            action = new ServerAction(serverIPAdress, serverPort);

            ServiceLocator.Register<ServerActionBase>(action); 
        }

        async Task Start()
        {
            Debug.Log("go");
            bool isConnected = await ServiceLocator.Resolve<ServerActionBase>().ConnectServer();    //  サーバーに接続
            Debug.Log("end");
        }
    }
}