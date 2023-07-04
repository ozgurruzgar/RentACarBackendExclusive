using Core.Entities.Concrete;
using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUserService
    {
        Task<IDataResult<List<User>>> GetAllAsync();
        Task<IDataResult<User>> GetbyIdAsync(int userId);
        IDataResult<List<OperationClaim>> GetClaims(User user);
        Task<User> GetByUserMail(string email);
        IResult Add(User user);
        IResult Delete(User user);
        IResult Update(User user);
    }
}
