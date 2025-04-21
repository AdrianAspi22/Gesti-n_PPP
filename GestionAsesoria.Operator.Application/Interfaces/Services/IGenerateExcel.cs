using GestionAsesoria.Operator.Shared.Static;
using System.Collections.Generic;
using System.IO;

namespace GestionAsesoria.Operator.Application.Interfaces.Services
{
    public interface IGenerateExcel
    {
        MemoryStream GenerateToExcel<T>(IEnumerable<T> data, List<TableColumn> columns);
    }
}