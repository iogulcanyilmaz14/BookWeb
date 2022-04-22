﻿using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<User, KitapSatisContext>, IUserDal
    {
        public List<AuthorityDto> GetAuthorities(User user)
        {
            using (KitapSatisContext context = new KitapSatisContext())
            {
                var result = from authority in context.Authorities
                             join userAuthority in context.UserAuthorities
                             on authority.Id equals userAuthority.AuthorityId
                             where userAuthority.UserId == user.Id
                             select new AuthorityDto { AuthorityId = authority.Id, AuthorityName = authority.Name };
                return result.ToList();

            }
        }

        public UserSecurityQuestionDto GetUserSecurityQuestion(User user)
        {
            using (KitapSatisContext context = new KitapSatisContext())
            {
                var result = from securityQuestion in context.SecurityQuestions
                             where user.SecurityQuestionId == securityQuestion.Id
                             select new UserSecurityQuestionDto { UserEmail = user.Email, SecurityQuestion = securityQuestion.Question, SecurityQuestionAnswer = user.SecurityQuestionAnswer };
                return result.FirstOrDefault();
            }
        }
    }
}
