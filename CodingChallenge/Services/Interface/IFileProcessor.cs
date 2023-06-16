using CodingChallenge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenge.Services.Interface
{
    public interface IFileProcessor
    {
        Task<ServiceResponse<List<FileIdentity>>> GetAllFilesInDirectory(string path);
        Task<ServiceResponse<List<List<FileIdentity>>>> SplitFilesBasedOnSize(List<FileIdentity> files);
    }
}
