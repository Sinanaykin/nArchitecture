using Application.Features.Brands.Dtos;
using Core.Persistence.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Brands.Models
{
    public class BrandListModel: BasePageableModel//CorePackage katmanında BasePageableModel tanımladık sayfalama için
    {
        // Hem dto var hem de BasePageableModel yapısından gelen sayfalama var BrandListModel de.
        public IList<BrandListDto> Items { get; set; }//buna Items ismini verdik çünkü IPaginate içinde IList<T> Items { get; } içinde böyle bir yapı var Tekrar map lemekle ugrasmama için
    }
}
