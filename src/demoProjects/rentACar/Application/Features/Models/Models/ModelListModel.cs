using Application.Features.Models.Dtos;
using Core.Persistence.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Models.Models
{
    public class ModelListModel: BasePageableModel//CorePackage katmanında BasePageableModel tanımladık sayfalama yapmak için
    {
        // Hem dto var hem de BasePageableModel yapısından gelen sayfalama var BrandListModel de.

        public IList<ModelListDto> Items { get; set; }//buna Items ismini verdik çünkü IPaginate içinde IList<T> Items { get; } içinde böyle bir yapı var Tekrar map lemekle ugrasmama için
    }
}
