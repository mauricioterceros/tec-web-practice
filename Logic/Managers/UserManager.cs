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
            List<Database.Models.User> userFromDB = _uow.UserRepository.GetAll().Result;
            List<Logic.Models.User> mappedUsers = new List<Logic.Models.User>();

            foreach (Database.Models.User user in userFromDB)
            {
                mappedUsers.Add(new Logic.Models.User()
                {
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
    }
}
