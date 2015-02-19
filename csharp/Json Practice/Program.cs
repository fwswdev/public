// Json Practice

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace JsonPractice
{
    class Program
    {
        [DataContract]
        public class User
        {
            [DataMember]
            public bool IsMember { get; set; }
            [DataMember]
            public string Name { get; set; }
            [DataMember]
            public int Age { get; set; }
        }



        public static Stream GenerateStreamFromString(string s)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }


        static void Main(string[] args)
        {
            // Reading Json String
            string strStream = "{\"IsMember\" : true, \"Name\" : \"John\", \"Age\" : 24}";

            using (var myStream = GenerateStreamFromString(strStream))
            {
                var serializer =
                    new DataContractJsonSerializer(typeof(User));
                var user = (User)serializer.ReadObject(myStream);
                myStream.Dispose();

                var isMember = user.IsMember;
                var name = user.Name;
                var age = user.Age;

                Debug.Assert(isMember == true);
                Debug.Assert(name == "John");
                Debug.Assert(age == 24);

                // Creating Json object
                var newUser = new User();
                newUser.Age = 19;
                newUser.Name = "Harry";
                newUser.IsMember = false;

                using (var memStream = new MemoryStream())
                {
                    serializer.WriteObject(memStream, newUser);
                    memStream.Position = 0;

                    var sr = new StreamReader(memStream);
                    string readJson = sr.ReadToEnd();
                    Debug.Assert(readJson == "{\"Age\":19,\"IsMember\":false,\"Name\":\"Harry\"}");
                }
            }

        }
    }
}
