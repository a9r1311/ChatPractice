using System.IO.Pipes;
using System.Threading.Tasks;
using UnityEngine;

namespace Practice.Server
{
    public class ServerAction : ServerActionBase    //  サーバー系関数を持つクラス
    {
        public ServerAction(string IP, int port) : base(IP, port)    //  接続サーバーのIPとポートを取得するコンストラクタ
        { }

        public override async Task<bool> ConnectServer()    //  サーバーに接続する
        {
            bool result = await base.ConnectServer();
            return result;
        }
    }
}