using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UserDataCommonLayer.Models;

namespace UserDataRepositoryLayer.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(ComposeMail composeMail);
    }
}
