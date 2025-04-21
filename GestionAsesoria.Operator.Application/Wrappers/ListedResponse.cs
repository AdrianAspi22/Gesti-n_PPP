using System.Collections.Generic;

namespace GestionAsesoria.Operator.Application.Wrappers
{
    public class ListedResponse<T> : Response<T>
        where T : class
    {
        public ListedResponse() { }

        public ListedResponse(List<T> items)
        {
            Items.AddRange(items);
            if (items.Count > 0)
            {
                Succeeded = true;
            }
        }
        public List<T> Items { get; set; } = new List<T>();
    }
}
