using MxWork.ElsaWf.UserRegis.BlazorServerMongo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MxWork.ElsaWf.UserRegis.BlazorServerMongo.Services
{
    public interface IPasswordHasher
    {
        HashedPassword HashPassword(string password);
        HashedPassword HashPassword(string password, byte[] salt);
    }
}
