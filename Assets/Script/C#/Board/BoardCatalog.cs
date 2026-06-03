using System.Collections.Generic;
using UnityEngine;

namespace Practice.Gomoku
{
    [CreateAssetMenu(fileName = "BoardCatalog", menuName = "InGame/Gomoku/Catalog")]
    public class BoardCatalog : ScriptableObject    //  サイズに応じたボードを持ってるクラス
    {
        [Header("サイズ別ボード群")]
        [SerializeField] List<BoardDefition> Boards;
    }
}