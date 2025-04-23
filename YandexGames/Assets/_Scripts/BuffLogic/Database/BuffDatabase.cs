using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Scripts.BuffLogic.Database
{
    [CreateAssetMenu(menuName = "Buffs/BuffDatabase")]
    public class BuffDatabase : ScriptableObject
    {
        [Tooltip("Все определения баффов")]
        public BuffDefinition[] Definitions;

        public Sprite GetIcon(string buffId)
        {
            var def = Definitions.FirstOrDefault(d => d.buffId == buffId);
            return def != null ? def.icon : null;
        }

        private static BuffDatabase _instance;
        public static BuffDatabase Instance
        {
            get
            {
                if (_instance == null)
                    _instance = Resources.Load<BuffDatabase>("Buffs/_Database");
                return _instance;
            }
        }
    }
}