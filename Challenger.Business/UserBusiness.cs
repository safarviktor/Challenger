using System;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Challenger.DataAccess;
using Challenger.Models;

namespace Challenger.Business
{
    public class UserBusiness
    {
        private readonly UserRepository _userRepository;

        public UserBusiness()
        {
            _userRepository = new UserRepository();
        }

        public async Task<GetUserResult> GetUser(string email, string password)
        {
            var completeUser = await _userRepository.GetCompleteUserByEmail(email);

            if (completeUser == null)
            {
                return new GetUserResult()
                {
                    Message = MessageType.InvalidCredentials,
                    Success = false
                };
            }

            var inputPasswordHash = GetCompletePasswordHash(password, Convert.FromBase64String(completeUser.PasswordSalt));

            if (inputPasswordHash != completeUser.PasswordHash)
            {
                return new GetUserResult()
                {
                    Message = MessageType.InvalidCredentials,
                    Success = false
                };
            }

            return new GetUserResult()
            {
                Success = true,
                User =  new UserModel()
                {
                    Email = completeUser.Email,
                    Id = completeUser.Id,
                    Name = completeUser.Name
                }
            };
        }

        public async Task<CreateUserResult> CreateUser(CreateUserModel model)
        {
            if (model.Password != model.ConfirmPassword)
            {
                return new CreateUserResult()
                {
                    Success = false,
                    Message = MessageType.ConfirmPasswordIncorrect
                };
            }

            if (await _userRepository.EmailExists(model.Email))
            {
                return new CreateUserResult()
                {
                    Success = false,
                    Message = MessageType.UserEmailAlreadyExists
                };
            }

            var salt = GetNewSalt();
            var passwordHash = GetCompletePasswordHash(model.Password, salt);

            var userId = await _userRepository.CreateUser(model.Name, model.Email, passwordHash, Convert.ToBase64String(salt));

            return new CreateUserResult()
            {
                Success = true
            };
        }

        private string GetCompletePasswordHash(string password, byte[] salt)
        {
            var hashedPasswordWithSalt = HashPasswordAndSalt(password, salt);

            var hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hashedPasswordWithSalt, 0, hashBytes, 16, 20);
            var passwordHash = Convert.ToBase64String(hashBytes);
            return passwordHash;
        }

        private byte[] HashPasswordAndSalt(string password, byte[] salt)
        {
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 1000);
            return pbkdf2.GetBytes(20);
        }

        private byte[] GetNewSalt()
        {
            byte[] salt;
            new RNGCryptoServiceProvider(salt = new byte[16]);
            return salt;
        }
    }
} 