using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TestingWASM.Shared.Data.Entities;
using TestingWASM.Shared.DTOs;

namespace TestingWASM.Shared.Context
{
    public static class Dataload
    {
        public static FormEntry LoadData()
        {

            var loc = Environment.CurrentDirectory;
            FormEntry form = new FormEntry();
            // read JSON directly from a file

            var file = new StreamReader(GetImageResourceStream("data.json"));

            string json = file.ReadToEnd();
            var ro = JsonConvert.DeserializeObject<FormEntry>(json);
           

            return ro;
            
        }

        public static FormType LoadDataType()
        {

            var loc = Environment.CurrentDirectory;
            var form = new FormType();
            // read JSON directly from a file

            var file = new StreamReader(GetImageResourceStream("data2.json"));
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                JObject o2 = (JObject)JToken.ReadFrom(reader);

                form = JsonConvert.DeserializeObject<FormType>(o2.ToString());
                return form;
            }
        }


        private static Stream GetImageResourceStream(string filename)
        {
            var assembly = Assembly.GetExecutingAssembly();

            var resourceName = $"TestingWASM.Shared.Context.{filename}";
            var x = assembly.GetManifestResourceNames();

            return assembly.GetManifestResourceStream(resourceName);
        }
        public static void AddTestData(pocContext context)
        {

            var type = LoadDataType();
            context.FormTypes.Add(type);

            var form = LoadData();
            context.FormEntries.Add(form);

            context.SaveChanges();
        }
    }
}
