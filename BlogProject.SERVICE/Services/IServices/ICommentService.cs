using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.SERVICE.Services.IServices
{
    public interface ICommentService
    {
        Task<int> CountAsync();
    }
}
