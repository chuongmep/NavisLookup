using System.Reflection;

namespace AppInfo.Model
{
    /// <summary>
    ///  constructor of node item
    /// </summary>
    public class NodeInfo : IDisposable
    {
        public Type Type { get; set; }
        public object Value { get; set; }
        public bool IsStatic { get; set; }
        public int Index { get; set; }
        public MemberInfo Info { get; set; }

        /// <summary>
        ///  init the node info
        /// </summary>
        /// <param name="type"></param>
        /// <param name="value"></param>
        /// <param name="isStatic"></param>
        /// <param name="info"></param>
        /// <param name="index"></param>
        public NodeInfo(Type type, object value, bool isStatic, MemberInfo info, int index)
        {
            Type = type;
            Value = value;
            IsStatic = isStatic;
            Index = index;
            Info = info;
        }

        public void Dispose()
        {
            Type = null;
            Value = null;
        }
    }
}