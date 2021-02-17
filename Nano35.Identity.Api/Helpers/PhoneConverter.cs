using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Nano35.Identity.Api.Helpers
{
    public class PhoneConverter :ICustomPhoneConverter
    {
        public string Phone { get; }

        private string CurrentPhone => this.Phone; // Comes like +7(965)597-56-79

        public void RuPhoneConverter()
        {
            //string result =
            //return result;
        }
        
    }
    
    public interface ICustomPhoneConverter
    {
        string Phone {get;}
    }
}