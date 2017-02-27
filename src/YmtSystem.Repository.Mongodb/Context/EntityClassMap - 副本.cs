using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YmtSystem.Repository.Mongodb.Mapping;
using YmtSystem.Repository.Mongodb.Extend;

namespace YmtSystem.Repository.Mongodb.Context
{
    /// <summary>
    /// 实体映射
    /// </summary>
    public class EntityClassMap
    {
        private readonly Dictionary<string, List<EntityMappingConfigure>> _mapDic =
            new Dictionary<string, List<EntityMappingConfigure>>();

        /// <summary>
        /// 
        /// </summary>
        public EntityClassMap()
        {

        }

        /// <summary>
        /// 添加映射
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="map"></param>
        public void AddMap(EntityMappingConfigure map, string contextName)
        {
            AddMapItem(map, contextName);
        }

        /// <summary>
        /// 获取映射（转为强类型）
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public IEnumerable<EntityMappingConfigure> GetMap(string contextName)
        {
            Assert(contextName);
            return _mapDic[contextName];
        }

        public bool IsinitMap(string contextName)
        {
            return _mapDic.ContainsKey(contextName);
        }

        /// <summary>
        /// 清除所有映射
        /// </summary>
        public void Clear()
        {
            _mapDic.Clear();
        }

        private void Assert(string context)
        {
            if (_mapDic.IsEmpty()) throw new InvalidOperationException(" map list is empty");
            if (!_mapDic.ContainsKey(context))
                throw new KeyNotFoundException(string.Format("{0}:key not find ", context));
        }

        private void AddMapItem(EntityMappingConfigure map, string contextName)
        {
            if (_mapDic.ContainsKey(contextName))
            {
                _mapDic[contextName].Add(map);
            }
            else
            {
                var _list = new List<EntityMappingConfigure>();
                _list.Add(map);
                _mapDic.Add(contextName, _list);
            }
        }
    }
}
