using PRIME_UCR.Application.Repositories.Multimedia;
using PRIME_UCR.Domain.Models;
using PRIME_UCR.Infrastructure.DataProviders;
using System;
using System.Collections.Generic;
using System.Text;

namespace PRIME_UCR.Infrastructure.Repositories.Sql.Multimedia
{
    public class MultimediaContentRepository : GenericRepository<MultimediaContent, string>, IMultimediaContentRepository
    {
        public MultimediaContentRepository(ISqlDataProvider dataProvider) : base(dataProvider)
        {

        }
    }
}
