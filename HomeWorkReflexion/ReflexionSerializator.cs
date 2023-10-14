using System.Reflection;
using System.Text;

namespace HomeWorkReflexion
{
    internal class ReflexionSerializator : IReflexionSerializator
    {
        private FieldInfo[] GetFields<T>(T obj)
        {
            if (obj == null)
            {
                throw new NullReferenceException(nameof(obj));
            }

            return obj.GetType().GetFields(BindingFlags.Public |
                                             BindingFlags.NonPublic |
                                             BindingFlags.Instance);
        }

        private void SetFieldValue<T>(T obj, FieldInfo field, object fieldValue)
        {
            System.TypeCode typeCode = System.Type.GetTypeCode(field.FieldType);

            switch (typeCode)
            {
                case TypeCode.Int32:
                    field.SetValue(obj, Convert.ToInt32(fieldValue));
                    break;
                case TypeCode.Int64:
                    field.SetValue(obj, Convert.ToInt64(fieldValue));
                    break;
                case TypeCode.Double:
                    field.SetValue(obj, Convert.ToDouble(fieldValue));
                    break;
                case TypeCode.Boolean:
                    field.SetValue(obj, Convert.ToBoolean(fieldValue));
                    break;
                case TypeCode.String:
                    field.SetValue(obj, fieldValue);
                    break;
                default:
                    field.SetValue(obj, fieldValue);
                    break;
            }
        }

        public T Deserialize<T>(string csvWithoutHeaders, char delimeter)
        {
            var splits = csvWithoutHeaders.Split(delimeter);

            var obj = Activator.CreateInstance<T>();
            var fields = GetFields(obj);

            if (fields.Length != splits.Length)
            {
                throw new FormatException("Incorrect input string. Incorrect fields count in string. Cannot deserialize to type");
            }
            for (var i = 0; i < splits.Length; i++)
            {
                System.TypeCode typeCode = System.Type.GetTypeCode(fields[i].FieldType);
                var currentSplit = splits[i];
                SetFieldValue(obj, fields[i], currentSplit);
            }

            return obj;
        }

        public string Serialize<T>(T obj)
        {
            var fields = GetFields(obj);
            var sb = new StringBuilder("{ ");

            foreach (var field in fields)
            {
                sb.Append(field.Name + ": ");
                sb.Append(field.GetValue(obj));
                sb.Append(", ");
            }

            sb.Append(" }");

            return sb.ToString();
        }

    }
}
