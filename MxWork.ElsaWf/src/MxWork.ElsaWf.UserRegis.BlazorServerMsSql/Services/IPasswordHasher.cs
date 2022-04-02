using MxWork.ElsaWf.UserRegis.BlazorServerMsSql.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MxWork.ElsaWf.UserRegis.BlazorServerMsSql.Services
{
    public interface IPasswordHasher
    {
        HashedPassword HashPassword(string password);
        HashedPassword HashPassword(string password, byte[] salt);
    }
}
