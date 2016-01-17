using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.BusinessComponents;
using BankBL.Interfaces;
using BankUnitOfWork.Interfaces;
using Entities;
using Entities.Enums;
//using BankDAL.Repositories;

namespace BankBL.BusinessComponents
{
    public class UserBusinessComponent : GenericBusinessComponent<IUserUnitOfWork>, IUserBusinessComponent
    {
        public UserBusinessComponent(IUserUnitOfWork unitOfWork) : base(unitOfWork) { }

        public UserRole? Login(string name, string pass)
        {
            var user = _unitOfWork.UserRepository.GetSingle(x => x.Login == name && x.Password == pass);
            if (user == null)
            {
                return null;
            }
            else
            {
                return user.Role;
            }
        }

        public bool? IsActive(string name, string pass)
        {
            var user = _unitOfWork.UserRepository.GetSingle(x => x.Login == name && x.Password == pass);
            if (user == null)
            {
                return null;
            }
            else
            {
                return user.IsActive;
            }
        }

        public bool Exists(string name)
        {
            var user = _unitOfWork.UserRepository.GetSingle(x => x.Login == name);
            return user != null;
        }

        public int Add(string name, string pass, UserRole userRole)
        {
            var userToAdd = 
                new User
                {
                    Login = name,
                    Password = pass,
                    Role = userRole,
                    IsActive = true
                };
            _unitOfWork.UserRepository.Add(userToAdd);
            _unitOfWork.Save();

            return userToAdd.UserId;
        }

        public IEnumerable<User> GetAll()
        {
            return _unitOfWork.UserRepository.GetAll();
        } 

        public int GetIdByLogin(string name)
        {
            var id = _unitOfWork.UserRepository.GetSingle(x => x.Login == name).UserId;
            return id;
        }

        public void Update(User user)
        {
            _unitOfWork.UserRepository.Update(user);
            _unitOfWork.Save();
        }

        public void Delete(User user)
        {
            _unitOfWork.UserRepository.Remove(user);
            _unitOfWork.Save();
        }
        /// TODO: Add remaining methods here
    }
}
