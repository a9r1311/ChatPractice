using UnityEngine;
using UnityEngine.AddressableAssets;


namespace Practice.Gomoku
{
    [CreateAssetMenu(fileName ="BoardDefinition", menuName = "InGame/Gomoku/BoardDef")]
    public class BoardDefition : ScriptableObject
    {
        [Header("ê‡ñæ")]
        [SerializeField] string BoardLabel;

        [Header("Addressable adress")]
        public AssetReferenceGameObject prefabReference;
    }
}