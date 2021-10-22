﻿using AuthorizationJWTTeam2.CakeModel;
using AuthorizationService.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthorizationService.Provider
{
    public class AuthProvider:IAuthProvider
    {
        private readonly ICredentialsRepo obj;
        public AuthProvider(ICredentialsRepo _obj)
        {
            obj = _obj;
        }
        /// <summary>
        /// This method is responsible for generating token as per the userinfo given by the authenticate method.
        /// </summary>
        /// <param name="userInfo"></param>
        /// <param name="_config"></param>
        /// <returns></returns>
        public string GenerateJSONWebToken(Authenticate userInfo,IConfiguration _config)
        {
            if (userInfo == null)
                return null;
            try
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                    _config["Jwt:Issuer"],
                    null,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: credentials);

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch(Exception)
            {
                return null;
            }
            
        }
        /// <summary>
        /// This method is used to authenticate user if the user credentials exist int the database and it will return the same.
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>

        public dynamic AuthenticateUser(Authenticate login)
        {
            if(login==null)
            {
                return null;
            }
            try
            {
                Authenticate user = null;                

                List<Customer> cust = obj.GetCredentials();

                if (cust == null)
                    return null;
                else
                {
                    /*foreach(var item in ValidUsersDictionary)
                    {
                        if (item.Key == login.Name && item.Value == login.Password)
                        {
                            user = new Authenticate { Name = login.Name, Password = login.Password };
                            break;
                        }
                    }*/
                    if (cust.Any(u => u.Phone == login.Phone && u.Password == login.Password))
                    {
                        user = new Authenticate { Phone = login.Phone, Password = login.Password };
                    }
                }               

                return user;
            }
            catch(Exception)
            {
                return null;
            }
            
        }
    }
}
