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
using BankBL.BusinessComponents;

namespace BankBL.BusinessComponents
{
    public class ClientBusinessComponent : GenericBusinessComponent<IClientUnitOfWork>, IClientBusinessComponent
    {
        public ClientBusinessComponent(IClientUnitOfWork unitOfWork) : base(unitOfWork) { }

        public int Add(string username, string pass, UserRole userRole,
            string lastName, string name/*, string patronymic*/, DateTime birthday,
            string mobile, string email, string passportNo,
            DateTime passwordExpiration, string passportIdentityNo, string passportAuthority, 
            string placeOfResidence,  string registrationAddress)
        {
            var userToAdd = new User
            {
                Login = username,
                Password = pass,
                Role = userRole
            };
            var clientToAdd = new Client
            {
                LastName = lastName,
                Name = name,
                //Patronymic = patronymic,
                Birthday = birthday,
                Mobile = mobile,
                Email = email,
                PassportExpirationDate = passwordExpiration,
                PassportIdentificationNo = passportIdentityNo,
                PassportNo = passportNo,
                PassportAuthority = passportAuthority,
                PlaceOfResidence = placeOfResidence,
                RegistrationAddress = registrationAddress,
                User = userToAdd
            };
            _unitOfWork.ClientRepository.Add(clientToAdd);
            _unitOfWork.Save();

            return clientToAdd.ClientId;
        }

        public int Count()
        {
            return _unitOfWork.ClientRepository.GetAll().Count;
        }

        public bool Exists(string passportNo)
        {
            var client = _unitOfWork.ClientRepository.GetSingle(x => x.PassportNo == passportNo);
            return client != null;
        }

        public IList<Client> GetAll()
        {
            return _unitOfWork.ClientRepository.GetAll();
        }

        public Client GetByID(int id)
        {
            return _unitOfWork.ClientRepository.GetSingle(x=> x.ClientId == id);
        }

        public Client GetByUserId(int userId)
        {
            return _unitOfWork.ClientRepository.GetSingle(x => x.UserId == userId);
        }

        public void Update(Client client)
        {
            _unitOfWork.ClientRepository.Update(client);
            _unitOfWork.Save();
        }

        public void Delete(Client client)
        {
            _unitOfWork.ClientRepository.Remove(client);
            _unitOfWork.Save();
        }
    }
}
