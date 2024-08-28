using CsvHelper;
using CsvHelper.Configuration;
using StudentsWebApi.Models;
using System.Globalization;
using System.Formats.Asn1;

namespace StudentsWebApi.DataContext
{
    public class DataSeeder
    {
        public static async Task SeedDataBaseAsync(ApplicationDbContext context)
        {
            if (context.Students.Any())
            {
                return;
            }

            var caminhoArquivo = Path.Combine(Directory.GetCurrentDirectory(), "SeedData", "data.csv");

            using (var leitor = new StreamReader(caminhoArquivo))

            using (var arquivoCsv = new CsvReader(leitor, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                Delimiter = ",",
            }))
            {
                var records = arquivoCsv.GetRecords<StudentModel>().ToList();
                await context.Students.AddRangeAsync(records);
                await context.SaveChangesAsync();
            }
        }
    }
}
