using DocumentFormat.OpenXml.Math;
using System;
using System.Collections.Generic;

namespace cupprintAccounts.Services
{
    public class LoadExcelMaps
    {
        List<ColumnMap> columnMap = new();

        public async Task<List<ColumnMap>> GetMenu()
        {
            // Condition to check if menu is already loaded and not null
            if (columnMap?.Count > 0)
                return columnMap;

            // OpenAppPackageFileAsync as file is on application
            using var stream = await FileSystem.OpenAppPackageFileAsync("excelmap.json");
            using var reader = new StreamReader(stream);
            var items = await reader.ReadToEndAsync();
            columnMap = JsonSerializer.Deserialize<List<ColumnMap>>(items);

            return columnMap;
        }

    }
    
}
