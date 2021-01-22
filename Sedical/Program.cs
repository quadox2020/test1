using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Sedical.DAO;
using CsvHelper;
using System.Data;
using Newtonsoft.Json;
using System.Globalization;

namespace Sedical
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("SEDICAL CONCENTRADOR DE CONTADORES");

            Console.WriteLine("El archivo debe estar en formato valido JSON.");
            Console.WriteLine("Dime el nombre del archivo sin formato: ");
            string namePath = Console.ReadLine();

            var jsonString = File.ReadAllText(namePath + ".json");

            var obj = System.Text.Json.JsonSerializer.Deserialize<List<SedicalDAO>>(jsonString);

            var values = obj.Where(x => x.name.Equals("ValueDesc")).FirstOrDefault();

            var result = new List<SedicalO>();

            var data = values.columns.Where(x => x.name.Equals("LoggerLastValue")).FirstOrDefault();
            var name = obj.Where(x => x.name.Equals("Device")).FirstOrDefault().columns.Where(x => x.name.Equals("Name")).FirstOrDefault();

            if (values != null)
            {
                var indice = 6;
                var name_ind = 6;
                do
                {
                    var sedical = new SedicalO
                    {
                        DireccionSecundaria = data.values[indice + 14].ToString(),
                        Nombre = name.values[name_ind].ToString(),
                        EnergiaActual = data.values[indice].ToString(),
                        VolumenActual = data.values[indice + 2].ToString(),
                        VolumenInput1_Actual = data.values[indice + 7].ToString(),
                        VolumenInput2_Actual = data.values[indice + 10].ToString(),
                        TemperaturaImpulsion = data.values[indice + 15].ToString(),
                        TemperaturaRetorno = data.values[indice + 16].ToString(),
                        CaudalInstantaneo = data.values[indice + 17].ToString(),
                        PotenciaInstantanea = data.values[indice + 18].ToString(),
                        HorasFuncionamiento = data.values[indice + 20].ToString(),
                        EnergiaMes1 = data.values[indice + 22].ToString(),
                        EnergiaMes2 = data.values[indice + 23].ToString(),
                        EnergiaMes3 = data.values[indice + 24].ToString(),
                        EnergiaMes4 = data.values[indice + 25].ToString(),
                        EnergiaMes5 = data.values[indice + 26].ToString(),
                        EnergiaMes6 = data.values[indice + 27].ToString(),
                        EnergiaMes7 = data.values[indice + 28].ToString(),
                        EnergiaMes8 = data.values[indice + 29].ToString(),
                        EnergiaMes9 = data.values[indice + 30].ToString(),
                        EnergiaMes10 = data.values[indice + 31].ToString(),
                        EnergiaMes11 = data.values[indice + 32].ToString(),
                        EnergiaMes12 = data.values[indice + 33].ToString(),
                        EnergiaMes13 = data.values[indice + 34].ToString(),
                        EnergiaMes14 = data.values[indice + 35].ToString(),
                        EnergiaMes15 = data.values[indice + 36].ToString(),
                        EnergiaMes16 = data.values[indice + 37].ToString(),
                        EnergiaMes17 = data.values[indice + 38].ToString(),
                        EnergiaMes18 = data.values[indice + 39].ToString(),
                        Input1Mes1 = data.values[indice + 60].ToString(),
                        Input1Mes2 = data.values[indice + 61].ToString(),
                        Input1Mes3 = data.values[indice + 62].ToString(),
                        Input1Mes4 = data.values[indice + 63].ToString(),
                        Input1Mes5 = data.values[indice + 64].ToString(),
                        Input1Mes6 = data.values[indice + 65].ToString(),
                        Input1Mes7 = data.values[indice + 66].ToString(),
                        Input1Mes8 = data.values[indice + 67].ToString(),
                        Input1Mes9 = data.values[indice + 68].ToString(),
                        Input1Mes10 = data.values[indice + 69].ToString(),
                        Input1Mes11 = data.values[indice + 70].ToString(),
                        Input1Mes12 = data.values[indice + 71].ToString(),
                        Input1Mes13 = data.values[indice + 72].ToString(),
                        Input1Mes14 = data.values[indice + 73].ToString(),
                        Input1Mes15 = data.values[indice + 74].ToString(),
                        Input1Mes16 = data.values[indice + 75].ToString(),
                        Input1Mes17 = data.values[indice + 76].ToString(),
                        Input1Mes18 = data.values[indice + 77].ToString(),
                        Input2Mes1 = data.values[indice + 78].ToString(),
                        Input2Mes2 = data.values[indice + 79].ToString(),
                        Input2Mes3 = data.values[indice + 80].ToString(),
                        Input2Mes4 = data.values[indice + 81].ToString(),
                        Input2Mes5 = data.values[indice + 82].ToString(),
                        Input2Mes6 = data.values[indice + 83].ToString(),
                        Input2Mes7 = data.values[indice + 84].ToString(),
                        Input2Mes8 = data.values[indice + 85].ToString(),
                        Input2Mes9 = data.values[indice + 86].ToString(),
                        Input2Mes10 = data.values[indice + 87].ToString(),
                        Input2Mes11 = data.values[indice + 88].ToString(),
                        Input2Mes12 = data.values[indice + 89].ToString(),
                        Input2Mes13 = data.values[indice + 90].ToString(),
                        Input2Mes14 = data.values[indice + 91].ToString(),
                        Input2Mes15 = data.values[indice + 92].ToString(),
                        Input2Mes16 = data.values[indice + 93].ToString(),
                        Input2Mes17 = data.values[indice + 94].ToString(),
                        Input2Mes18 = data.values[indice + 95].ToString(),
                    };


                    result.Add(sedical);
                    indice += 96;
                    name_ind++;
                } while (data.values.Count() > indice);

                var jsonstring = System.Text.Json.JsonSerializer.Serialize(result);
                jsonToCSV(jsonstring, " ");
            }
            Console.WriteLine("Pulsa cualquier tecla para finalizar.");
            Console.WriteLine("Completado");
            Console.ReadLine();
        }

        public static void jsonToCSV(string jsonContent, string delimiter)
        {
            using (var writer = new StreamWriter("sedical_result.csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                //csv.Configuration.SkipEmptyRecords = true;
                //csv.Configuration.WillThrowOnMissingField = false;
                csv.Configuration.Delimiter = delimiter;

                using (var dt = jsonStringToTable(jsonContent))
                {
                    foreach (DataColumn column in dt.Columns)
                    {
                        csv.WriteField(column.ColumnName);
                    }
                    csv.NextRecord();

                    foreach (DataRow row in dt.Rows)
                    {
                        for (var i = 0; i < dt.Columns.Count; i++)
                        {
                            csv.WriteField(row[i]);
                        }
                        csv.NextRecord();
                    }
                }
            }
        }
        public static DataTable jsonStringToTable(string jsonContent)
        {
            DataTable dt = JsonConvert.DeserializeObject<DataTable>(jsonContent);
            return dt;
        }
    }
}
