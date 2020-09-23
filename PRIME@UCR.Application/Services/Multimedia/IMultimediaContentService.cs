using PRIME_UCR.Application.Services;
using PRIME_UCR.Application.Repositories;
using PRIME_UCR.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PRIME_UCR.Application.Services.Multimedia
{

    public interface IMultimediaContentService {

        MultimediaContent AddFile();

    }
}
