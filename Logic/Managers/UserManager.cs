using System;
using System.Collections.Generic;
using System.Text;
using BackingServices.Services;
using Database;

namespace Logic.Managers
{
    public class UserManager
    {
        private UnitOfWork _uow;
        private IdNumberService _idNumberService;
        public UserManager(UnitOfWork uow, IdNumberService idNumberService)
        {
            _uow = uow;
            _idNumberService = idNumberService;
        }

        public List<Logic.Models.User> GetUsers() 
        {
            List<Database.Models.User> usersFromDB = _uow.UserRepository.GetAll().Result;
            List<Logic.Models.User> mappedUsers = new List<Logic.Models.User>();

            foreach (Database.Models.User user in usersFromDB)
            {
                mappedUsers.Add(new Logic.Models.User()
                {
                    Id = user.Id,
                    Name = user.Name,
                    LastName = user.LastName
                }); ;
            }

            return mappedUsers;
        }

        public Logic.Models.IdNumber GetSSN() 
        {
            BackingServices.Models.IdNumber idNumberFromService = _idNumberService.GetIdNumberServiceAsync().Result;

            return new Logic.Models.IdNumber()
            {
                Id = idNumberFromService.id,
                InvalidUsSsn = idNumberFromService.invalid_us_ssn,
                Uid = idNumberFromService.uid,
                ValidUsSsn = idNumberFromService.valid_us_ssn
            };
        }

        public Logic.Models.User CreateUser(Logic.Models.User user)
        {
            Database.Models.User userToCreate = new Database.Models.User()
            {
                // Code = genCode(); // key (unique)
                Id = new Guid(),
                Name = user.Name, // MAURICIO TERCEROS => Mauricio Terceros
                LastName = user.LastName
            };
            _uow.UserRepository.CreateUser(userToCreate);
            _uow.Save();

            return new Logic.Models.User()
            {
                Id = userToCreate.Id,
                Name = userToCreate.Name,
                LastName = userToCreate.LastName
            };
        }

        public Logic.Models.User UpdateUser(Logic.Models.User user)
        {
            Database.Models.User userToUpdate = _uow.UserRepository.GetById(user.Id);

            // userToUpdate.Id = user.Id;
            userToUpdate.Name = user.Name;
            userToUpdate.LastName = user.LastName;


            _uow.UserRepository.UpdateUser(userToUpdate);
            _uow.Save();

            return new Logic.Models.User()
            {
                Id = userToUpdate.Id,
                Name = userToUpdate.Name,
                LastName = userToUpdate.LastName
            };
        }
    }
}
