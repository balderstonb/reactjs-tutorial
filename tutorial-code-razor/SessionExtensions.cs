using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using tutorial_code_razor.Models;

namespace tutorial_code_razor
{
    public static class SessionExtensions
    {
        public static List<CommentModel> GetComments(this ISession session, string key)
        {
            var data = session.Get(key);

            if (data == null)
            {
                return null;
            }

            var ms = new MemoryStream(data);
            var bf = new BinaryFormatter();

            return (List<CommentModel>)bf.Deserialize(ms);
        }
        public static void SetComments(this ISession session, string key, List<CommentModel> value)
        {
            var ms = new MemoryStream();
            var bf = new BinaryFormatter();

            bf.Serialize(ms, value);

            ms.Close();

            session.Set(key, ms.ToArray());
        }
    }
}
