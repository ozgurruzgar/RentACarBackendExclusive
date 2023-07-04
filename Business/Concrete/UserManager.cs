using Business.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public IResult Add(User user)
        {
            _userDal.Add(user);
            return new SuccessResult();
        }

        public IResult Delete(User user)
        {
            _userDal.Delete(user);
            return new SuccessResult();
        }

        public async Task<IDataResult<List<User>>> GetAllAsync()
        {
            var result = await _userDal.GetAllAsync();
            return new SuccessDataResult<List<User>>(result);
        }

        public async Task<IDataResult<User>> GetbyIdAsync(int userId)
        {
            var result = await _userDal.GetAsync(u=>u.Id == userId);
            return new SuccessDataResult<User>(result);
        }

        public async Task<User> GetByUserMail(string email)
        {
            var expectedUser = await _userDal.GetAsync(u => u.Email == email);
            return expectedUser;
        }

        public IDataResult<List<OperationClaim>> GetClaims(User user)
        {
            var claimList = _userDal.GetClaims(user);
            return new SuccessDataResult<List<OperationClaim>>(claimList);
        }

        public IResult Update(User user)
        {
            _userDal.Update(user);
            return new SuccessResult();
        }
    }
}
