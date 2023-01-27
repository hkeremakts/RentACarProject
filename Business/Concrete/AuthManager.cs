using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Validation;
using Core.Entities.DTOs;
using Core.Utilities.Business;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        IUserService _userService;
        ITokenHelper _tokenHelper;
        public AuthManager(IUserService userService, ITokenHelper tokenHelper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
        }
        
        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var claims = _userService.GetClaims(user);
            var result = _tokenHelper.CreateToken(user, claims.Data);
            return new SuccessDataResult<AccessToken>(result);
        }

        public IDataResult<User> Login(UserForLoginDto userForLoginDto)
        {
            var userToCheck=_userService.GetByEmail(userForLoginDto.Email);
            var result =BusinessRules.Run(CheckIfUserExists(userToCheck.Data),
                CheckIfPasswordIsCorrect(userToCheck.Data, userForLoginDto));
            if (result!=null)
            {
                return new ErrorDataResult<User>(result.Message);
            }
            return new SuccessDataResult<User>(userToCheck.Data,Messages.SuccessfulLogin);
        }
        public IDataResult<User> Register(UserForRegisterDto userForRegisterDto)
        {
            var result = BusinessRules.Run(CheckIfUserAlreadyExists(userForRegisterDto));
            if (result!=null)
            {
                return new ErrorDataResult<User>(result.Message);
            }
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(userForRegisterDto.Password,out passwordHash,out passwordSalt);
            var user = new User()
            {
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                Email = userForRegisterDto.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };
            _userService.Add(user);
            return new SuccessDataResult<User>(user,Messages.UserRegistered);
        }

        private IResult CheckIfPasswordIsCorrect(User userToCheck,UserForLoginDto userForLoginDto)
        {
            if (userToCheck==null)
            {
                return new ErrorResult();
            }
            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
            {
                return new ErrorResult(Messages.PasswordError);
            }
            return new SuccessResult();
        }
        private IResult CheckIfUserExists(User userToCheck)
        {
            if (userToCheck==null)
            {
                return new ErrorResult(Messages.UserNotFound);
            }
            return new SuccessResult();
        }
        private IResult CheckIfUserAlreadyExists(UserForRegisterDto userForRegisterDto)
        {
            if (_userService.GetByEmail(userForRegisterDto.Email).Data != null)
            {
                return new ErrorResult(Messages.UserAlreadyExists);
            }
            return new SuccessResult();
        }
    }
}
